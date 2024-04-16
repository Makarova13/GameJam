using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioTools
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private GameObject _audioSourceHolder;

        /// <summary>
        /// Randomises AudioClips, pitch and volume to create variation in sound.
        /// </summary>
        /// <param name="_soundSettings"></param>

        public void PlayRandom(SoundSettings[] _soundSettings)
        {
            if (_soundSettings.Length == 0)
            {
                Debug.LogError("There are no audio settings defined");
            }
            else
            {
                for (int i = 0; i < _soundSettings.Length; i++)
                {
                    if (!AnyClipPlaying(_soundSettings[i]._clipList))
                    {
                        Debug.LogError("There are no clips to play");
                    }
                    else
                    {
                        AudioSource audioSource = _audioSourceHolder.AddComponent<AudioSource>();
                        audioSource.pitch = UnityEngine.Random.Range(_soundSettings[i]._pitchMin, _soundSettings[i]._pitchMax);
                        audioSource.volume = UnityEngine.Random.Range(_soundSettings[i]._volumeMin, _soundSettings[i]._volumeMax);
                        int clipRandomiser = UnityEngine.Random.Range(0, _soundSettings[i]._clipList.Length);
                        AudioClip clip = _soundSettings[i]._clipList[clipRandomiser];
                        audioSource.clip = clip;
                        audioSource.Play();
                        Destroy(audioSource, clip.length);
                    }
                }
            }
        }

        public void PlayRandom(SoundSettings _soundSettings)
        {
            if (AnyClipPlaying(_soundSettings._clipList))
            {
                Debug.LogError("There are no clips to play");
            }
            else
            {
                AudioSource audioSource = _audioSourceHolder.AddComponent<AudioSource>();
                audioSource.pitch = UnityEngine.Random.Range(_soundSettings._pitchMin, _soundSettings._pitchMax);
                audioSource.volume = UnityEngine.Random.Range(_soundSettings._volumeMin, _soundSettings._volumeMax);
                int clipRandomiser = UnityEngine.Random.Range(0, _soundSettings._clipList.Length);
                AudioClip clip = _soundSettings._clipList[clipRandomiser];
                audioSource.clip = clip;
                audioSource.Play();
                Destroy(audioSource, clip.length);
            }
        }

        public bool AnyClipPlaying(AudioClip[] clipList)
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
