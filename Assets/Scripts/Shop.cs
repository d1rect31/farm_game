using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : Interactable
{
    [System.Serializable]
    public class ItemEntry
    {
        public Item item; // �������
        public int quantity; // ����������
    }

    [SerializeField] private List<ItemEntry> itemsToBuy = new(); // ������ ��������� � �� ����������

    // ��������� �������� ��� ������� � itemsToHarvest
    public List<ItemEntry> ItemsToBuy
    {
        get => itemsToBuy;
        set => itemsToBuy = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void Interact()
    {
        Inventory playerInventory = FindObjectOfType<Inventory>();
        var money = playerInventory.money;
        if (money>3) 
        {
            playerInventory.money-=3;
            foreach (var entry in itemsToBuy)
            {
                if (entry.item != null && entry.quantity > 0)
                {
                    Debug.Log($"Bought {entry.quantity}x {entry.item.id}");
                    playerInventory.AddItem(entry.item, entry.quantity);
                }
            }
        }
        else {Debug.Log("Not enough money");}
        onInteract?.Invoke();
    }
}
