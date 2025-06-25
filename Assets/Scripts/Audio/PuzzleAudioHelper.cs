using AudioSystem;
using UnityEngine;

public class PuzzleAudioHelper : MonoBehaviour
{
    [Header("Sons do Puzzle")]
    public string soundOnOpen;
    public string soundOnSuccess;
    public string soundOnFail;
    public string soundOnClose;

    [Header("Configurações")]
    public bool randomPitch = true;

    public void PlayOpenSound()
    {
        Debug.Log("PlayOpenSound chamado!");
        if (!string.IsNullOrEmpty(soundOnOpen))
        {
            SetRandomPitch();
            AudioSystem.AudioManager.Instance.PlaySFX(soundOnOpen);
        }
    }

    public void PlaySuccessSound()
    {
        if (!string.IsNullOrEmpty(soundOnSuccess))
        {
            SetRandomPitch();
            AudioSystem.AudioManager.Instance.PlaySFX(soundOnSuccess);
        }
    }

    public void PlayFailSound()
    {
        if (!string.IsNullOrEmpty(soundOnFail))
        {
            SetRandomPitch();
            AudioSystem.AudioManager.Instance.PlaySFX(soundOnFail);
        }
    }

    private void SetRandomPitch()
    {
        if (randomPitch && AudioSystem.AudioManager.Instance != null)
        {
            var src = AudioSystem.AudioManager.Instance.sfxSource;
            src.pitch = Random.Range(0.9f, 1.1f);
        }
    }

    public void PlayCloseSound()
    {
        if (!string.IsNullOrEmpty(soundOnClose))
            AudioSystem.AudioManager.Instance.PlaySFX(soundOnClose);
    }
}
