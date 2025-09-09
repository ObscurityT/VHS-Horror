using AudioSystem;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
   [SerializeField]
   private UIInventory inventory;

    [SerializeField]
    private InventorySO inventoryData;

    [Header("Áudio")]
    [SerializeField] private AudioClip openSfx;
    [SerializeField] private AudioClip closeSfx;
    [SerializeField] private AudioClip inventoryMusic;
    [SerializeField] private bool pauseGameTime = false;


    public void Awake()
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
        inventory.UpdateDescription(itemIndex, item.ItemImage, item.NameKey, item.DescriptionKey);
    }

    public InventorySO GetInventory()
    {
        return inventoryData;
    }

    public void UpdateInventorySlot(int index, Sprite icon)
    {
        inventory.UpdateInventorySlot(index, icon);
    }

    public void OpenInventory()
    {

        if (!inventory.isActiveAndEnabled)
        {
            if (inventoryMusic != null)
                AudioManager.Instance.PlayMusic(inventoryMusic);

            inventory.Show();

            foreach (var item in inventoryData.GetCurrentInventoryState())
            {
                inventory.UpdateData(item.Key, item.Value.item.ItemImage);
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (pauseGameTime)
                Time.timeScale = 0f;

            if (openSfx != null)
                AudioManager.Instance.PlaySFX(openSfx);
        }
    }

    public void CloseInventory()
    {
        if (inventory.isActiveAndEnabled)
        {
            inventory.Hide();

            if (pauseGameTime)
                Time.timeScale = 1f;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            AudioManager.Instance.StopMusic();

            if (closeSfx != null)
                AudioManager.Instance.PlaySFX(closeSfx);
        }
    }

    public void ToggleInventory()
    {
        if (!inventory.isActiveAndEnabled)
            OpenInventory();
        else
            CloseInventory();
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    if (!inventory.isActiveAndEnabled)
        //    {
        //        inventory.Show();
        //        Cursor.lockState = CursorLockMode.None;
        //        Cursor.visible = true;

        //        // Atualiza visualmente os slots com os dados do inventário
        //        foreach (var item in inventoryData.GetCurrentInventoryState())
        //        {
        //            inventory.UpdateData(
        //                item.Key,
        //                item.Value.item.ItemImage);
        //        }

        //        AudioManager.Instance.PlayMusic("InventoryOpen");
        //    }
        //    else
        //    {
        //        inventory.Hide();
        //        Cursor.lockState = CursorLockMode.Locked;
        //        Cursor.visible = false;
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseInventory();
        }
    }
}
