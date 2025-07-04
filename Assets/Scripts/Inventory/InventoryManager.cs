using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [Header("UI")]
    public GameObject inventoryMenu;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public Image imagePreview;

    [Header("Slots")]
    public GameObject slotContainer;
    private ItemSlot[] slots;

    private bool isActive = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        ClearPreview();
        inventoryMenu.SetActive(isActive);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isActive = !isActive;
            inventoryMenu.SetActive(isActive);

            if (isActive)
            {
                EnsureSlotsLoaded();
            }
        }
    }

    public void AddItem(Item item)
    {
        // Garante que os slots estão carregados mesmo se o inventário ainda não foi aberto
        EnsureSlotsLoaded();

        foreach (var slot in slots)
        {
            Debug.Log("Verificando slot: " + slot.name + " | isFull: " + slot.isFull);

            if (!slot.isFull)
            {
                slot.AddItem(item);
                Debug.Log("Item adicionado ao slot: " + item.data.itemName);
                return;
            }
        }

        Debug.LogWarning("Todos os slots estão ocupados ou com erro!");
    }

    public void DeselectAllSlots()
    {
        foreach (var slot in slots)
        {
            slot.Deselect();
        }

        ClearPreview();
    }

    private void ClearPreview()
    {
        imagePreview.gameObject.SetActive(false);
        titleText.text = "";
        descriptionText.text = "";
    }

    private void EnsureSlotsLoaded()
    {
        if (slots == null || slots.Length == 0)
        {
            slots = slotContainer.GetComponentsInChildren<ItemSlot>(true); // importante o true!
            Debug.Log("Slots carregados dinamicamente: " + slots.Length);
        }
    }

    public void ShowPreview(ItemData data)
    {
        imagePreview.sprite = data.icon;
        imagePreview.gameObject.SetActive(true);
        titleText.text = data.itemName;
        descriptionText.text = data.description;
    }
}
