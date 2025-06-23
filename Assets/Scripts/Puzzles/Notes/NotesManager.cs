using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NotesManager : MonoBehaviour
{
    public static NotesManager instance;

    private HashSet<int> collectedNoteOrders = new HashSet<int>();
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

    public void MarkNoteCollected(int noteOrder)
    {
        if (!collectedNoteOrders.Contains(noteOrder))
        {
            collectedNoteOrders.Add(noteOrder);
            Debug.Log($"Nota {noteOrder} coletada ({collectedNoteOrders.Count}/{totalNotes})");

            if (collectedNoteOrders.Count >= totalNotes)
            {
                Debug.Log("All notes are collected");
                onAllNotesCollected?.Invoke(); // Gatilho para liberar puzzle
            }
        }
    }
}
