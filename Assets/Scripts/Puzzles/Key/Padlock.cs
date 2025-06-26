using UnityEngine;

public class Padlock : MonoBehaviour, IInteractable
{
    public KeyType expectedKey;
    private bool unlocked = false;
    public PuzzleManager puzzleManager;
    public PadlockAnimator padlockAnimator;
    public PuzzleAudioHelper audioHelper;

    public void Interact(GameObject interactor)
    {
        if (unlocked) return;

        InHandKey keyInHand = interactor.GetComponent<InHandKey>();
        if (keyInHand == null) return;

        if (keyInHand.GetKey() == expectedKey)
        {
            unlocked = true;
            Debug.Log("Unlocked with: " + expectedKey);

            if (audioHelper != null)
                audioHelper.PlaySuccessSound();

            keyInHand.ClearKey();
            padlockAnimator?.Unlock(); 
            puzzleManager?.CheckPuzzle();
        }
        else
        {
            if (audioHelper != null)
                audioHelper.PlayFailSound();
            Debug.Log("Wrong key.");
        }
    }

    public bool IsUnlocked() => unlocked;
}
