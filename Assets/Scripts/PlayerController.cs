using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    //Adewuyi Adebusuyi 33707780, unless commented otherwise
    //player input script for translating input to action
    
    [HideInInspector]
    public PlayerInput input;
    //script in charge of the object selection using ray casting
    [HideInInspector]
    public ObjectSelector objectSelector;

    //the action associated with the inputs for looking around
    public InputAction camLook;

    //player camera, lookVector is used to rotate it
    [HideInInspector]
    public Transform playerCam;
    [HideInInspector]
    public Vector2 lookVector;
    public Vector2 moveVector;
    //handles look sensivity for mouse and controller
    public float sensitivityX = 0.24f;
    public float sensitivityY = 0.24f;
    //handles allows access to ink events, here used to progress dialogue
    public INKManager inkController;
    [HideInInspector]
    public GameObject eventSystem;
    public PlayerBaseState currentState;
    public PlayerLookingState lookingState = new PlayerLookingState();
    public PlayerDialogueState dialogueState = new PlayerDialogueState();
    public PlayerInspectState inspectState = new PlayerInspectState();
    public PlayerPauseState pauseState = new PlayerPauseState();
    public PlayerStandingState standingState = new PlayerStandingState();
    private PlayerBaseState prevState;
    private float getUpDelay;
    public bool playerZoomed;
    public CharacterController _CharacterController;
    public enum PlayerStartState
    {
        dialogueState,lookingState,inspectState,pauseState,standingState
    }
    public PlayerStartState selectPlayerStartState;
    private float timePaused;
    private float timeUnpaused;
    private float timePauseDelay = 1.5f;
    public Vector2 yClamp;
    [HideInInspector]
    public bool delayPassed = true;
    public float speed;
    private void Awake()
    {
        if (GetComponent<CharacterController>())
            _CharacterController = GetComponent<CharacterController>();
        eventSystem = GameObject.Find("EventSystem");
        input = new PlayerInput();
        objectSelector = GetComponent<ObjectSelector>();
        camLook = input.PlayerControls.Look;
        switch (selectPlayerStartState)
        {
            case PlayerStartState.dialogueState:
             currentState = dialogueState;
             break;
            case PlayerStartState.lookingState:
                currentState = lookingState;
                break;
            case PlayerStartState.inspectState:
                currentState = inspectState;
                break;
            case PlayerStartState.pauseState:
                currentState = pauseState;
                break;
            case PlayerStartState.standingState:
                currentState = standingState;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        if (inkController == null)
        {
             Debug.LogError("State change failed, missing InkManager(Ink Canvas Prefab) on PlayerController Script");
        }
    }

    void Start()
    {
        playerCam = transform.Find("Player Camera").GetComponent<Transform>();
        lookVector = new Vector2(0, 260);
        
        playerCam.rotation = Quaternion.Euler(0, lookVector.y, 0);
        input.PlayerControls.Enable();
        currentState.EnterState(this);
    }

    private void ChangeState(PlayerBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
    public void ChangePlayerState(PlayerBaseState state)
    {
        //This adds a delay to opening and closing the pause menu to stop the user from spamming it. 
        //It creates a smoother affect due to the animation.
        if (currentState == pauseState)
        {
            if (timePaused > timePauseDelay)
            {
                timeUnpaused = 0;
                timePaused = 0;
                ChangeState(prevState);
                AudioManager.Instance.PlaySelectSound();
            }
        }
        else if (currentState != pauseState && state == pauseState)
        {
            if (timeUnpaused > timePauseDelay)
            {
                prevState = currentState;
                timePaused = 0;
                timeUnpaused = 0;
                ChangeState(state);
                AudioManager.Instance.PlaySelectSound();
            }
        }
        else
        {
            ChangeState(state);
            AudioManager.Instance.PlaySelectSound();
        }
        Debug.Log("Change Player state" + currentState);
        
        //trigger audio
    }
    private void Update()
    {
        /*enables looking around, y is clamped at -85 to 90 to prevent rotation going upside down, 
        current input from the player is being added to lookVector */
        currentState.UpdateState(this);
        if (currentState == pauseState)
        {
            timePaused += Time.fixedDeltaTime;
        }
        else
        {
            timeUnpaused += Time.fixedDeltaTime;
        }
        if (input.PlayerControls.Zoom.IsPressed())
        {
            cameraZoom(20);
        }
        else
        {
            if (!playerZoomed)
                cameraZoom(60);
        }
        
    }

    //check for movement and set audio
    private void OnMovement(InputValue value)
    {
        moveVector = value.Get<Vector2>();
        bool isMoving = moveVector != Vector2.zero;
        currentState.PlayAudio(isMoving);
    }

    private void OnEnable()
    {
        //calls Confirm() when A/M1 is pressed
        input.PlayerControls.Confirm.performed += Confirm;
        //toggle between dialogue and world interaction, performed with space/back
        input.PlayerControls.Pause.performed += Pause;
    }


    private void OnDisable()
    {
        input.PlayerControls.Disable();
    }

    private void Confirm(InputAction.CallbackContext obj) { currentState.ConfirmState(this); }

    //change between dialogue and camera interactivity
    private void Pause(InputAction.CallbackContext obj) { currentState.Pause(this); }

    //press Esc to exit appliction
    private void QuitApplication(InputAction.CallbackContext context)
    {
        Debug.Log("QUIT GAME");
        Application.Quit();
    }

    private void cameraZoom(float fov)
    {
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fov, 5 * Time.fixedDeltaTime);

    }
    
    public void cameraZoomWithSpeed(float fov, float speed)
    {
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fov, speed * Time.fixedDeltaTime);

    }
    public IEnumerator Delay(int delay)
    {
        yield return new WaitForSeconds(delay);
        delayPassed = true;
    }
}
