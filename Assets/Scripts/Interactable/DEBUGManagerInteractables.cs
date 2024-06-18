using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Interactable testing class made by Olly - Student Number: 33697643

public class DEBUGManagerInteractables : MonoBehaviour
{
    public bool interact;

    public Interactable[] interactableTesting;


    private void Update()
    {
        if (interact)
        {
            for (int i = 0; i < interactableTesting.Length; i++)
            {
                interactableTesting[i].Interaction();
            }
            interact = false;
        }

    }

}

