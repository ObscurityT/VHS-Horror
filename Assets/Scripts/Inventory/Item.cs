using InventorySystem;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;
    public bool collected;
    public string id;

    [SerializeField] GameObject visual;
    [SerializeField] Collider _collider;

    private void Start()
    {
        if (data == null) { Debug.LogError("Faltando ItemData!"); return; }
    }

    public void Collect()
    {
        collected = true;
        InventoryManager.instance.AddItem(this);
        RemoveFromScene();
    }

    public void RemoveFromScene()
    {
        visual.SetActive(false);
        _collider.enabled = false;
    }
}
