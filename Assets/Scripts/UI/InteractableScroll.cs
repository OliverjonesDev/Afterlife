using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScroll : Interactable
{
    [SerializeField]
    PlayerController player;
    PlayerInput playerInput;
    ConsoleText console;
    private void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
            if (player == null)
            {
                Debug.LogError("Interactable Scroll missing Player object, the object you attach it in is located in TextCanvas2/Scroll View/Viewport");
                return;   
            }
        }
        playerInput = player.input;
        console = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<ConsoleText>();
        playerInput.PlayerControls.MouseWheelScroll.Enable();
        interactedWith = true;
    }
    private void Update()
    {
        if (player == null) return; 
        if (hovering && player.currentState == player.lookingState)
            console.Scroll(playerInput.PlayerControls.MouseWheelScroll.ReadValue<float>());
    }
}
