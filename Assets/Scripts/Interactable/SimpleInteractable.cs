using UnityEngine;

public class SimpleInteractable : MonoBehaviour, IInteractable
{
    public void Interact(GameObject interaction)
    {
        Debug.Log("Voc� interagiu com o objeto!");
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = Color.red;
        }
    }
}
