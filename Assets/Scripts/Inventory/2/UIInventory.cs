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

    public event Action<UIInventoryItem> OnDescriptionRequested;

    

    private void Awake()
    {
        Hide();
        itemDescription.ResetDescription();
    }
    public void InitializeInventoryUI(List<ItemData> items)
    {
        // Limpa todos os slots
        for (int i = 0; i < slotList.Count; i++)
        {
            var slot = slotList[i];

            slot.OnItemClicked += HandleItemSelection;

            if (i < items.Count)
            {
                slotList[i].SetData(items[i].icon); // Mostra o item no slot
            }
            else
            {
                slotList[i].ClearSlot(); // Limpa se não tiver item
            }
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

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
