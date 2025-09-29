using UnityEngine;

public class DebugButtonTest : MonoBehaviour
{
    [Header("UI de Teste")]
    public GameObject painelDeTeste;

    private bool painelAtivo = false;

    void Start()
    {
        Debug.Log("[DEBUG] Script iniciado");
        if (painelDeTeste != null)
            painelDeTeste.SetActive(false); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            painelAtivo = !painelAtivo;

            if (painelDeTeste != null)
                painelDeTeste.SetActive(painelAtivo);

            
            Cursor.visible = painelAtivo;
            Cursor.lockState = painelAtivo ? CursorLockMode.None : CursorLockMode.Locked;

            // Pausa o jogo enquanto o painel está ativo
            Time.timeScale = painelAtivo ? 0f : 1f;

            Debug.Log("[DEBUG UI] Painel de teste " + (painelAtivo ? "ABERTO" : "FECHADO"));
        }
    }
}
