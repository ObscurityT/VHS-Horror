using UnityEngine;
using TMPro;

public class MelodyMessageDisplay : MonoBehaviour
{
    public GameObject painelMelodyDialogo; // painel que aparece
    public TextMeshProUGUI textoMelodyDialogo; // onde o texto aparece

    [TextArea]
    public string[] frasesPossiveis = {
        "Você não devia ter se movido...",
        "Ele está ouvindo.",
        "A música conhece você.",
        "Se mexer agora seria um erro.",
        "O castelo sente seu medo."
    };

    public float tempoDeExibicao = 4f;

    private void OnEnable()
    {
        GameEvents.OnMelodyStart += MostrarMensagem;
    }

    private void OnDisable()
    {
        GameEvents.OnMelodyStart -= MostrarMensagem;
    }

    void MostrarMensagem()
    {
        if (painelMelodyDialogo != null && textoMelodyDialogo != null)
        {
            string frase = frasesPossiveis[Random.Range(0, frasesPossiveis.Length)];
            textoMelodyDialogo.text = frase;
            painelMelodyDialogo.SetActive(true);
            CancelInvoke(); // evita sobreposição
            Invoke(nameof(EsconderMensagem), tempoDeExibicao);
        }
    }

    void EsconderMensagem()
    {
        painelMelodyDialogo.SetActive(false);
    }
}
