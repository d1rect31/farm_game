using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : MonoBehaviour
{
    public GameObject objectToPlant; // The prefab to instantiate
    public Transform playerTransform;

    public void Plant()
    {
        Inventory playerInventory = FindObjectOfType<Inventory>();
        if (playerInventory != null)
        {
            Item activeElement = playerInventory.activeElement;
            if (activeElement == null)
            {
                Debug.LogError("The object to plant does not have an Item component.");
                return;
            }
            if (playerInventory.items.ContainsKey(activeElement))
            {
                Instantiate(objectToPlant, playerTransform.position, Quaternion.identity);
                playerInventory.RemoveItem(activeElement, 1); 
                Debug.Log("Planted " + objectToPlant.name);
            }
            else
            {
                Debug.LogWarning("Item not found in inventory");
            }
        }
        else
        {
            Debug.LogWarning("Player inventory not found");
        }
    }
}
