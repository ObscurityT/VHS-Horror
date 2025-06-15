using System.Collections.Generic;
using UnityEngine;

public class DiaryManager : MonoBehaviour
{
    public static DiaryManager Instance;

    private SortedDictionary<int, string> collectedPages = new SortedDictionary<int, string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPage(int pageNumber, string text)
    {
        if (!collectedPages.ContainsKey(pageNumber))
            collectedPages.Add(pageNumber, text);
    }

    public List<string> GetOrderedPages()
    {
        return new List<string>(collectedPages.Values);
    }
}
