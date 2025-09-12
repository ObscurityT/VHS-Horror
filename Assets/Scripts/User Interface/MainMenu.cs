using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject MenuPanel;
    [SerializeField]
    private GameObject SettingsPanel;


    public string sceneToLoad;
    
    public void StartGame()
    {
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
