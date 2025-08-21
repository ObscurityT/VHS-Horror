using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GroundSound
{
    public string groundTag;   // Ex: "Wood", "Rock"
    public AudioClip clip;     // Som correspondente
}

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public float speedThreshold = 0.1f;
    public List<GroundSound> groundSounds = new List<GroundSound>();

    private Rigidbody rb;
    private Dictionary<string, AudioClip> soundDictionary;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        audioSource.loop = true;
        audioSource.playOnAwake = false;

        // Converte lista para dicion�rio para acesso r�pido
        soundDictionary = new Dictionary<string, AudioClip>();
        foreach (var gs in groundSounds)
        {
            if (!soundDictionary.ContainsKey(gs.groundTag))
                soundDictionary.Add(gs.groundTag, gs.clip);
        }
    }

    void Update()
    {
        // Ignora movimento vertical
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        bool isWalking = horizontalVelocity.magnitude > speedThreshold;

        string groundTag = GetGroundTag();

        if (isWalking && groundTag != null && soundDictionary.ContainsKey(groundTag))
        {
            AudioClip desiredClip = soundDictionary[groundTag];

            // Troca de som ou in�cio do som
            if (audioSource.clip != desiredClip)
            {
                audioSource.clip = desiredClip;
                audioSource.pitch = Random.Range(0.95f, 1.05f); // Pitch aleat�rio no in�cio
                audioSource.Play();
            }
            else if (!audioSource.isPlaying)
            {
                audioSource.pitch = Random.Range(0.95f, 1.05f); // Pitch aleat�rio se reiniciar
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }

    string GetGroundTag()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.5f))
        {
            return hit.collider.tag;
        }
        return null;
    }
}
