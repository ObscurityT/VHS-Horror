using System.Collections.Generic;
using UnityEngine;

public class BookPuzzleManager : MonoBehaviour
{
    public PuzzleAudioHelper audioHelper;
    public List<SlotBook> slots; 
    public List<int> correctOrder; 
    public GameObject puzzlePanel;
    private bool puzzleSolved = false;


    public void CheckPuzzle()
    {
        Debug.Log($"Ordem atual: {slots[0].bookID}, {slots[1].bookID}, {slots[2].bookID}");
        Debug.Log($"Ordem correta: {correctOrder[0]}, {correctOrder[1]}, {correctOrder[2]}");

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].bookID != correctOrder[i])
            {
                Debug.Log("Ainda não está na ordem!");
                audioHelper.PlayFailSound();
                return;
            }
        }
        
        Debug.Log("Sucesso!");
        audioHelper.PlaySuccessSound();
        puzzleSolved = true;
        StartCoroutine(ClosePuzzleAfterDelay(1.5f));
    }

    void Update()
    {
        if (puzzlePanel != null && puzzlePanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePuzzle();
        }
    }

    public void OpenPuzzle()
    {
        if (puzzleSolved) return;

        if (puzzlePanel != null)
            puzzlePanel.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        var player = FindFirstObjectByType<PlayerController>();
        if (player != null) player.canLook = false;

        Time.timeScale = 0f;
    }

    public void ClosePuzzle()
    {
        if (puzzlePanel != null)
            puzzlePanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        var player = FindFirstObjectByType<PlayerController>();
        if (player != null) player.canLook = true;

        Time.timeScale = 1f;
    }

    private System.Collections.IEnumerator ClosePuzzleAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        if (puzzlePanel != null)
            puzzlePanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        var player = FindFirstObjectByType<PlayerController>();
        if (player != null) player.canLook = true;
    }
}
