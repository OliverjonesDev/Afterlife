using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    [SerializeField] private PlayerInventory inventory;
    public override void AdditionalStartFunctions()
    {
        inventory = FindObjectOfType<PlayerInventory>();
    }
    public override void InteractionBehaviour()
    {
        inventory.inventory.Add(gameObject);
        gameObject.SetActive(false);
    }
}
