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
        public Item item;
        public int quantity;
    }

    [SerializeField] private List<ItemEntry> itemsToHarvest = new(); // List of items and their count

    // ��������� �������� ��� ������� � itemsToHarvest
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
                playerInventory.AddItem(entry.item, entry.quantity); // Add the item to the player's inventory
            }
        }

        onInteract?.Invoke(); // Trigger the event if there are any listeners  
        Destroy(gameObject);
    }
}

