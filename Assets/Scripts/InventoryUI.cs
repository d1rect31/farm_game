using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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
        // Удаляем все дочерние объекты
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Создаем новые объекты ItemUI
        foreach (KeyValuePair<Item, int> entry in playerInventory.items)
        {
            Item item = entry.Key;
            int count = entry.Value;
            GameObject itemUI = Instantiate(itemUIPrefab, transform);

            // Настраиваем ItemUI
            var itemUIScript = itemUI.GetComponent<ItemUI>();
            itemUIScript.Setup(item, count, playerInventory);

            // Добавляем обработчик клика
            if (itemUI.TryGetComponent<Button>(out var button))
            {
                button.onClick.AddListener(itemUIScript.OnClick);
            }
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
