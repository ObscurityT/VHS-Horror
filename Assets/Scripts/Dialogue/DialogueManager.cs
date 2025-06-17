using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Referências")]
    public GameObject legendaBox;            
    public TextMeshProUGUI legendaText;

    [Header("Settings")]
    public float screenTime = 3f;

    private Coroutine dialogoAtual;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        legendaBox.SetActive(false);
    }

    public void ShowMessage(string mensagem)
    {
        if (dialogoAtual != null)
            StopCoroutine(dialogoAtual);

        dialogoAtual = StartCoroutine(ShowMessageCoroutine(mensagem));
    }

    private IEnumerator ShowMessageCoroutine(string mensagem)
    {
        legendaBox.SetActive(true);
        legendaText.text = mensagem;

        yield return new WaitForSeconds(screenTime);
        legendaBox.SetActive(false);
    }
}
