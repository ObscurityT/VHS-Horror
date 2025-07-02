using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public Image itemImage;
    public Image slotBackground;
    public Sprite normalSprite;
    public Sprite selectedSprite;

    public bool isFull = false;
    public bool isSelected = false;

    private ItemData itemData;


    public void AddItem(Item item)
    {
        itemData = item.data;

        Debug.Log("Slot " + name + " recebeu o item: " + itemData.itemName);

        if (itemImage == null)
        {
            Debug.LogError("itemImage está NULL no slot " + name);
        }

        if (itemData.icon == null)
        {
            Debug.LogWarning("itemData.icon está NULL para " + itemData.itemName);
        }

        itemImage.sprite = itemData.icon;
        itemImage.gameObject.SetActive(true);
        isFull = true;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isFull) return;

        Debug.Log("Slot clicado: " + itemData.itemName);

        InventoryManager.instance.DeselectAllSlots();

        isSelected = true;
        slotBackground.sprite = selectedSprite;

        InventoryManager.instance.ShowPreview(itemData);

    }

    public void Deselect()
    {
        isSelected = false;
        slotBackground.sprite = normalSprite;
    }
}
