using UnityEngine;


public class BookPuzzleInteract : MonoBehaviour, IInteractable
{
    public BookPuzzleCanvas puzzleCanvas;

    public void Interact()
    {
        puzzleCanvas.OpenCanvas();
    }
}