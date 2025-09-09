using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    public event System.Action OnLanguageChanged;
    private Dictionary<string, Sprite> currentSpriteDictionary;

    public LocalizationData[] availableLanguages;
    private Dictionary<string, string> currentDictionary;
    public SystemLanguage currentLanguage = SystemLanguage.Portuguese;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("[LocalizationManager] Inicializado.");
            SetLanguage(currentLanguage);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLanguage(SystemLanguage lang)
    {
        if (availableLanguages == null || availableLanguages.Length == 0)
        {
            Debug.LogError("[LocalizationManager] Nenhum idioma disponível!");
            return;
        }

        currentLanguage = lang;
        var data = GetDataForLanguage(lang);

        currentDictionary = new Dictionary<string, string>();
        currentSpriteDictionary = new Dictionary<string, Sprite>();

        foreach (var entry in data.entries)
        {
            currentDictionary[entry.key] = entry.value;
            currentSpriteDictionary[entry.key] = entry.sprite;
        }

        OnLanguageChanged?.Invoke();
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
    public Sprite GetSprite(string key)
    {
        if (currentSpriteDictionary != null && currentSpriteDictionary.ContainsKey(key))
            return currentSpriteDictionary[key];
        return null;
    }
}
