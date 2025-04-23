using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
public class Inventory : MonoBehaviour
{
    public int money = 0;
    public Dictionary<Item, int> items = new();
    public InventoryUI inventoryUI;
    public Item activeElement;
    public void Start()
    {
        
    }
    public void AddItem(Item item, int quantity = 1)
    {
        for (int i = 0; i < quantity; i++)
        {
            if (item != null)
            {
                if (items.ContainsKey(item))
                {
                    items[item] += 1; 
                }
                else
                {
                    items.Add(item, 1); 
                }
                inventoryUI.UpdateUI();
            }
            else
            {
                Debug.LogWarning("Item is null and cannot be added to the inventory");
            }
        }
    }
    public void RemoveItem(Item item, int quantity = 1)
    {
        if (item == null || !items.ContainsKey(item))
        {
            Debug.LogWarning("Item not found in inventory or is null");
            return;
        }

        items[item] -= quantity;

        if (items[item] <= 0)
        {
            items.Remove(item);
        }

        inventoryUI.UpdateUI();
    }
    public void SetActiveElement(Item item)
    {
        if (item != null && items.ContainsKey(item))
        {
            activeElement = item;
            Debug.Log("Active element set to: " + item.name);
        }
        else
        {
            Debug.LogWarning("Cannot set active element. Item is null or not in inventory.");
        }
    }
}