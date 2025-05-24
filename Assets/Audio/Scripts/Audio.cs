using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace AudioSystem
{
    [CreateAssetMenu(fileName = "New Audio", menuName = "Scriptable Objects/Audio")]
    public class Audio : ScriptableObject
    {
        public bool isMusic = false;

        [ShowIf("isMusic")][AllowNesting] public MUSIC musicID;
        [HideIf("isMusic")][AllowNesting] public SOUND soundID;

        public List<AudioClip> clip;
        [ShowIf("isMusic")][AllowNesting] public bool isLoop;
        [HideIf("isMusic")][AllowNesting] public bool hasPitchVariation;
        [HideIf("isMusic")][AllowNesting] public float minPitch = 1f;
        [HideIf("isMusic")][AllowNesting] public float maxPitch = 1f;
    }
}