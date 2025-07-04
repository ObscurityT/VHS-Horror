using TMPro;
using UnityEngine;

public class NoteUIController : MonoBehaviour
{
    public static NoteUIController instance;

    public GameObject noteCanvas;
    public TMP_Text noteUIText;
    private GameObject lastNoteObject;

    private PuzzleAudioHelper audioHelper;

    private void Start()
    {
        audioHelper = GetComponent<PuzzleAudioHelper>();
    }

    void Awake()
    {
        instance = this;
    }

    public void ShowNote(string content, GameObject noteObject)
    {
        lastNoteObject = noteObject;
        noteUIText.text = content;
        noteCanvas.SetActive(true);

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        FindFirstObjectByType<PlayerController>().canLook = false;
    }

    public void CloseNote()
    {
        Debug.Log("Fechando nota...");

        if (audioHelper != null)
            audioHelper.PlayCloseSound();
        else
            Debug.LogWarning("audioHelper está NULL!");

        noteCanvas.SetActive(false);

        
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        FindFirstObjectByType<PlayerController>().canLook = true;

        if (lastNoteObject != null)
        {
            var notePickup = lastNoteObject.GetComponent<NotePickup>();
            if (notePickup != null)
            {
                NotesManager.instance.MarkNoteCollected(notePickup.noteOrder);
                Destroy(lastNoteObject);
            }

            lastNoteObject = null;
        }
    }
}
