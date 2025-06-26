using System.Collections.Generic;
using UnityEngine;

namespace AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Audio Sources")]
        public AudioSource sfxSource;
        public AudioSource musicSource;

        [Header("Audio Clips")]
        public List<AudioClip> sfxClips;
        public List<AudioClip> musicClips;

        private Dictionary<string, AudioClip> sfxDict;
        private Dictionary<string, AudioClip> musicDict;

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

        public void StopMusic()
        {
            musicSource.Stop();
        }
    }
}