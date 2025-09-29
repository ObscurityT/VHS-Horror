using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject panelPause;
    public GameObject panelSettings;
    public GameObject crosshair;
    public GameObject cameraOverlay;

    private PlayerController playerController;


    private bool isPaused = false;

    void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        Debug.Log("Botão Resume foi clicado!");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        panelPause.SetActive(false);
        crosshair.SetActive(true);
        cameraOverlay.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;

        if (playerController != null)
            playerController.canLook = true;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        panelPause.SetActive(true);
        crosshair.SetActive(false);
        cameraOverlay.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;

        if (playerController != null)
            playerController.canLook = false;
    }

    public void OpenOptions()
    {
        panelPause.SetActive(false);
        panelSettings.SetActive(true);
    }

    public void BackToPauseMenu()
    {
        panelPause.SetActive(true);
        panelSettings.SetActive(false);
    }

    public void LoadMainMenu()
    {
        if (DataPersistenceManager.instance != null)
            DataPersistenceManager.instance.SaveGame();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        StartCoroutine(SaveAndQuitCoroutine());
    }

    private System.Collections.IEnumerator SaveAndQuitCoroutine()
    {
        if (DataPersistenceManager.instance != null)
        {
            Debug.Log("[QUIT] Salvando jogo...");
            DataPersistenceManager.instance.SaveGame();
        }

        // Espera 0.2 segundos para garantir que o JSON foi escrito
        yield return new WaitForSecondsRealtime(0.2f);

        Debug.Log("[QUIT] Encerrando jogo");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

}
