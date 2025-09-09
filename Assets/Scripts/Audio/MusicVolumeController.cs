using AudioSystem;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeController : MonoBehaviour
{
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    [SerializeField] private Slider musicSlider;

    private void Awake()
    {
        if (musicSlider == null) musicSlider = GetComponent<Slider>();
        musicSlider.minValue = 0f;
        musicSlider.maxValue = 1f;
    }

    private void OnEnable()
    {
        float saved = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f);
        musicSlider.SetValueWithoutNotify(saved);
        ApplyMusicVolume(saved);
        musicSlider.onValueChanged.AddListener(ApplyMusicVolume);
    }

    private void OnDisable()
    {
        musicSlider.onValueChanged.RemoveListener(ApplyMusicVolume);
        PlayerPrefs.Save();
    }

    private void ApplyMusicVolume(float value)
    {
        if (AudioManager.Instance != null && AudioManager.Instance.musicSource != null)
        {
            AudioManager.Instance.musicSource.volume = value;
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, value);
        }
    }

}
