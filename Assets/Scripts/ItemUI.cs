using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Text idText;
    public Text descriptionText;
    public Image itemImage;

    public void Setup(Item item)
    {
        //idText.text = item.id;
        //descriptionText.text = item.description;
        itemImage.sprite = item.sprite;
    }
}