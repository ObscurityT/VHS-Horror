using System.Collections.Generic;
using UnityEngine;

public class DiaryManager : MonoBehaviour
{
    public static DiaryManager Instance;

    private SortedDictionary<int, string> legendPages = new SortedDictionary<int, string>();
    private SortedDictionary<int, string> rulesPages = new SortedDictionary<int, string>();

    private void Start()
    {
      
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddLegendPage(int pageNumber, string text)
    {
        if (!legendPages.ContainsKey(pageNumber))
            legendPages.Add(pageNumber, text);
    }

    public List<string> GetLegendPages()
    {
        List<string> pages = new List<string>();
        foreach (var key in legendPages.Values)
        {
            pages.Add(LocalizationManager.Instance.GetText(key));
        }
        return pages;
    }

    public List<string> GetRulesPages()
    {
        int i = 1;
        var linhas = new List<string>();
        foreach (var key in rulesPages.Values)
        {
            linhas.Add($"{i}: {LocalizationManager.Instance.GetText(key)}");
            i++;
        }
        string allRules = string.Join("\n", linhas);
        return new List<string> { allRules };
    }

    public void AddRulePage(int ruleNumber, string text)
    {
        if (!rulesPages.ContainsKey(ruleNumber))
            rulesPages.Add(ruleNumber, text);
    }
}
