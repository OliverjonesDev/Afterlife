using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenButton : Interactable
{
    [SerializeField]
    private INKManager ink;

    private void Awake()
    {

    }
    
    public override void InteractionBehaviour()
    {
        ink.Judge(true);
        interactedWith = false;
        
    }
}
