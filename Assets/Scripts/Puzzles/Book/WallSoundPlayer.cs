using AudioSystem;
using UnityEngine;

public class WallSoundPlayer : MonoBehaviour
{
    public string descendSoundName = "ParedeDesce"; 

    public void PlayWallDescendSound()
    {
        AudioManager.Instance.PlaySFX(descendSoundName);
    }
}
