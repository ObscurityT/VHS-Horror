using UnityEngine;

public class Padlock : MonoBehaviour, IInteractable
{
    public KeyType expectedKey;
    private bool unlocked = false;
    public PuzzleManager puzzleManager;

    public void Interact(GameObject interactor)
    {
        if (unlocked) return;

        InHandKey keyInHand = interactor.GetComponent<InHandKey>();
        if (keyInHand == null) return;

        if (keyInHand.GetKey() == expectedKey)
        {
            unlocked = true;
            Debug.Log("Unlocked with: " + expectedKey);
            keyInHand.ClearKey();
            puzzleManager?.CheckPuzzle();
        }
        else
        {
            Debug.Log("Wrong key.");
        }
    }

    public bool IsUnlocked() => unlocked;
}
