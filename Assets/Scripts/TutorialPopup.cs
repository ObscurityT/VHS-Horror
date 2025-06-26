using UnityEngine;
using static UnityEngine.Rendering.BoolParameter;

public class TutorialPopup : MonoBehaviour
{
    public GameObject tutorialPanel;

    void Start()
    {
        Debug.Log("Tutorial iniciado");
        // Ativa o painel e libera o mouse
        tutorialPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseTutorial()
    {
        // Fecha o painel e trava o mouse de novo
        Debug.Log("Fechando tutorial");
        tutorialPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
