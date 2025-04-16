using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Harvestable : Interactable
{
    [System.Serializable]
    public class ItemEntry
    {
        public Item item; // Предмет
        public int quantity; // Количество
    }

    [SerializeField] private List<ItemEntry> itemsToHarvest = new(); // Список предметов и их количества

    // Публичное свойство для доступа к itemsToHarvest
    public List<ItemEntry> ItemsToHarvest
    {
        get => itemsToHarvest;
        set => itemsToHarvest = value;
    }

    public override void Interact()
    {
        Inventory playerInventory = FindObjectOfType<Inventory>();

        foreach (var entry in itemsToHarvest)
        {
            if (entry.item != null && entry.quantity > 0)
            {
                Debug.Log($"Harvested {entry.quantity}x {entry.item.id}");
                playerInventory.AddItem(entry.item, entry.quantity); // Добавить предметы в инвентарь
            }
        }

        onInteract?.Invoke(); // Trigger the event if there are any listeners  
        Destroy(gameObject);
    }
}

