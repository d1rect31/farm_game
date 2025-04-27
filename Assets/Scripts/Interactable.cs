using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public Item ItemID;
    public Inventory inventory;
    [SerializeField] GameObject player;
    // public Inventory playerInventory;
    public UnityEvent onInteract; // Event to be triggered on interaction
    private void OnValidate()
    {
        // Change the texture of the object itself
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && ItemID != null)
        {
            spriteRenderer.sprite = ItemID.sprite;
        }
    }
    public virtual void Interact()
    {
        onInteract?.Invoke(); // Trigger the event if there are any listeners  
    }
    private void Teleport() 
    {
        
    }
}

