using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public ItemData data;
    public bool collected;

    [SerializeField] GameObject visual;
    [SerializeField] Collider _collider;

    private void Start()
    {
        if (data == null)
        {
            Debug.LogError("Item sem ItemData!");
        }
    }

    public void Interact(GameObject interactor)
    {
        if (collected) return;

        Debug.Log($"Item coletado via interação: {data.itemName}");
        collected = true;
        InventoryManager.instance.AddItem(this);
        RemoveFromScene();
    }

    public void RemoveFromScene()
    {
        if (visual != null) visual.SetActive(false);
        if (_collider != null) _collider.enabled = false;
    }
}
