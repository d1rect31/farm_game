using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public GameObject itemPrefab; // ������ �������� ���������
    

    private Dictionary<Item, GameObject> itemInstances = new Dictionary<Item, GameObject>();

    public void UpdateInventoryDisplay()
    {
        foreach (var entry in inventory.items)
        {
            if (itemInstances.ContainsKey(entry.item))
            {
                // ��������� ����������, ���� ������� ��� ����
                itemInstances[entry.item].GetComponent<ItemUI>().UpdateQuantity(entry.quantity);
            }
            else
            {
                // ������� ����� ������� UI
                GameObject newItem = Instantiate(itemPrefab);
                newItem.GetComponent<ItemUI>().Setup(entry.item, entry.quantity);
                itemInstances[entry.item] = newItem;
            }
        }
    }
}