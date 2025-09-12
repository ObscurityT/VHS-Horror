using UnityEngine;
using UnityEngine.UI;

public class BrightnessMenu : MonoBehaviour
{
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Image brightnessOverlay; 

    private const string BRIGHTNESS_KEY = "BrightnessValue";

    [Range(0f, 1f)][SerializeField] private float maxDarkAlpha = 0.5f;
    [Range(0f, 1f)][SerializeField] private float maxLightAlpha = 0.5f;

    private void Start()
    {
        float savedValue = PlayerPrefs.GetFloat(BRIGHTNESS_KEY, 0.5f);
        brightnessSlider.value = savedValue;

        brightnessSlider.onValueChanged.AddListener(HandleBrightnessChange);
        HandleBrightnessChange(savedValue);
    }

    private void HandleBrightnessChange(float value)
    {
        PlayerPrefs.SetFloat(BRIGHTNESS_KEY, value);
        PlayerPrefs.Save();

        if (brightnessOverlay != null)
        {
            if (value < 0.5f)
            {
                float t = 1f - (value / 0.5f);
                float alpha = Mathf.Lerp(0f, maxDarkAlpha, t);
                brightnessOverlay.color = new Color(0f, 0f, 0f, alpha);
            }
            else if (value > 0.5f)
            {
                float t = (value - 0.5f) / 0.5f;
                float alpha = Mathf.Lerp(0f, maxLightAlpha, t);
                brightnessOverlay.color = new Color(1f, 1f, 1f, alpha);
            }
            else
            {
                brightnessOverlay.color = new Color(0f, 0f, 0f, 0f);
            }
        }
    }
}
