using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public Image itemImage;
    public GameObject highlight;
    public bool isFull = false;
    public bool isSelected = false;

    private ItemData itemData;

    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public Image imagePreview;

    public void AddItem(Item item)
    {
        itemData = item.data;
        itemImage.sprite = itemData.icon;
        itemImage.gameObject.SetActive(true);
        isFull = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isFull)
        {
            InventoryManager.instance.DeselectAllSlots();

            highlight.SetActive(true);
            isSelected = true;

            imagePreview.sprite = itemData.icon;
            imagePreview.gameObject.SetActive(true);
            titleText.text = itemData.itemName;
            descriptionText.text = itemData.description;
        }
    }

    public void Deselect()
    {
        highlight.SetActive(false);
        isSelected = false;
    }
}
