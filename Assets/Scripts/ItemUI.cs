using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Text idText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text countText;
    [SerializeField] private Image itemImage;

    public void Setup(Item item, int count)
    {
        idText.text = item.id;
        //descriptionText.text = item.description;
        itemImage.sprite = item.sprite;
        countText.text = $"{count}"; // Display the count
    }
}