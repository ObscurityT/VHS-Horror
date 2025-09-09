using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Referências")]
    public GameObject legendaBox;            
    public TextMeshProUGUI legendaText;

    [Header("Settings")]
    public float screenTime = 3f;
    public float fadeDuration = 0.15f;

    private Coroutine dialogoAtual;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        var cg = legendaBox.GetComponent<CanvasGroup>();
        if (!cg) cg = legendaBox.AddComponent<CanvasGroup>();
        canvasGroup = cg; 
        canvasGroup.alpha = 0f;
        legendaBox.SetActive(false);

        legendaText.textWrappingMode = TextWrappingModes.Normal;
        legendaText.overflowMode = TextOverflowModes.Overflow;
        legendaText.maxVisibleCharacters = int.MaxValue;

        var le = legendaText.GetComponent<LayoutElement>();
        le.preferredWidth = -1f;    
        le.flexibleWidth = 0f;
        le.minWidth = 0f;
        //if (!le) le = legendaText.gameObject.AddComponent<LayoutElement>();
    }

    public void ShowMessage(string mensagem, float? customTime = null)
    {
        if (dialogoAtual != null) StopCoroutine(dialogoAtual);
        dialogoAtual = StartCoroutine(ShowMessageCoroutine(mensagem, customTime ?? screenTime));
    }

    private IEnumerator ShowMessageCoroutine(string mensagem, float timeOnScreen)
    {
        legendaBox.SetActive(true);
        legendaText.text = mensagem;

        
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 0f, 1f, fadeDuration));

        
        yield return new WaitForSeconds(timeOnScreen);

        
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
