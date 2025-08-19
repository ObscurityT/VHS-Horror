using UnityEngine;


public class BookPuzzleInteract : MonoBehaviour, IInteractable
{
    public BookPuzzleManager puzzleManager;

    public void Interact(GameObject interaction)
    {
        puzzleManager.OpenPuzzle();
    }

}