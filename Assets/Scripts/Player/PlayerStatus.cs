using AudioSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public string sfxRespiracao;
    public string sfxMurmurios;
    public string sfxGrito;
    public string sfxVozInterna;
    public string sfxGameOver;

    [Header("Post Processing")]
    public Volume postProcessVolume;
    private Vignette vignette;

    [Header("Insanity Effects")]
    public Volume insanityVolume;

    [Header("Sanity")]
    public Slider sanitySlider;
    public int maxSanity = 5;
    private int currentSanity;
    private int lastStage = 5;

    [Header("Alucinação")]
    public GameObject alucinacaoPrefab;

    [Header("Door")]
    public string lastDoorID = "";

       void Start()
    {
        currentSanity = maxSanity;
        UpdateSanityUI();    
                
    }

    public void DecreaseSanity(int amount = 1)
    {
        if (amount <= 0) return;

        currentSanity -= amount;
        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);
        UpdateSanityUI();
        CheckSanityMilestones();

        if (currentSanity <= 0)
        {
            Debug.Log("Adeus"); //death effect next

            ScreenFade fade = FindFirstObjectByType<ScreenFade>();
            AudioManager.Instance.PlaySFX("gameover");
            if (fade != null)
            {
                if (fade.blackScreen != null)
                    fade.blackScreen.rectTransform.localScale = Vector3.one;

                fade.StartFade(); 
            }
            else
            {
                Debug.LogWarning("Nenhum ScreenFade encontrado!");
            }
        }
    }

    public float GetCurrentSanity()
    { return currentSanity; }

    void UpdateSanityUI()
    {
        if (sanitySlider != null)
        {
            sanitySlider.maxValue = maxSanity;
            sanitySlider.value = currentSanity;
        }
    }

    public void CheckSanityMilestones()
    {

        if (currentSanity < lastStage)
        {
            switch (currentSanity)
            {
                case 4:
                    AudioManager.Instance.PlaySFX(sfxRespiracao);
                    StartCoroutine(BlendInsanity(0.2f, 1f));
                    break;
                case 3:
                    AudioManager.Instance.PlaySFX(sfxMurmurios);
                    StartCoroutine(BlendInsanity(0.45f, 1f));
                    break;
                case 2:
                    StartCoroutine(CameraDistortionRoutine());
                    AudioManager.Instance.PlaySFX(sfxGrito);
                    StartCoroutine(BlendInsanity(0.7f, 1f));
                    break;
                case 1:
                    SpawnHallucination();
                    AudioManager.Instance.PlaySFX(sfxVozInterna);
                    if (insanityVolume != null)
                    insanityVolume.gameObject.SetActive(true);
                    
                    ScreenFade fade = FindFirstObjectByType<ScreenFade>();
                    if (fade != null)
                        StartCoroutine(BlendInsanity(1.0f, 0.5f));
                    break;
                case 0:
                    AudioManager.Instance.PlaySFX(sfxGameOver);
                    break;
            }
            lastStage = currentSanity;
        }
    }

    private IEnumerator CameraDistortionRoutine()
    {
        Camera mainCam = Camera.main;
        if (mainCam == null) yield break;

        float originalFOV = mainCam.fieldOfView;
        float targetFOV = originalFOV + 15f;

        float duration = 0.5f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            mainCam.fieldOfView = Mathf.Lerp(originalFOV, targetFOV, t / duration);
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            mainCam.fieldOfView = Mathf.Lerp(targetFOV, originalFOV, t / duration);
            yield return null;
        }

        mainCam.fieldOfView = originalFOV;
    }

    private void SpawnHallucination()
    {
        if (alucinacaoPrefab == null) return;

        Vector3 spawnPosition = transform.position + transform.forward * 2f + Vector3.right * Random.Range(-1f, 1f);
        Quaternion rotation = Quaternion.LookRotation(-transform.forward); 

        Instantiate(alucinacaoPrefab, spawnPosition, rotation);
    }

    IEnumerator BlendInsanity(float targetWeight, float duration)
    {
        float start = insanityVolume.weight;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float k = t / duration;
            insanityVolume.weight = Mathf.Lerp(start, targetWeight, k);
            yield return null;
        }
        insanityVolume.weight = targetWeight;
    }
}
