using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public Item ItemID;
    public UnityEvent interacted;
    public Inventory playerInventory;
    public void Interact() 
    {
        interacted.Invoke();
        playerInventory.AddItem(ItemID);
    }
}

