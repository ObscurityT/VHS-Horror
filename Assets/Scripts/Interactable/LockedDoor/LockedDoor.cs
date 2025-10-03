using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    [Header("Configuração")]
    public bool isLocked = true;
    public bool hasLock = false;

    [Header("Localização")]
    public string lockedKey = "DOOR_LOCKED";   


    public void Interact(GameObject interactor)
    {
        if (isLocked)
        {
            UIMessage.Instance.ShowMessage(lockedKey);
        }
        else
        {
            Debug.Log("Porta aberta!");
        }
    }
}
