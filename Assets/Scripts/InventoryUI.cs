using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    public Inventory playerInventory;
    public GameObject itemUIPrefab;
    

    void Start()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        // Удаляем все дочерние объекты
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Создаем новые объекты ItemUI
        foreach (Item item in playerInventory.items)
        {
            GameObject itemUI = Instantiate(itemUIPrefab, transform);
            itemUI.GetComponent<ItemUI>().Setup(item);
        }
    }
}
