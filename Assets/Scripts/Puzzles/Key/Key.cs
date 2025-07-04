using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public KeyType type;

    public ItemSO inventoryItem;

    public void Interact(GameObject interactor)
    {
        InHandKey inhandKey = interactor.GetComponent<InHandKey>();

        if (inhandKey != null)
        {
            inhandKey.SetKey(type);
            Debug.Log("Picked up key: " + type);
        }

        var inventoryController = interactor.GetComponent<InventoryController>();
        if (inventoryController != null && inventoryItem != null)
        {
            var inventory = inventoryController.GetInventory();
            int slotIndex = inventory.AddItem(inventoryItem);
            if (slotIndex >= 0)
            {
                inventoryController.UpdateInventorySlot(slotIndex, inventoryItem.ItemImage);
                Debug.Log("Item adicionado ao inventário: " + inventoryItem.Name);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("Inventário cheio! Não foi possível adicionar: " + inventoryItem.Name);
            }
            //inventoryController.UpdateInventorySlot(slotIndex, inventoryItem.ItemImage); // atualiza a UI
            //Debug.Log("Item adicionado ao inventário: " + inventoryItem.Name);
        }

        Destroy(gameObject);
    }
}
