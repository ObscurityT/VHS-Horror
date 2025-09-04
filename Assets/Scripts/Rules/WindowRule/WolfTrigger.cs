using System.Data.Common;
using AudioSystem;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class WolfTrigger : MonoBehaviour
{
    [Header("Áudio")]
    public AudioClip wolfClip;
    [Range(0f, 1f)] public float volume = 1f;
 
    [Header("Parâmetros")]
    public string playerTag = "Player";
    public string windowTag = "Window";
    public string closeParameter = "Close"; //aqui vem o nome do parametro que tem no animator do unity na animação de fechar a janela
    public bool useTriggerParameter = true;

    AudioSource audioSource;
    bool triggered = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag(playerTag)) return;

        triggered = true;
        PlayWolf();
        CloseAllWindows();
        
    }

    void PlayWolf()
    {
        audioSource.PlayOneShot(wolfClip, volume);
    }

    void CloseAllWindows()
    {
        var windows = GameObject.FindGameObjectsWithTag(windowTag);
        foreach (var w in windows)
        {
            var animator = w.GetComponent<Animator>();
            if (useTriggerParameter)
            {
                animator.SetTrigger(closeParameter);
            }
            else
            {
                animator.SetBool(closeParameter, true);
            }
        }
    }
}
