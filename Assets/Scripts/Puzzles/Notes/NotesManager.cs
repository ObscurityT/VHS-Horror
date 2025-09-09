using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NotesManager : MonoBehaviour
{
    public static NotesManager instance;

    private Dictionary<int, string> noteTexts = new Dictionary<int, string>();

    private List<int> collectedNoteOrdersList = new List<int>();
    
    private HashSet<int> collectedNoteOrdersSet = new HashSet<int>();

    public int totalNotes = 5;
    [Header("Event when all notes are collected")]
    public UnityEvent onAllNotesCollected;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    public void RegisterNoteText(int order, string text)
    {
        if (!noteTexts.ContainsKey(order))
            noteTexts.Add(order, text);
    }

    public string GetNoteText(int order)
    {
        if (noteTexts.TryGetValue(order, out var text))
            return text;
        return "";
    }

    public void MarkNoteCollected(int noteOrder)
    {
        if (!collectedNoteOrdersSet.Contains(noteOrder))
        {
            collectedNoteOrdersSet.Add(noteOrder);
            collectedNoteOrdersList.Add(noteOrder);

            Debug.Log($"Nota {noteOrder} coletada ({collectedNoteOrdersList.Count}/{totalNotes})");

            if (collectedNoteOrdersList.Count >= totalNotes)
            {
                Debug.Log("All notes are collected");
                onAllNotesCollected?.Invoke(); 
            }
        }
    }

    public List<int> GetCollectedNotesInOrder()
    {
        return collectedNoteOrdersList;
    }
}
