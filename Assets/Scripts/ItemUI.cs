using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Text idText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text countText;
    [SerializeField] private Image itemImage;
    [SerializeField] private GameObject selectedOutline; // ������ �� ������ SelectedOutline

    private Item item;
    private Inventory inventory;

    public void Setup(Item item, int count, Inventory inventory)
    {
        this.item = item;
        this.inventory = inventory;
        idText.text = item.id;
        itemImage.sprite = item.sprite;
        countText.text = $"{count}";
        UpdateSelectedOutline(); // ��������� ��������� SelectedOutline
    }

    public void OnClick()
    {
        if (inventory != null)
        {
            inventory.SetActiveElement(item);
            inventory.inventoryUI.UpdateUI(); // ��������� UI ����� ������
        }
    }

    private void UpdateSelectedOutline()
    {
        if (selectedOutline != null)
        {
            selectedOutline.SetActive(inventory != null && inventory.activeElement == item);
        }
    }
}
