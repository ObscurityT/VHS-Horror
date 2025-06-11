using UnityEngine;

public class SimpleInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Você interagiu com o objeto!");
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = Color.red;
        }
    }
}
