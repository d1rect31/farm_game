using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public GameObject itemPrefab; // Префаб элемента инвентаря
    

    private Dictionary<Item, GameObject> itemInstances = new Dictionary<Item, GameObject>();

    public void UpdateInventoryDisplay()
    {
        foreach (var entry in inventory.items)
        {
            if (itemInstances.ContainsKey(entry.item))
            {
                // Обновляем количество, если предмет уже есть
                itemInstances[entry.item].GetComponent<ItemUI>().UpdateQuantity(entry.quantity);
            }
            else
            {
                // Создаем новый элемент UI
                GameObject newItem = Instantiate(itemPrefab);
                newItem.GetComponent<ItemUI>().Setup(entry.item, entry.quantity);
                itemInstances[entry.item] = newItem;
            }
        }
    }
}