using UnityEngine;

public class TriggerQuadro : MonoBehaviour
{
    public GameObject quadroPendurado;  // Quadro na parede
    public GameObject quadroNoChao;     // Quadro caído
    public AudioSource audioQuadro;     // Som do quadro caindo

    private bool ativado = false;       // Evita tocar múltiplas vezes

    private void OnTriggerEnter(Collider other)
    {
        if (!ativado && other.CompareTag("Player")) // Certifique que o jogador tem a tag "Player"
        {
            ativado = true;

            // Toca o som
            audioQuadro.Play();

            // Troca os quadros
            quadroPendurado.SetActive(false);
            quadroNoChao.SetActive(true);
        }
    }
}
