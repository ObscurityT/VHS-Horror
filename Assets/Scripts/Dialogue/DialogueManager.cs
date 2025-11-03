using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // Singleton seguro — evita que outro script altere o Instance
    public static DialogueManager Instance { get; private set; }

    [Header("Referências")]
    [Tooltip("Caixa onde o texto aparecerá.")]
    public GameObject legendaBox;

    [Tooltip("Texto da legenda (TMP)")]
    public TextMeshProUGUI legendaText;

    [Header("Configurações")]
    [Tooltip("Tempo que o texto permanece na tela (em segundos).")]
    public float screenTime = 3f;

    [Tooltip("Duração do fade in/out (em segundos).")]
    public float fadeDuration = 0.15f;

    private Coroutine dialogoAtual;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        // Singleton seguro
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // Se quiser manter o diálogo entre cenas, descomente a linha abaixo
        // DontDestroyOnLoad(gameObject);

        // Garante que legendaBox tenha um CanvasGroup para o fade
        var cg = legendaBox.GetComponent<CanvasGroup>();
        if (!cg) cg = legendaBox.AddComponent<CanvasGroup>();
        canvasGroup = cg;
        canvasGroup.alpha = 0f;
        legendaBox.SetActive(false);

        // Configura o TextMeshPro
        legendaText.textWrappingMode = TextWrappingModes.Normal;
        legendaText.overflowMode = TextOverflowModes.Overflow;
        legendaText.maxVisibleCharacters = int.MaxValue;

        // Ajusta LayoutElement (se existir)
        var le = legendaText.GetComponent<LayoutElement>();
        if (le)
        {
            le.preferredWidth = -1f;
            le.flexibleWidth = 0f;
            le.minWidth = 0f;
        }
    }

    public void ShowMessage(string mensagem, float? customTime = null)
    {
        if (dialogoAtual != null)
            StopCoroutine(dialogoAtual);

        dialogoAtual = StartCoroutine(ShowMessageCoroutine(mensagem, customTime ?? screenTime));
    }

    private IEnumerator ShowMessageCoroutine(string mensagem, float timeOnScreen)
    {
        legendaBox.SetActive(true);
        legendaText.text = mensagem;

        // Fade in
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 0f, 1f, fadeDuration));

        // Mantém o texto na tela
        yield return new WaitForSeconds(timeOnScreen);

        // Fade out
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 1f, 0f, fadeDuration));

        legendaBox.SetActive(false);
        dialogoAtual = null;
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float from, float to, float duration)
    {
        float t = 0f;
        cg.alpha = from;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            cg.alpha = Mathf.Lerp(from, to, t / duration);
            yield return null;
        }

        cg.alpha = to;
    }
}
