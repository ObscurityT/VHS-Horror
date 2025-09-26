using SaveSystem;
using TMPro; 
using UnityEngine;

public class TimerOverlay : MonoBehaviour, IDataPersistence
{
    [Header("UI")]
    public TextMeshProUGUI timerText;

    [Header("Behaviour")]
    public bool isPaused = false;

    private float elapsedTime = 0f;


    void Update()
    {
        if (isPaused) return;

        elapsedTime += Time.deltaTime;

        int hours = Mathf.FloorToInt(elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        if (timerText != null)
            timerText.text = $"{hours:00}:{minutes:00}:{seconds:00}";
    }

    // ===== IDataPersistence =====
    public void LoadData(GameData data)
    {
        elapsedTime = data.overlayElapsedSeconds;
        Debug.Log("[TIMER] Carregado tempo: " + elapsedTime);

        // atualiza a UI imediatamente
        int hours = Mathf.FloorToInt(elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        if (timerText != null)
            timerText.text = $"{hours:00}:{minutes:00}:{seconds:00}";
        else
            Debug.LogWarning("[TIMER] timerText está nulo no LoadData!");

        if (data == null)
        {
            Debug.LogError("[TIMER] GameData está NULO no LoadData");
            return;
        }
    }

    public void SaveData(GameData data)
    {
        if (data == null)
        {
            Debug.LogWarning("[TIMER] GameData está nulo no SaveData!");
            return;
        }
        Debug.Log("[TIMER] Salvando tempo: " + elapsedTime);
        data.overlayElapsedSeconds = elapsedTime;
    }
}
