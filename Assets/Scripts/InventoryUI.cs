using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    public Inventory playerInventory;
    public GameObject itemUIPrefab;
    

    void Start()
    {
        UpdateUI();
        GetComponentInParent<Canvas>().enabled = false;
    }

    public void UpdateUI()
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
    // inventory off-on
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            GetComponentInParent<Canvas>().enabled = !GetComponentInParent<Canvas>().enabled;
        }
    }
}
