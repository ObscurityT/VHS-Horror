using System;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
   [SerializeField]
   private UIInventory inventory;

    [SerializeField]
    private InventorySO inventoryData;

    public void Start()
    {
        inventoryData.Initialize();
        PrepareUI();
    }

    private void PrepareUI()
    {
        inventory.InitializeInventoryUI(inventoryData.GetCurrentInventoryState());
        inventory.OnDescriptionRequested += HandleDescriptionRequest;
    }

    public void AddItemToInventory(ItemSO item)
    {
        int index = inventoryData.AddItem(item);
        if (index != -1)
        {
            inventory.UpdateInventorySlot(index, item.ItemImage);
        }
    }

    private void HandleDescriptionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
        {
            inventory.ResetSelection();
            return;
        }

        ItemSO item = inventoryItem.item;
        inventory.UpdateDescription(itemIndex, item.ItemImage, item.Name, item.Description);
    }

    public InventorySO GetInventory()
    {
        return inventoryData;
    }

    public void UpdateInventorySlot(int index, Sprite icon)
    {
        inventory.UpdateInventorySlot(index, icon);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventory.isActiveAndEnabled)
            {
                inventory.Show();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                // Atualiza visualmente os slots com os dados do inventário
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventory.UpdateData(
                        item.Key,
                        item.Value.item.ItemImage);
                }
            }
            else
            {
                inventory.Hide();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
