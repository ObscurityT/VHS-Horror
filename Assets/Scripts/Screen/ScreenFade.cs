using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    public Image blackScreen;
    public float fadeSpeed = 1.5f;
    private bool fading = false;

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
            }
        }
    }

    public void StartFade()
    {
        fading = true;
    }
}
