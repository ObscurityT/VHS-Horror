using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("NomeDaCenaDoJogo");  // Coloque aqui o nome da sua cena
    }

    public void Options()
    {
        Debug.Log("Open options");
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Para funcionar no editor
#endif
    }
}
