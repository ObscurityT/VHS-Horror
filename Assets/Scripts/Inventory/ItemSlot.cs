using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public Image itemImage; // ícone do item
    public Image slotBackground; // imagem de fundo do slot
    public Sprite normalSprite; // slot escuro
    public Sprite selectedSprite; // slot com highlight

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
        if (!isFull) return;

        InventoryManager.instance.DeselectAllSlots();

        isSelected = true;
        slotBackground.sprite = selectedSprite;

        imagePreview.sprite = itemData.icon;
        imagePreview.gameObject.SetActive(true);
        titleText.text = itemData.itemName;
        descriptionText.text = itemData.description;
    }

    public void Deselect()
    {
        isSelected = false;
        slotBackground.sprite = normalSprite;
    }
}
