using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static AudioTools.SoundSettings;

namespace AudioTools
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] public GameObject _audioSourceHolder;

        /// <summary>
        /// Randomises AudioClips, pitch and volume to create variation in sound.
        /// </summary>
        /// <param name="_soundEffectSettings"></param>

        public void PlayRandom(SoundSettings.IdleSFXSettings[] _soundEffectSettings)
        {
            if (_soundEffectSettings.Length == 0)
            {
                Debug.LogError("There are no audio settings defined");
            }
            else
            {
                for (int i = 0; i < _soundEffectSettings.Length; i++)
                {
                    if (!AnyClipPlaying(_soundEffectSettings[i]._clipList))
                    {
                        Debug.LogError("There are no clips to play");
                    }
                    else
                    {
                        AudioSource audioSource = _audioSourceHolder.AddComponent<AudioSource>();
                        audioSource.pitch = UnityEngine.Random.Range(_soundEffectSettings[i]._pitchMin, _soundEffectSettings[i]._pitchMax);
                        audioSource.volume = UnityEngine.Random.Range(_soundEffectSettings[i]._volumeMin, _soundEffectSettings[i]._volumeMax);
                        int clipRandomiser = UnityEngine.Random.Range(0, _soundEffectSettings[i]._clipList.Length);
                        AudioClip clip = _soundEffectSettings[i]._clipList[clipRandomiser];
                        audioSource.PlayOneShot(clip);
                        Destroy(audioSource, 1);
                    }
                }
            }
        }

        public void PlayRandom(SoundSettings.IdleSFXSettings _soundEffectSettings)
        {
            if (AnyClipPlaying(_soundEffectSettings._clipList))
            {
                Debug.LogError("There are no clips to play");
            }
            else
            {
                AudioSource audioSource = _audioSourceHolder.AddComponent<AudioSource>();
                audioSource.pitch = UnityEngine.Random.Range(_soundEffectSettings._pitchMin, _soundEffectSettings._pitchMax);
                audioSource.volume = UnityEngine.Random.Range(_soundEffectSettings._volumeMin, _soundEffectSettings._volumeMax);
                int clipRandomiser = UnityEngine.Random.Range(0, _soundEffectSettings._clipList.Length);
                AudioClip clip = _soundEffectSettings._clipList[clipRandomiser];
                audioSource.clip = clip;
                audioSource.Play();
                Destroy(audioSource, clip.length);
            }
        }

        private bool AnyClipPlaying(AudioClip[] clipList)
        {
            try
            {
                for (int i = 0; i < clipList.Length; i++)
                {
                    if (clipList[i].length > 0) return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }
            return false;
        }
    }
}
