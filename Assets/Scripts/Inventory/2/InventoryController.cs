using UnityEngine;

public class InventoryController : MonoBehaviour
{
   [SerializeField]
   private UIInventory inventory;

   public void Update()
   {
        if (Input.GetKeyDown(KeyCode.I))
        {
            bool isActive = inventory.isActiveAndEnabled;

            if (!isActive)
            {
                inventory.Show();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
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
