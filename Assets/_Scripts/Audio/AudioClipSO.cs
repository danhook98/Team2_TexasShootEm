using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TexasShootEm
{
    [CreateAssetMenu(fileName = "AudioClip", menuName = "TexasShootEm/AudioClip")]
    public class AudioClipSO : ScriptableObject
    {
        public AudioClip clip;
    }
}
