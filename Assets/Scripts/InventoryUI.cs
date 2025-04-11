using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    public Inventory playerInventory;
    [SerializeField] private GameObject itemUIPrefab;
    

    void Start()
    {
        UpdateUI();
        GetComponentInParent<Canvas>().enabled = false;
    }

    public void UpdateUI()
    {
        // ������� ��� �������� �������
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // ������� ����� ������� ItemUI
        foreach (KeyValuePair<Item, int> entry in playerInventory.items)
        {
            Item item = entry.Key; // �������� ���� (Item) �� ����
            int count = entry.Value; // �������� �������� (����������) �� ����
            GameObject itemUI = Instantiate(itemUIPrefab, transform);
            itemUI.GetComponent<ItemUI>().Setup(item, count);
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
