using UnityEngine;

public class RandomLightning : MonoBehaviour
{
    public ParticleSystem lightningEffect; // arrasta o sistema de partículas aqui
    public float minDelay = 2f;  // tempo mínimo entre raios
    public float maxDelay = 8f;  // tempo máximo entre raios

    private float timer;

    void Start()
    {
        ScheduleNextLightning();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            TriggerLightning();
            ScheduleNextLightning();
        }
    }

    void TriggerLightning()
    {
        if (lightningEffect != null)
        {
            lightningEffect.Play();
        }
    }

    void ScheduleNextLightning()
    {
        timer = Random.Range(minDelay, maxDelay);
    }
}
