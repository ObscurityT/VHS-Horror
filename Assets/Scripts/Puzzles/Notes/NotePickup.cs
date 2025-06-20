using TMPro;
using UnityEngine;

public class NotePickup : MonoBehaviour, IInteractable
{
    [Header("Texto e Ordem da Nota")]
    [TextArea]
    public string noteText;
    public int noteOrder = 0;

    public void Interact(GameObject interactor)
    {
        Debug.Log("Interacting with the note");
        NoteUIController.instance.ShowNote(noteText, this.gameObject);
    }
}
