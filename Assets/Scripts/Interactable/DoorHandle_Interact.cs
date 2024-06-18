using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DoorHandle_Interact : Interactable
{
    [SerializeField] GameObject key;
    [SerializeField] GameObject doorPivot;
    private PlayerInventory inventory;
    bool doorOpen;
    [SerializeField] float doorSpeed;
    private Quaternion startRot;
    [SerializeField] AudioClip doorSwingAudio;
    [SerializeField] AudioClip openDoorAudio;
    [SerializeField] AudioClip lockedDoorAudio;
    [SerializeField] private AudioSource doorSource;

    public override void AdditionalStartFunctions()
    {
        startRot = doorPivot.transform.rotation;
        inventory = FindObjectOfType<PlayerInventory>();
    }
    public override void InteractionBehaviour()
    {
        
        if (key == null)
        {
            
            //doorSource.PlayOneShot(doorSwingAudio);
            interactionSound = openDoorAudio;
            promptMessage = "Open Door";
            doorOpen = true;
        }
        else
        {
            
            if (hasKey())
            {
                
                //doorSource.PlayOneShot(doorSwingAudio);
                interactionSound = openDoorAudio;
                promptMessage = "Open Door";
                doorOpen = true;
            }
            else
            {
                //door is closed and there is no key
                interactionSound = lockedDoorAudio;
                promptMessage = "The Door is Locked";
                doorOpen = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (doorOpen)
        {
            StartCoroutine(Rotate(90));
        }
        else
        {
            StartCoroutine(Rotate(0));
        }
    }

    public override void InteractionExitBehaviour()
    {
        if (hasKey() && doorOpen)
        {
            
            //doorSource.PlayOneShot(doorSwingAudio);
            interactionExitSound = openDoorAudio;
        }
        else if (hasKey() && !doorOpen)
        {
           
            //doorSource.PlayOneShot(doorSwingAudio);
            interactionExitSound = openDoorAudio;
        }
        else if (!hasKey())
        {
            interactionExitSound = lockedDoorAudio;
        }
        doorOpen = false;
    }

    public bool hasKey()
    {
        if (inventory.inventory.Contains(key))
        {
            return true;
        }

        return false;
    }
    
    IEnumerator Rotate(float rotationAmount){
        Quaternion finalRotation = Quaternion.Euler( 0, rotationAmount, 0 ) * startRot;

        while(doorPivot.transform.rotation != finalRotation){
            doorPivot.transform.rotation = Quaternion.Lerp(doorPivot.transform.rotation, finalRotation, Time.deltaTime*doorSpeed);
            yield return 0;
        }
    }
}
