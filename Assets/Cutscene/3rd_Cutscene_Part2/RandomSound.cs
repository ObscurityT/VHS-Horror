using UnityEngine;

public class RandomSound : MonoBehaviour
{
    public AudioSource audioSource;   // arrasta o AudioSource aqui
    public AudioClip[] sounds;        // podes colocar vários sons, ele escolhe 1 aleatório
    public float minDelay = 600f;     // 10 minutos
    public float maxDelay = 1200f;    // 20 minutos

    private float timer;

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            PlayRandomSound();
            ResetTimer();
        }
    }

    void PlayRandomSound()
    {
        if (sounds.Length == 0 || audioSource == null) return;

        AudioClip clip = sounds[Random.Range(0, sounds.Length)];
        audioSource.clip = clip;
        audioSource.Play();
    }

    void ResetTimer()
    {
        timer = Random.Range(minDelay, maxDelay);
    }
}
