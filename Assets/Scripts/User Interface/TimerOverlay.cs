using UnityEngine;
using TMPro; 

public class TimerOverlay : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float elapsedTime = 0f;
    public bool isPaused = false;

    private const string TIMER_KEY = "OverlayElapsedTime";

    void Start()
    {
        
        if (PlayerPrefs.HasKey(TIMER_KEY))
        {
            elapsedTime = PlayerPrefs.GetFloat(TIMER_KEY, 0f);
        }
    }

    void Update()
    {
        if (!isPaused)
        {
            elapsedTime += Time.deltaTime;
            int hours = Mathf.FloorToInt(elapsedTime / 3600);
            int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);

            timerText.text = $"{hours:00}:{minutes:00}:{seconds:00}";
        }
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat(TIMER_KEY, elapsedTime);
        PlayerPrefs.Save();
    }
}
