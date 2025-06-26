using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager instance;

        [Header("Input")]
        [SerializeField] private KeyCode toggleKey = KeyCode.I;

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
            slots = slotContainer.GetComponentsInChildren<ItemSlot>();
            ClearPreview();
        }

        private void Update()
        {
            if (Input.GetKeyDown(toggleKey))
            {
                isActive = !isActive;
                inventoryMenu.SetActive(isActive);
            }
        }

        public void AddItem(Item item)
        {
            foreach (var slot in slots)
            {
                if (!slot.isFull)
                {
                    slot.AddItem(item);
                    return;
                }
            }
        }

        public void DeselectAllSlots()
        {
            foreach (var slot in slots)
                slot.Deselect();

            ClearPreview();
        }

        private void ClearPreview()
        {
            imagePreview.gameObject.SetActive(false);
            titleText.text = "";
            descriptionText.text = "";
        }
    }
}