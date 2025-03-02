using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace TexasShootEm
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource musicSource;
        
        [Header("Audio Mixer")]
        [SerializeField] private AudioMixer audioMixer;
        
        [Header("Sliders")]
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;

        private void Awake()
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

        public void SetVolume(float mixerVolume)
        {
            // Ensure the volume value given is valid. 
            if (mixerVolume is < 0 or > 1)
            {
                Debug.LogWarning("<color=red>AudioManager</color>: Attempting to set mixer volume, but the given" +
                                 $"value was outside the range [0, 1]: {mixerVolume}.");
                return;
            }

            if(sfxSlider.value != mixerVolume)
            {
                float sfxMixerVolume = (Mathf.Log10(mixerVolume) * 20);
                audioMixer.SetFloat("SFXParameter", sfxMixerVolume);
            }
            
            if (musicSlider.value != mixerVolume)
            {
                float musicMixerVolume = (Mathf.Log10(mixerVolume) * 20);
                audioMixer.SetFloat("musicParameter", musicMixerVolume);
            }
        }

        private void LoadVolume()
        {
            // Load the saved volume if it exists, otherwise it defaults to 1.
            float sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 1f);
            float musicVolume = PlayerPrefs.GetFloat("musicVolume", 1f);
            
            // Set the volume levels of the mixers.
            audioMixer.SetFloat("SFXParameter", sfxVolume);
            audioMixer.SetFloat("musicParameter", musicVolume);
        }
    }
}
