using UnityEngine;

public class SoundTest : MonoBehaviour
{

    //programador noob ( ﾉ ﾟｰﾟ)ﾉ
    public AudioClip soundClip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Tocando som");
            audioSource.PlayOneShot(soundClip);
        }
    }
}