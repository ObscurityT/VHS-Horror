using TMPro;
using UnityEngine;

public class NotePickup : MonoBehaviour, IInteractable
{
    [Header("Texto e Ordem da Nota")]
    [TextArea]
    public string noteText;
    public int noteOrder = 0;
    
    
    
    private PuzzleAudioHelper audioHelper;

    public void Start()
    {
        audioHelper = GetComponent<PuzzleAudioHelper>();
    }


    public void Interact(GameObject interactor)
    {
        Debug.Log("Interacting with the note");

        if (audioHelper != null)
            audioHelper.PlayOpenSound();

        NoteUIController.instance.ShowNote(noteText, this.gameObject);
    }
}
