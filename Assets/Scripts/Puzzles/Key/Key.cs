using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public KeyType type;

    public void Interact(GameObject interactor)
    {
        InHandKey inhandKey = interactor.GetComponent<InHandKey>(); 

        if (inhandKey != null)
        {
            inhandKey.SetKey(type);
            Debug.Log("Picked up key: " + type);
            Destroy(gameObject);

        }
    }
}
