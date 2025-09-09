using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public GameObject puzzlePanel; 
    private PlayerController player;

    void Start()
    {
        puzzlePanel.SetActive(false); 
        player = FindFirstObjectByType<PlayerController>();
    }

    public void OpenPuzzle()
    {
        puzzlePanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (player != null)
            player.canLook = false;

        Time.timeScale = 0f; 
    }

    public void ClosePuzzle()
    {
        puzzlePanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (player != null)
            player.canLook = true;

        Time.timeScale = 1f;
    }
}

