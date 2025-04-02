using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new();
    public InventoryUI inventoryUI;

    public void AddItem(Item item)
    {
        if (item != null)
        {
            items.Add(item);
            inventoryUI.UpdateUI();
        }
        else
        {
            Debug.LogWarning("Item is null and cannot be added to the inventory");
        }
    }
}