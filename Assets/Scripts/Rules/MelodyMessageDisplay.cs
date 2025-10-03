using UnityEngine;
using TMPro;

public class MelodyMessageDisplay : MonoBehaviour
{
    public GameObject painelMelodyDialogo; // painel que aparece
    public TextMeshProUGUI textoMelodyDialogo; // onde o texto aparece

    [TextArea]
    public string[] frasesPossiveis = {
        "MELODY_01",
        "MELODY_02",
        "MELODY_03",
        "MELODY_04",
        "MELODY_05"
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
            string chave = frasesPossiveis[Random.Range(0, frasesPossiveis.Length)];
            string fraseTraduzida = LocalizationManager.Instance.GetText(chave);

            Debug.Log($"Chave: {chave} | Tradução: {fraseTraduzida}");

            textoMelodyDialogo.text = fraseTraduzida;
            painelMelodyDialogo.SetActive(true);
            CancelInvoke();
            Invoke(nameof(EsconderMensagem), tempoDeExibicao);
        }
    }

    void EsconderMensagem()
    {
        painelMelodyDialogo.SetActive(false);
    }
}
