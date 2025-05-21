using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Plantable : Interactable
{
    private bool isOccupied = false;

    public override void Interact()
    {
        Inventory playerInventory = FindObjectOfType<Inventory>();
        if (playerInventory == null)
        {
            Debug.LogWarning("Player inventory not found.");
            return;
        }
        Item activeElement = playerInventory.activeElement;
        if (activeElement == null)
        {
            Debug.LogWarning("No active item selected for planting.");
            return;
        }
        if (!playerInventory.items.ContainsKey(activeElement) || playerInventory.items[activeElement] <= 0 )
        {
            if (!playerInventory.itemsToolbar.ContainsKey(activeElement) || playerInventory.itemsToolbar[activeElement] <= 0 )
            {
                Debug.LogWarning("Not enough items in inventory to plant.");
                return;
            }
        }
        if (isOccupied)
        {
            Debug.LogWarning("This planting slot is already occupied.");
            return;
        }

        // Creating the crop object
        GameObject cropObject = Instantiate(activeElement.prefab, transform.position, Quaternion.identity);
        Crop crop = cropObject.GetComponent<Crop>();
        if (crop != null)
        {
            crop.SetPlantingSlot(this); // Passing the planting slot reference to the crop
        }

        isOccupied = true;
        playerInventory.RemoveItem(activeElement, 1);
        Debug.Log("Planted " + activeElement.name + " in slot " + gameObject.name);
        onInteract?.Invoke();
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }
}
