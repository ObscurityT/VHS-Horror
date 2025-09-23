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

        [SerializeField] private AudioClip startMusic;
        [SerializeField] private bool playOnStart = true;

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

        void Start()
        {
            if (musicSource == null)
            {
                Debug.LogError("[AudioManager] Music Source não está atribuído no Inspector!");
                return;
            }

            if (musicClips.Count > 0 && playOnStart)
            {
                PlayMusic(musicClips[0].name);
            }
        }

        public void PlaySFX(string name)
        {
            if (sfxDict.ContainsKey(name))
            {
                Debug.Log("[AudioManager] Tocando SFX: " + name);
                // sfxSource.spatialBlend = 0f; // forÃ§a som 2D
                // sfxSource.volume = 1f;       // forÃ§a volume mÃ¡ximo
                sfxSource.PlayOneShot(sfxDict[name]);
            }
            else
                Debug.LogWarning("SFX nï¿½o encontrado: " + name);
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
                Debug.LogWarning("Mï¿½sica nï¿½o encontrada: " + name);
            }
        }

        public void StopMusic()
        {
            musicSource.Stop();
        }

        public List<string> GetSFXNames()
        {
            List<string> names = new();
            foreach (var clip in sfxClips)
                if (clip != null)
                    names.Add(clip.name);
            return names;
        }

        public List<string> GetMusicNames()
        {
            List<string> names = new();
            foreach (var clip in musicClips)
                if (clip != null)
                    names.Add(clip.name);
            return names;
        }

        public void PlaySFX(AudioClip clip)
        {
            if (clip != null)
                sfxSource.PlayOneShot(clip);
        }

        public void PlayMusic(AudioClip music)
        {
            if (musicSource.clip != music)
            {
                musicSource.clip = music;
                musicSource.Play();
            }
        }
    }
}