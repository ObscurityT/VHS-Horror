using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/Inventory Item")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        [TextArea] public string description;
        public Sprite icon;
        public InventoryItemType itemType;
        public bool isCollectible;
    }
}