using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

namespace TexasShootEm
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource musicSource;
        
        [Header("Audio Mixer")]
        [SerializeField] private AudioMixer audioMixer;

        private void Start()
        {
            if (!sfxSource)
            {
                Debug.LogWarning("No SFX Audio Source assigned!");
                sfxSource = gameObject.AddComponent<AudioSource>();
            }

            if (!musicSource)
            {
                Debug.LogWarning("No Music Audio Source assigned!");
                musicSource = gameObject.AddComponent<AudioSource>();
            }
        }

        public void PlayOneShotSFX(AudioClipSO audioClip)
        {
            sfxSource.PlayOneShot(audioClip.clip);
        }

        public void PlayMusic(AudioClipSO audioClip)
        {
            musicSource.clip = audioClip.clip;
            musicSource.Play();
        }

        public void LoadVolume()
        {
            
        }
    }
}
