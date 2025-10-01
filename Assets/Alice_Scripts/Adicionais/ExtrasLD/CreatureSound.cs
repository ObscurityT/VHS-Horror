using UnityEngine;

public class CreatureSound : MonoBehaviour
{
    [Header("Som da criatura")]
    public AudioClip soundClip;

    [Range(0f, 1f)]
    public float volume = 1f;       // controla volume
    public bool loop = false;       // se quer que o som repita

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = soundClip;
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
        audioSource.loop = loop;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Stop();
        }
    }
}
