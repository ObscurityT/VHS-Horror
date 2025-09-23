using SaveSystem;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour, IDataPersistence
{
    [Header("Sanity")]
    public Slider sanitySlider;
    public float maxSanity = 100f;
    private float currentSanity;

    [Header("Door")]
    public string lastDoorID = "";

    void Start()
    {
        currentSanity = maxSanity;
        UpdateSanityUI();

    }

    public void DecreaseSanity(float amount)
    {
        currentSanity -= amount;
        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);
        UpdateSanityUI();

        if (currentSanity <= 0)
        {
            Debug.Log("Adeus"); //death effect next
            //salvar o estador ANTES de qualquer alteração de respawn
            if (DataPersistenceManager.instance != null)
            {
                DataPersistenceManager.instance.SaveGame();
            }
            else
            {
                Debug.LogWarning("DataPersistenceManager.instance é null ao salvar na morte.");
            }

            //respawn em runtime pra não modificar o arquivo salvo
            //usar a posição inicial definida em GameData 
            if (DataPersistenceManager.instance != null && DataPersistenceManager.instance.CurrentGameData != null)
            {
                Vector3 spawnPos = DataPersistenceManager.instance.CurrentGameData.playerPosition;
                transform.position = spawnPos;
            }
            else
            {
                Debug.LogWarning("Não foi possível obter posição inicial do DataPersistenceManager.");
            }

            //restaura sanity em runtime pra permitir continuar o jogo
            currentSanity = maxSanity;
            UpdateSanityUI();

            ScreenFade fade = FindFirstObjectByType<ScreenFade>();
            if (fade != null)
            {
                fade.StartFade();
            }
            else
            {
                Debug.LogWarning("Nenhum ScreenFade encontrado na cena!");
            }
        }
    }

    public float GetCurrentSanity()
    { return currentSanity; }

    void UpdateSanityUI()
    {
        if (sanitySlider != null)
        {
            sanitySlider.value = currentSanity;
        }
    }

    public void LoadData(GameData data)
    {
        if (data == null) return;
        //carregar sanity e posição salva quando o jogo carregar
        currentSanity = data.currentSanity;
        lastDoorID = data.lastDoorID;

        //posição definida apenas no caregamento de cena
        transform.position = data.playerPosition;
        UpdateSanityUI();
    }

    public void SaveData(GameData data)
    {
        if (data == null) return;

        //salvar sanity, posição atual e lastDoorID
        data.playerPosition = transform.position;
        data.currentSanity = currentSanity;
        data.lastDoorID = lastDoorID;
    }
}
