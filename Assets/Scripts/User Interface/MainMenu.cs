using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject MenuPanel;
    [SerializeField]
    private GameObject SettingsPanel;


    public string sceneToLoad;

    public GameObject dataPersistencePrefab;

    void Awake()
    {
        if (FindFirstObjectByType<DataPersistenceManager>() == null)
        {
            Instantiate(dataPersistencePrefab); 
        }
    }

    public void StartGame()
    {
        if (DataPersistenceManager.instance != null)
            DataPersistenceManager.instance.NewGame(new GameData());

        // Encontra e destrói a música do menu antes de trocar de cena
        var menuMusic = GameObject.FindWithTag("MenuMusic");
        if (menuMusic != null)
            Destroy(menuMusic);

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Continue()
    {

        if (DataPersistenceManager.instance == null) { StartGame(); return; }

        var data = DataPersistenceManager.instance.CurrentGameData;
        if (data == null) { StartGame(); return; }

        var menuMusic = GameObject.FindWithTag("MenuMusic");
        if (menuMusic != null)
            Destroy(menuMusic);

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Options()
    {
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        SettingsPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#endif
    }


}
