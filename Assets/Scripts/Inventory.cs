using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
public class Inventory : MonoBehaviour
{
    public int money = 0;
    public Dictionary<Item, int> items = new(14);
    public Dictionary<Item, int> itemsToolbar = new(8);
    public List<Dictionary<Item, int>> playerItems = new();
    public InventoryUI inventoryUI;
    public Item activeElement;
    public void Start()
    {
        playerItems.Add(items);
        playerItems.Add(itemsToolbar);
    }
    public void AddItemToInventory(Item item, int quantity = 1)
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
        public void AddItem(Item item, int quantity = 1)
        {
            HUD hud = FindObjectOfType<HUD>();
            if (itemsToolbar.Count < 8) 
                {
                        for (int i = 0; i < quantity; i++)
                    {
                        if (item != null)
                        {
                            if (itemsToolbar.ContainsKey(item))
                            {
                                itemsToolbar[item] += 1; 
                            }
                            else
                            {
                                itemsToolbar.Add(item, 1); 
                            }
                            inventoryUI.UpdateToolbarUI();
                        }
                        else
                        {
                            Debug.LogWarning("Item is null and cannot be added to the inventory");
                        }
                        Debug.Log(itemsToolbar);
                    }
                }
            else if (itemsToolbar.Count == 8 && items.Count < 14) 
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
            else {hud.DescribeEvent("Inventory full");}
    }
    public void RemoveItem(Item item, int quantity = 1)
    {
        if (item == null || !items.ContainsKey(item))
        {
                if (item == null || !itemsToolbar.ContainsKey(item))
                {
                    Debug.LogWarning("Item not found in inventory or is null");
                    return;
                }

                itemsToolbar[item] -= quantity;
                if (itemsToolbar[item] <= 0)
                {
                    itemsToolbar.Remove(item);
                }
                inventoryUI.UpdateUI();
                inventoryUI.UpdateToolbarUI();
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
        if (item != null && itemsToolbar.ContainsKey(item))
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