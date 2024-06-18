using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

//Object viewer interaction made by Olly - Student Number: 33697643
public class ObjectViewerInteraction : Interactable
{
    //ADEWUYI
    [SerializeField]
    PlayerController player;
    //ADEWUYI
    PlayerInput playerInput;
    InputAction camLook;
    MeshRenderer mesh;
    Transform originalTransform, objectCurTransform;
    Vector2 lookVector;
    private Quaternion originalRot;
    public bool resetRotationAfterExitInteraction;
    public bool rotateX, rotateY, rotateZ;
    private Transform camRot;

    private void Awake()
    {
        if (GetComponent<MeshRenderer>()) mesh = GetComponent<MeshRenderer>();
        else { Debug.LogError(gameObject.name + " Does not have a mesh renderer, please add one before using. Line 13 in Object Viewer Interaction"); }
        originalTransform = transform;
        originalRot = transform.rotation;
    }
    private void Start()
    {
        playerInput = player.input;
        camLook = playerInput.PlayerControls.Look;
        //playerInput.PlayerControls.Enable();
    }
    public void ObjectRotation()
    {
        lookVector.y += camLook.ReadValue<Vector2>().x * .5f;
        lookVector.x = Mathf.Clamp(lookVector.x - camLook.ReadValue<Vector2>().y * .5f, -85, 90);
        transform.rotation = Quaternion.Euler(lookVector.x, lookVector.y, 0);
    }
    
    public override void InteractionBehaviour()
    {
        if (camRot == null) camRot = Camera.main.transform;
        if (player.currentState != player.inspectState) player.ChangePlayerState(player.inspectState);
        ObjectRotation();
        //This creates a bug that I cant fix for now.
        Camera.main.transform.LookAt(this.transform);
        Debug.Log("INTERACTED WITH: " + gameObject.name);
    }
    public override void InteractionExitBehaviour()
    {
        Debug.Log("EXIT INTERACTED WITH: " + gameObject.name);
        if (resetRotationAfterExitInteraction)
        {
            transform.position = originalTransform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRot, 10 * Time.deltaTime);
        }
    }
}
