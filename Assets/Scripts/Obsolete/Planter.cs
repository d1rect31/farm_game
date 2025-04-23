using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : MonoBehaviour
{
    private bool isOccupied = false;

    public void Plant(Item item, Inventory playerInventory)
    {
        if (isOccupied)
        {
            Debug.LogWarning("This planting slot is already occupied.");
            return;
        }

        if (item == null || playerInventory == null)
        {
            Debug.LogError("Invalid item or inventory.");
            return;
        }

        // Creating the object at the slot position
        Instantiate(item.prefab, transform.position, Quaternion.identity);
        isOccupied = true;
        playerInventory.RemoveItem(item, 1);
        Debug.Log("Planted " + item.name + " in slot " + gameObject.name);
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }
}
