using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LocalizedImageButton : MonoBehaviour
{
    public string keyNormal;
    public string keyHighlighted;
    public string keyPressed;
    public string keySelected;
    public string keyDisabled;

    private Button button;
    private Image targetImage;

    void Start()
    {
        if (LocalizationManager.Instance == null)
        {
            Debug.LogError("[LocalizedImageButton] LocalizationManager ainda é null no Start!");
            return;
        }

        button = GetComponent<Button>();
        targetImage = button.targetGraphic as Image;

        UpdateSprites();

        LocalizationManager.Instance.OnLanguageChanged += UpdateSprites;
    }

    void OnDestroy()
    {
        if (LocalizationManager.Instance != null)
            LocalizationManager.Instance.OnLanguageChanged -= UpdateSprites;
    }

    void UpdateSprites()
    {
        Debug.Log($"[LocalizedImageButton] Atualizando sprites para o botão: {gameObject.name}");

        Sprite normal = LocalizationManager.Instance.GetSprite(keyNormal);
        Sprite highlighted = LocalizationManager.Instance.GetSprite(keyHighlighted);
        Sprite pressed = LocalizationManager.Instance.GetSprite(keyPressed);
        Sprite selected = LocalizationManager.Instance.GetSprite(keySelected);
        Sprite disabled = LocalizationManager.Instance.GetSprite(keyDisabled);

        Debug.Log($"Normal: {normal?.name}");
        Debug.Log($"Highlighted: {highlighted?.name}");
        Debug.Log($"Pressed: {pressed?.name}");
        Debug.Log($"Selected: {selected?.name}");
        Debug.Log($"Disabled: {disabled?.name}");

        if (normal != null) targetImage.sprite = normal;

        SpriteState spriteState = new SpriteState
        {
            highlightedSprite = highlighted,
            pressedSprite = pressed,
            selectedSprite = selected,
            disabledSprite = disabled
        };

        button.spriteState = spriteState;
    }
}
