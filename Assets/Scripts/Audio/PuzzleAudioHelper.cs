using AudioSystem;
using UnityEngine;

public class PuzzleAudioHelper : MonoBehaviour
{
    [Header("Sons do Puzzle")]
    public SOUND soundOnOpen;
    public SOUND soundOnSuccess;
    public SOUND soundOnFail;

    [Header("Configurações")]
    public bool randomPitch = true;

    public void PlayOpenSound()
    {
        if (soundOnOpen != SOUND.None)
            AudioManager.instance.PlaySfx(soundOnOpen, -1, randomPitch);
    }

    public void PlaySuccessSound()
    {
        if (soundOnSuccess != SOUND.None)
            AudioManager.instance.PlaySfx(soundOnSuccess, -1, randomPitch);
    }

    public void PlayFailSound()
    {
        if (soundOnFail != SOUND.None)
            AudioManager.instance.PlaySfx(soundOnFail, -1, randomPitch);
    }
}
