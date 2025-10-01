using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    public AudioSource audioSource; // arraste o AudioSource aqui no Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // garante que só o player ativa
        {
            if (!audioSource.isPlaying) // opcional: evita sobreposição
            {
                audioSource.Play();
            }
        }
    }
}

