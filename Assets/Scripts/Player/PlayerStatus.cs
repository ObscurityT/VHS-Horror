using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [Header("Sanity")]
    public Slider sanitySlider;
    public float maxSanity = 100f;
    private float currentSanity;

    [Header("Door")]
    public string lastDoorID = "";

       void Start()
    {
        currentSanity = maxSanity;
        UpdateSanityUI();

    }

    public void DecreaseSanity(float amount)
    { 
        currentSanity -= amount;
        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);
        UpdateSanityUI();

        if(currentSanity <= 0)
        {
            Debug.Log("Adeus"); //death effect next

            ScreenFade fade = FindFirstObjectByType<ScreenFade>();
            if (fade != null)
            {
                fade.StartFade();
            }
            else
            {
                Debug.LogWarning("Nenhum ScreenFade encontrado na cena!");
            }
        }
    }

    void UpdateSanityUI()
    {
        if (sanitySlider != null)
        {
            sanitySlider.value = currentSanity;
        }
    }
}
