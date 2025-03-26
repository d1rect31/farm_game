using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image itemImage;
    public Text quantityText;

    public void Setup(Item item, int quantity)
    {
        itemImage.sprite = item.sprite;
        UpdateQuantity(quantity);
    }

    public void UpdateQuantity(int quantity)
    {
        quantityText.text = quantity.ToString();
    }
}
