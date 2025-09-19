using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    public Image blackScreen;
    public float fadeSpeed = 1.5f;
    private bool fading = false;

    public event Action OnFadeFinished;



    private void Update()
    {
        if (fading)
        {
            Color color = blackScreen.color;
            color.a = Mathf.MoveTowards(color.a, 1f, fadeSpeed * Time.deltaTime);
            blackScreen.color = color;

            if (color.a >= 1f)
            {
                fading = false;
                Debug.Log("You're Dead");
                OnFadeFinished?.Invoke();
            }
        }
    }

    public void StartFade()
    {
        fading = true;
    }

    public IEnumerator CollapseTVEffect(float duration)
    {
        if (blackScreen == null) yield break;

        RectTransform rt = blackScreen.GetComponent<RectTransform>();
        Vector3 originalScale = rt.localScale;
        Vector3 collapsedScale = new Vector3(1f, 0f, 1f);

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float lerp = Mathf.SmoothStep(0f, 1f, t / duration);
            rt.localScale = Vector3.Lerp(originalScale, collapsedScale, lerp);
            yield return null;
        }

        rt.localScale = collapsedScale;
    }
}
