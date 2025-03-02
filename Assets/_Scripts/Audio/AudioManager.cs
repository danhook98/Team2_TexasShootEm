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
            // If audio sources are missing, send warning messages through the console and add sources.
            if (!sfxSource)
            {
                Debug.LogWarning("<color=red>AudioManager</color>: SFX AudioSource is missing or the reference is " +
                                 "missing. Creating a new AudioSource.");
                sfxSource = gameObject.AddComponent<AudioSource>();
            }

            if (!musicSource)
            {
                Debug.LogWarning("<color=red>AudioManager</color>: Music AudioSource is missing or the reference is " +
                                 "missing. Creating a new AudioSource.");
                musicSource = gameObject.AddComponent<AudioSource>();
            }

            LoadVolume();
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

        public void StopMusic()
        {
            musicSource.Stop();
        }

        public void SetMusicVolume(float musicVolume)
        {
            // Ensure the volume value given is valid. 
            if (musicVolume is < 0 or > 1)
            {
                Debug.LogWarning("<color=red>AudioManager</color>: Attempting to set music mixer volume, but the given" +
                                 $"value was outside the range [0, 1]: {musicVolume}.");
                return;
            } 
            
            // Update the Music mixer with the given volume value. A float value between 0.001 and 1 turns into - 60 db to
            // 0 db in the audio mixer. The formula below grants a logarithmic change in volume, it is calculated by converting the value to a % value.
            float volume = (Mathf.Log10(musicVolume) * 20);
            audioMixer.SetFloat("music", volume);
            PlayerPrefs.SetFloat("musicVolume", volume); // Save changes as PlayerPrefs.
        }

        public void SetSFXVolume(float sfxVolume)
        {
            // Ensure the volume value given is valid. 
            if (sfxVolume is < 0 or > 1)
            {
                Debug.LogWarning("<color=red>AudioManager</color>: Attempting to set SFX mixer volume, but the given" +
                                 $"value was outside the range [0, 1]: {sfxVolume}.");
                return;
            }
            
            // Update the SFX mixer with the given volume value. A float value between 0.001 and 1 turns into - 60 db to
            // 0 db in the audio mixer. The formula below grants a logarithmic change in volume, it is calculated by converting the value to a % value.
            float volume = (Mathf.Log10(sfxVolume) * 20);
            audioMixer.SetFloat("SFX", volume);
            PlayerPrefs.SetFloat("sfxVolume", volume); // Save changes as PlayerPrefs.
        }

        private void LoadVolume()
        {
            // Load the saved volume if it exists, otherwise it defaults to 1.
            float sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 1f);
            float musicVolume = PlayerPrefs.GetFloat("musicVolume", 1f);
            
            // Set the volume levels of the mixers.
            audioMixer.SetFloat("SFX", sfxVolume);
            audioMixer.SetFloat("music", musicVolume);
        }
    }
}
