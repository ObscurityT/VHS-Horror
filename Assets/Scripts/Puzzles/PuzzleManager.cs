using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public Padlock[] padlocks;

    public void CheckPuzzle()
    {
        foreach (var l in padlocks)
        {
            if (!l.IsUnlocked())
                return; // Puzzle not solved yet
        }

        Debug.Log("Solved");
        // TODO: Call animation, sound, door opening, etc. //add sound of unlocking a door. 
    }
}

