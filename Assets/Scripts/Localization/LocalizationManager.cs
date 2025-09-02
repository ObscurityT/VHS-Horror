using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    public LocalizationData[] availableLanguages;
    private Dictionary<string, string> currentDictionary;
    public SystemLanguage currentLanguage = SystemLanguage.Portuguese;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetLanguage(currentLanguage);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLanguage(SystemLanguage lang)
    {
        currentLanguage = lang;
        var data = GetDataForLanguage(lang);

        currentDictionary = new Dictionary<string, string>();
        foreach (var entry in data.entries)
        {
            currentDictionary[entry.key] = entry.value;
        }
    }

    LocalizationData GetDataForLanguage(SystemLanguage lang)
    {
        foreach (var d in availableLanguages)
            if (d.language == lang)
                return d;
        return availableLanguages[0]; // fallback
    }

    public string GetText(string key)
    {
        if (currentDictionary.ContainsKey(key))
            return currentDictionary[key];
        return $"#{key}#";
    }
}
