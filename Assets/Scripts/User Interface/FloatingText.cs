using System.Collections;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float floatAmount = 0.2f;
    private LocalizedText localizedText;


    [Header("Fade")]
    public float fadeDuration = 1f;

    private Vector3 startPos;
    private TMP_Text textComponent;
    private Coroutine currentFade;

    void Start()
    {
        localizedText = GetComponent<LocalizedText>();
        startPos = transform.localPosition;
        textComponent = GetComponent<TMP_Text>();

        if (textComponent != null)
        {
            Color c = textComponent.color;
            c.a = 1;
            textComponent.color = c;
        }
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        transform.localPosition = startPos + new Vector3(0, y, 0);
    }

    public void ShowText()
    {
        if (localizedText != null)
            localizedText.UpdateText();

        if (currentFade != null) StopCoroutine(currentFade);
        currentFade = StartCoroutine(FadeTextAlpha(1f));
    }

    public void HideText()
    {
        Debug.Log("HideText()");
        if (currentFade != null) StopCoroutine(currentFade);
        currentFade = StartCoroutine(FadeTextAlpha(0f));
    }

    private IEnumerator FadeTextAlpha(float targetAlpha)
    {
        Color startColor = textComponent.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            textComponent.color = Color.Lerp(startColor, endColor, elapsed / fadeDuration);
            yield return null;
        }

        textComponent.color = endColor;
    }
}
