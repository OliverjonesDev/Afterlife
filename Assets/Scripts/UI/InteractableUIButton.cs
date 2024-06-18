using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableUIButton : Interactable
{
    [SerializeField]
    Button button;
    public override void highlight()
    {
        if (hovering) { button.Select(); }

    }
    public override void InteractionBehaviour()
    {
        if (button.interactable)
        {
            button.onClick.Invoke();
        }
        interactedWith = false;
    }
}
