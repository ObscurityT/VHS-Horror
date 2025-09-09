using TMPro;
using UnityEngine;

public class LanguageSwitcher : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    void Start()
    {
        dropdown.onValueChanged.AddListener(OnLanguageSelected);
    }

    void OnLanguageSelected(int index)
    {
        SystemLanguage selectedLanguage = SystemLanguage.English;

        switch (index)
        {
            case 0: selectedLanguage = SystemLanguage.Portuguese; break;
            case 1: selectedLanguage = SystemLanguage.English; break;
        }

        LocalizationManager.Instance.SetLanguage(selectedLanguage);
    }
}
