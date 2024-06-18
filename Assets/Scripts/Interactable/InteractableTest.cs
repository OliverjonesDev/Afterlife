using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//InteractableTest class made by Olly - Student Number: 33697643

public class InteractableTest : Interactable
{

    [SerializeField]
    private GameObject OpenUI;
    [SerializeField]
    private bool hasOpeningDelay;
    [SerializeField]
    private bool hasClosingDelay;
    [SerializeField]
    private float timer;

    private void Update()
    {

    }
    public override void InteractionBehaviour()
    {
        Debug.Log("Interaction called");
        OpenUI.SetActive(true);
    }

    public override void InteractionExitBehaviour()
    {
        OpenUI.SetActive(false);
    }
}
