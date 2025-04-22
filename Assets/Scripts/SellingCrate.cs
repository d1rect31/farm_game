using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SellingCrate : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void Interact() 
    {
        Inventory playerInventory = FindObjectOfType<Inventory>();
        var itemToSell = playerInventory.activeElement;
        if (itemToSell.id == "Bubblegum Mango") 
        {
            inventory.RemoveItem(itemToSell, 1);
            inventory.money +=4;
            Debug.Log("Sold 1 Bubblegum Mango for 4 money");
        }
        else if (itemToSell.id == "Brassiola") 
        {
            inventory.RemoveItem(itemToSell, 1);
            inventory.money +=4;
            Debug.Log("Sold 1 Brassiola for 3 money");
        }
        else if (itemToSell.id == "Blue Turnip") 
        {
            inventory.RemoveItem(itemToSell, 1);
            inventory.money +=4;
            Debug.Log("Sold 1 Blue Turnip for 3 money");
        }
        else if (itemToSell.id == "Lumi-Tomato") 
        {
            inventory.RemoveItem(itemToSell, 1);
            inventory.money +=4;
            Debug.Log("Sold 1 Lumi-Tomato for 3 money");
        }
        else {Debug.Log("You have nothing to sell");} 
    }
}
