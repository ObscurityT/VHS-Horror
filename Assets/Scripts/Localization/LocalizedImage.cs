using UnityEngine;
using UnityEngine.UI;

public class LocalizedImage : MonoBehaviour
{
    public string key; 
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();

        if (LocalizationManager.Instance != null)
        {
            LocalizationManager.Instance.OnLanguageChanged += UpdateSprite;
            UpdateSprite(); 
        }
        else
        {
            Debug.LogWarning($"[LocalizedImage] LocalizationManager.Instance ainda está NULL no Start de '{gameObject.name}'");
        }
    }

    void OnEnable()
    {
        if (LocalizationManager.Instance != null)
        {
            UpdateSprite();
        }
    }


    void OnDestroy()
    {
        if (LocalizationManager.Instance != null)
            LocalizationManager.Instance.OnLanguageChanged -= UpdateSprite;
    }

    public void UpdateSprite()
    {
        Sprite localizedSprite = LocalizationManager.Instance.GetSprite(key);
        if (localizedSprite != null)
        {
            image.sprite = localizedSprite;
            Debug.Log($"[LocalizedImage] Sprite atualizado: {localizedSprite.name}");
        }
        else
        {
            Debug.LogWarning($"[LocalizedImage] Sprite não encontrado para key '{key}'");
        }
    }
}
