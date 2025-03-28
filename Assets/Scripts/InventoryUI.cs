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
        // ������� ��� �������� �������
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // ������� ����� ������� ItemUI
        foreach (Item item in playerInventory.items)
        {
            GameObject itemUI = Instantiate(itemUIPrefab, transform);
            itemUI.GetComponent<ItemUI>().Setup(item);
        }
    }
}
