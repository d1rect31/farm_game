using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Harvestable : Interactable
{
    public override void Interact()
    {
        Inventory playerInventory = FindObjectOfType<Inventory>();
        Debug.Log("Harvested " + ItemID.id);
        onInteract?.Invoke(); // Trigger the event if there are any listeners  
        playerInventory.AddItem(ItemID);
        Destroy(gameObject);
    }
}

