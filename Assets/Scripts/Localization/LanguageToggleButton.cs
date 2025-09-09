using UnityEngine;

public class LanguageToggleButton : MonoBehaviour
{
    public void ToggleLanguage()
    {
        var lang = LocalizationManager.Instance.currentLanguage;
        var newLang = lang == SystemLanguage.Portuguese ? SystemLanguage.English : SystemLanguage.Portuguese;
        LocalizationManager.Instance.SetLanguage(newLang);
    }
}
