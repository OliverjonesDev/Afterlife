using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interactable class made by Olly - Student Number: 33697643

/// <summary>
/// This is the master class for the interactable objects in the game.
/// This will be the parent of all objects that can be interacted with, and their functionality scripts
/// This means we only have to access one component as they all inherit from this class.
/// </summary>

//namespace used for the Outline class
using cakeslice;

//By Oliver Jones
public class Interactable : MonoBehaviour
{
    public bool interactedWith;
    public bool exitInteraction;
    public string promptMessage;
    public bool hovering;
    private bool interactionSoundPlayed;
    private bool interactionExitSoundPlayed;
    protected Outline outline;
    public AudioClip interactionSound, interactionExitSound;
    public bool playExitAudio;

    private void Start()
    {
        if(outline != null)
        {
            outline.enabled = false;
        }
        AdditionalStartFunctions();
    }

    private void Awake()
    {
        AwakeBehaviour();

        hovering = false;
        outline = GetComponent<Outline>();
        if(outline != null)
        {
            outline.enabled = true; //GetComponent doesn't get disabled scripts
        }
    }

    private void Update()
    {
        if(outline != null) highlight();

        if (interactedWith)
        {
            InteractionBehaviour();
            if (!interactionSoundPlayed)
            {
                if (interactionSound != null)
                {
                    playExitAudio = true;
                    AudioManager.Instance.PlaySFX(interactionSound);
                }
                interactionSoundPlayed = true;interactionExitSoundPlayed = false;
            }
        }
        if (exitInteraction)
        {
            InteractionExitBehaviour();
            if (!interactionExitSoundPlayed)
            {
                if (interactionExitSound != null && playExitAudio) AudioManager.Instance.PlaySFX(interactionExitSound);
                interactionSoundPlayed = false;
                interactionExitSoundPlayed = true;
            }
        }
    }

    public void Interaction()
    {
        if (!interactedWith)
        {
            interactedWith = true;
            exitInteraction = false;
        }
        else
        {
            interactedWith = false;
            exitInteraction = true;
        }
    }
    public virtual void AwakeBehaviour(){}

    public virtual void InteractionBehaviour(){}

    public virtual void InteractionExitBehaviour(){}

    public virtual void AdditionalStartFunctions(){}
    /**
    * Hightlight function made by: Richard Bennett - Student Number: 33625957
    * Toggles the Outline script when the crosshair hovers over object
    */
    public virtual void highlight()
    {
        if(hovering) outline.enabled = true;
        else         outline.enabled = false;
        return;
    }
}


