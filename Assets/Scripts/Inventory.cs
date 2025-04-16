using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<Item, int> items = new();
    [SerializeField] private InventoryUI inventoryUI;

    public void AddItem(Item item, int quantity = 1)
    {
        for (int i = 0; i < quantity; i++)
        {
            if (item != null)
            {
                if (items.ContainsKey(item))
                {
                    items[item] += 1; // Увеличиваем значение на 1
                }
                else
                {
                    items.Add(item, 1); // Добавляем новый ключ с начальным значением 1
                }
                inventoryUI.UpdateUI();
            }
            else
            {
                Debug.LogWarning("Item is null and cannot be added to the inventory");
            }
        }
    }
}