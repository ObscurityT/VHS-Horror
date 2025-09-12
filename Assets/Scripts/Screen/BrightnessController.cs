using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class BrightnessController : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Volume globalVolume;
    [SerializeField] private Image brightnessOverlay;

    [Header("Limites de Alpha")]
    [Range(0f, 1f)][SerializeField] private float maxDarkAlpha = 0.5f;  
    [Range(0f, 1f)][SerializeField] private float maxLightAlpha = 0.5f;

    private ColorAdjustments colorAdjustments;

    private const string BRIGHTNESS_KEY = "BrightnessValue";


    private void Start()
    {
        if (globalVolume.profile.TryGet(out colorAdjustments))
        {
            float savedValue = PlayerPrefs.GetFloat(BRIGHTNESS_KEY, 0.5f);
            brightnessSlider.onValueChanged.AddListener(HandleBrightnessChange);

            brightnessSlider.value = 0.5f;

            brightnessSlider.value = savedValue;

            HandleBrightnessChange(savedValue);
        }
    }

    private void HandleBrightnessChange(float value)
    {

        PlayerPrefs.SetFloat(BRIGHTNESS_KEY, value);
        PlayerPrefs.Save();


        colorAdjustments.postExposure.value = Mathf.Lerp(-1f, 1f, value);

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
