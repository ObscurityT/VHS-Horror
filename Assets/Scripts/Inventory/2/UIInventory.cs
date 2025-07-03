using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;


public class UIInventory : MonoBehaviour
{
    [SerializeField]
    private List<UIInventoryItem> slotList = new(); // Referências manuais aos slots fixos

    [SerializeField]
    private UIInventoryDescription itemDescription;

    public event Action<int> OnDescriptionRequested;

    

    private void Awake()
    {
        Hide();
        itemDescription.ResetDescription();
    }
    public void InitializeInventoryUI(Dictionary<int, InventoryItem> items)
    {
        for (int i = 0; i < slotList.Count; i++)
        {
            var slot = slotList[i];

            slot.OnItemClicked -= HandleItemSelection;
            slot.OnItemClicked += HandleItemSelection;


            if (items.TryGetValue(i, out InventoryItem inventoryItem))
            {
                slot.SetData(inventoryItem.item.ItemImage);
            }
            else
            {
                slot.ClearSlot();
            }
        }
    }


    public void UpdateInventorySlot(int slotIndex, Sprite itemImage)
    {
        if (slotIndex >= 0 && slotIndex < slotList.Count)
        {
            slotList[slotIndex].SetData(itemImage);
            Debug.Log($"Slot {slotIndex} atualizado com imagem {itemImage.name}");
        }
        else
        {
            Debug.LogWarning("Tentativa de atualizar slot fora do intervalo válido.");
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        // Para garantir que o evento está conectado:
        foreach (var slot in slotList)
        {
            slot.OnItemClicked -= HandleItemSelection; // Evita duplicatas
            slot.OnItemClicked += HandleItemSelection;
        }

    }

    public void UpdateData(int itemIndex,Sprite itemImage)
    {
        if (slotList.Count > itemIndex)
        {
            slotList[itemIndex].SetData(itemImage);
        }
    }
    private void HandleItemSelection(UIInventoryItem item)
    {
        Debug.Log("Slot clicado: " + item.gameObject.name + " | Está vazio? " + item.IsEmpty);

        foreach (var slot in slotList)
            slot.Deselect();

        if (item.IsEmpty)
        {
            itemDescription.ResetDescription();
            Debug.Log("Descrição resetada porque o slot está vazio");
            return;
        }

        item.Select();

        int index = slotList.IndexOf(item);
        OnDescriptionRequested?.Invoke(index);

    }

  

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void DeselectAllItems()
    {
        foreach (UIInventoryItem item in slotList)
        {
            item.Deselect();
        }
    }

    public void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
    {
        itemDescription.SetDescription(itemImage, name, description);

        foreach (var slot in slotList)
            slot.Deselect();

        slotList[itemIndex].Select();
    }

    public void ResetSelection()
    {
        itemDescription.ResetDescription();
        foreach (var slot in slotList)
            slot.Deselect();
    }

  

}
