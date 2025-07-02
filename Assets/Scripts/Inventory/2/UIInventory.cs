using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;


public class UIInventory : MonoBehaviour
{
    [SerializeField]
    private List<UIInventoryItem> slotList = new(); // Refer�ncias manuais aos slots fixos

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
                slotList[i].ClearSlot(); // Limpa se n�o tiver item
            }
        }
    }

    private void HandleItemSelection(UIInventoryItem item)
    {
        Debug.Log("Slot clicado: " + item.gameObject.name + " | Est� vazio? " + item.IsEmpty);

        foreach (var slot in slotList)
            slot.Deselect();

        if (item.IsEmpty)
        {
            itemDescription.ResetDescription();
            Debug.Log("Descri��o resetada porque o slot est� vazio");
            return;
        }

        item.Select();
        
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        // Para garantir que o evento est� conectado:
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
