using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventory", menuName = "Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
    public int Size = 10;
    public List<InventoryItem> inventoryItems;

    public void Initialize()
    {
        inventoryItems = new List<InventoryItem>();
        for (int i = 0; i < Size; i++)
        {
            inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
    }

    //test purpose
    public int AddItem(ItemSO item)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
            {
                inventoryItems[i] = new InventoryItem { item = item };
                return i;
            }
        }

        Debug.LogWarning("Inventário cheio");
        return -1;
    }

    public Dictionary<int, InventoryItem> GetCurrentInventoryState()
    {
        Dictionary<int, InventoryItem> returnValue = new();

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
                continue;

            returnValue[i] = inventoryItems[i];
        }

        return returnValue;
    }

    public InventoryItem GetItemAt(int itemIndex)
    {
        return inventoryItems[itemIndex];
    }
}
public struct InventoryItem
{
    public ItemSO item;
    public bool IsEmpty => item == null;

    public static InventoryItem GetEmptyItem()
        => new InventoryItem
        {
            item = null
        };
}
