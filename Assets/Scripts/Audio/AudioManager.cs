using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Audio Sources")]
        public AudioSource sfxSource;
        public AudioSource musicSource;
        private AudioSource audioSource;


        [Header("Audio Clips")]
        public List<AudioClip> sfxClips;
        public List<AudioClip> musicClips;
        public AudioClip soundClip;

        private Dictionary<string, AudioClip> sfxDict;
        private Dictionary<string, AudioClip> musicDict;

        void Start()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = true;
        }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                sfxDict = new Dictionary<string, AudioClip>();
                musicDict = new Dictionary<string, AudioClip>();

                foreach (AudioClip clip in sfxClips)
                    sfxDict[clip.name] = clip;

                foreach (AudioClip clip in musicClips)
                    musicDict[clip.name] = clip;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlaySFX(string name)
        {
            if (sfxDict.ContainsKey(name))
                sfxSource.PlayOneShot(sfxDict[name]);
            else
                Debug.LogWarning("SFX não encontrado: " + name);
        }

        public void PlayMusic(string name, bool loop = true)
        {
            if (musicDict.ContainsKey(name))
            {
                musicSource.clip = musicDict[name];
                musicSource.loop = loop;
                musicSource.Play();
            }
            else
            {
                Debug.LogWarning("Música não encontrada: " + name);
            }
        }

        //programador noob ( ﾉ ﾟｰﾟ)ﾉ
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Tocando som");
                audioSource.PlayOneShot(soundClip);
            }
        }

        public void StopMusic()
        {
            musicSource.Stop();
        }
    }
}