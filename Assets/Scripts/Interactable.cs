using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public Item ItemID;
    public Inventory playerInventory;
    public void Interact()
    {
        Destroy(gameObject);
        playerInventory.AddItem(ItemID);
    }
}

