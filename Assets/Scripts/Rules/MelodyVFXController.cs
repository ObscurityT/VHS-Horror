using UnityEngine;
using UnityEngine.Rendering;

public class MelodyVFXController : MonoBehaviour
{
    public Volume glitchVolume;

    private void OnEnable()
    {
        GameEvents.OnMelodyStart += AtivarGlitch;
        GameEvents.OnMelodyEnd += DesativarGlitch;
    }

    private void OnDisable()
    {
        GameEvents.OnMelodyStart -= AtivarGlitch;
        GameEvents.OnMelodyEnd -= DesativarGlitch;
    }

    void AtivarGlitch()
    {
        if (glitchVolume != null)
            glitchVolume.weight = 1f;
    }

    void DesativarGlitch()
    {
        if (glitchVolume != null)
            glitchVolume.weight = 0f;
    }
}
