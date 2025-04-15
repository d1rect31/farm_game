using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : MonoBehaviour
{
    public Item itemToPlant;
    public GameObject objectToPlant; // The prefab to instantiate
    public Transform playerTransform;
    [SerializeField] private InventoryUI InventoryUI;
    public void Plant()
    {
        Inventory playerInventory = FindObjectOfType<Inventory>();
        // Check if the player has the item in their inventory  
        if (playerInventory != null)
        {
            Item itemComponent = itemToPlant;
            if (itemComponent == null)
            {
                Debug.LogError("The object to plant does not have an Item component.");
                return;
            }

            // Remove the item from the inventory  
            if (playerInventory.items.ContainsKey(itemComponent))
            {
                Instantiate(objectToPlant, playerTransform.position, Quaternion.identity);
                // Check if the player has enough of the item  
                playerInventory.items[itemComponent] -= 1;
                if (playerInventory.items[itemComponent] <= 0)
                {
                    playerInventory.items.Remove(itemComponent);
                }
                Debug.Log("Planted " + objectToPlant.name);
            }
            else
            {
                Debug.LogWarning("Item not found in inventory");
            }
            InventoryUI.UpdateUI();
        }
        else
        {
            Debug.LogWarning("Player inventory not found");
        }
    }
}
