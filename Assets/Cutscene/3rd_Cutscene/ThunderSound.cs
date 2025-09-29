using UnityEngine;
using System.Collections;

public class ThunderSound : MonoBehaviour
{
    [Header("Som do trovão")]
    public AudioSource thunderAudio;
    public float minDelay = 10f;
    public float maxDelay = 30f;

    [Header("Flash da luz")]
    public Light flashLight;
    public float flashIntensity = 8f;
    public float flashDuration = 0.2f;

    [Range(0f, 1f)]
    public float chanceDeSom = 0.7f; // 70% de chance de haver som com relâmpago

    private float originalIntensity;

    void Start()
    {
        if (flashLight != null)
            originalIntensity = flashLight.intensity;

        StartCoroutine(EventoAleatorio());
    }

    IEnumerator EventoAleatorio()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            // Relâmpago (sempre acontece)
            if (flashLight != null)
                StartCoroutine(FlashRelampago());

            // Chance de haver som também
            if (thunderAudio != null && Random.value < chanceDeSom)
            {
                // Opcional: atraso para simular distância do trovão
                float somDelay = Random.Range(0.2f, 2f);
                thunderAudio.PlayDelayed(somDelay);
            }
        }
    }

    IEnumerator FlashRelampago()
    {
        // Podemos fazer múltiplos flashes para parecer mais real
        int flashes = Random.Range(1, 4);

        for (int i = 0; i < flashes; i++)
        {
            flashLight.intensity = flashIntensity;
            yield return new WaitForSeconds(flashDuration);

            flashLight.intensity = originalIntensity;
            yield return new WaitForSeconds(Random.Range(0.05f, 0.2f)); // pausa curta
        }
    }
}
