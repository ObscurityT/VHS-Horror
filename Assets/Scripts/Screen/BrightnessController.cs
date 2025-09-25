using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class BrightnessController : MonoBehaviour
{
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Volume globalVolume;

    private ColorAdjustments colorAdjustments;

    private const string BRIGHTNESS_KEY = "BrightnessValue";

    void Start()
    {
        if (globalVolume.profile.TryGet(out colorAdjustments))
        {
            float savedValue = PlayerPrefs.GetFloat(BRIGHTNESS_KEY, 0.5f);
            brightnessSlider.value = savedValue;
            brightnessSlider.onValueChanged.AddListener(HandleBrightnessChange);
            HandleBrightnessChange(savedValue);
        }
    }

    private void HandleBrightnessChange(float value)
    {
        colorAdjustments.postExposure.value = Mathf.Lerp(-2f, 2f, value);

        PlayerPrefs.SetFloat(BRIGHTNESS_KEY, value);
        PlayerPrefs.Save();
    }
}
