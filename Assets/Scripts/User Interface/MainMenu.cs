using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string sceneToLoad;
    
    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoad);  
    }

    public void Options()
    {
        Debug.Log("Open options");
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#endif
    }
}
