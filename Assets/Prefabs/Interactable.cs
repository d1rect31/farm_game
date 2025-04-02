using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public Item ItemID;
    public UnityEvent interacted;
    public Inventory playerInventory;
    public GameObject drop;
    public void Interact() 
    {
        interacted.Invoke();
        playerInventory.AddItem(ItemID);
        Debug.Log("added to inventory");
    }
    public void Harvest() 
    {
        Debug.Log("chopped tree");
        Destroy(gameObject);
        Instantiate(drop, transform.position, Quaternion.identity);
    }
}

