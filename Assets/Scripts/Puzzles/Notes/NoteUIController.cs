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
        noteCanvas.SetActive(false);

        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        var player = FindFirstObjectByType<PlayerController>();
        if (player != null) player.canLook = true;

        if (lastNoteObject == null)
        {
            Debug.LogWarning("lastNoteObject está NULL!");
            return;
        }

        var notePickup = lastNoteObject.GetComponent<NotePickup>();
        if (notePickup == null)
        {
            Debug.LogWarning("NotePickup não encontrado no objeto da nota");
            return;
        }

        Debug.Log($"Nota coletada - Ordem: {notePickup.noteOrder}, Texto: {notePickup.noteText}");

        DiaryManager.Instance.AddLegendPage(notePickup.noteOrder, notePickup.noteText);
        NotesManager.instance.MarkNoteCollected(notePickup.noteOrder);

        Destroy(lastNoteObject.gameObject);
        lastNoteObject = null;

        var diaryUI = FindFirstObjectByType<DiaryTabsUI>();
        if (diaryUI != null && diaryUI.gameObject.activeSelf)
        {
            diaryUI.SelectTab("Legends");
        }

    }
}
