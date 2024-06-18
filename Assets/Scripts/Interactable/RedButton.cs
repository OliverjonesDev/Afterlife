using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButton : Interactable
{
    [SerializeField]
    private INKManager ink;

    private void Awake()
    {

    }
    
    public override void InteractionBehaviour()
    {
        ink.Judge(false);
        interactedWith = false;

    }
}
