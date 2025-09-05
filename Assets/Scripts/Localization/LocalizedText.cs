using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour
{
    public string key;
    private TMP_Text textComponent;

    void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        UpdateText();

        if (LocalizationManager.Instance != null)
            LocalizationManager.Instance.OnLanguageChanged += UpdateText;
    }

    void OnDestroy()
    {
        if (LocalizationManager.Instance != null)
            LocalizationManager.Instance.OnLanguageChanged -= UpdateText;
    }

    public void UpdateText()
    {
        if (LocalizationManager.Instance != null)
            textComponent.text = LocalizationManager.Instance.GetText(key);
    }
}