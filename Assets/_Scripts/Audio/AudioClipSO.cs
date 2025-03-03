using UnityEngine;

namespace TexasShootEm
{
    [CreateAssetMenu(fileName = "AudioClip", menuName = "TexasShootEm/AudioClip")]
    public class AudioClipSO : ScriptableObject
    {
        public AudioClip clip;
    }
}
