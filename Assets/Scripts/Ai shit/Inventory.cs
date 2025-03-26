using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public Item item;
    public int quantity;
    
    public InventoryItem(Item newItem, int qty)
    {
        item = newItem;
        quantity = qty;
    }
}

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();

    public void AddItem(Item newItem, int amount)
    {
        InventoryItem inventoryItem = items.Find(i => i.item == newItem);
        if (inventoryItem != null)
        {
            inventoryItem.quantity += amount;
        }
        else
        {
            items.Add(new InventoryItem(newItem, amount));
        }
    }

    public void RemoveItem(Item newItem, int amount)
    {
        InventoryItem inventoryItem = items.Find(i => i.item == newItem);
        if (inventoryItem != null)
        {
            inventoryItem.quantity -= amount;
            if (inventoryItem.quantity <= 0)
            {
                items.Remove(inventoryItem);
            }
        }
    }

    public void PrintInventory()
    {
        foreach (InventoryItem inventoryItem in items)
        {
            Debug.Log(inventoryItem.item.item + ": " + inventoryItem.quantity);
        }
    }
}
