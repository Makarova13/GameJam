using AudioTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioTools
{
    public class AudioSourceManager : MonoBehaviour
    {
        [SerializeField] private GameObject audioSourcePlaceHolder;

        private SoundSettings.AudioSourceSettings audioSourceSettings;
        private SoundSettings.RandomSFXAudioSourceSettings randomSFXAudioSourceSettings;

        public AudioSource CreateAudioSource(SoundSettings.RechargingStationIdleSFX rechargingStationIdleSFX)
        {
            audioSourceSettings.audioSource = audioSourcePlaceHolder.AddComponent<AudioSource>();
            audioSourceSettings.audioSource.loop = true;
            audioSourceSettings.audioSource.playOnAwake = false;
            audioSourceSettings.audioSource.volume = rechargingStationIdleSFX.volume;
            audioSourceSettings.audioSource.clip = rechargingStationIdleSFX.audioClip;

            return audioSourceSettings.audioSource;
        }

        public AudioSource CreateAudioSource(SoundSettings.RechargingStationActiveSFX rechargingStationActiveSFX)
        {
            audioSourceSettings.audioSource = audioSourcePlaceHolder.AddComponent<AudioSource>();
            audioSourceSettings.audioSource.loop = true;
            audioSourceSettings.audioSource.playOnAwake = false;
            audioSourceSettings.audioSource.volume = rechargingStationActiveSFX.volume;
            audioSourceSettings.audioSource.clip = rechargingStationActiveSFX.audioClip;

            return audioSourceSettings.audioSource;
        }

        public AudioSource GetAudioSource(SoundSettings.RechargingStationIdleSFX rechargingStationIdleSFX)
        {
            audioSourceSettings.audioSource = audioSourcePlaceHolder.GetComponent<AudioSource>();
            audioSourceSettings.audioSource.volume = rechargingStationIdleSFX.volume;
            audioSourceSettings.audioSource.clip = rechargingStationIdleSFX.audioClip;

            return audioSourceSettings.audioSource;
        }

        public AudioSource GetAudioSource(SoundSettings.RechargingStationActiveSFX rechargingStationIdleActiveSFX)
        {
            audioSourceSettings.audioSource = audioSourcePlaceHolder.GetComponent<AudioSource>();
            audioSourceSettings.audioSource.volume = rechargingStationIdleActiveSFX.volume;
            audioSourceSettings.audioSource.clip = rechargingStationIdleActiveSFX.audioClip;

            return audioSourceSettings.audioSource;
        }

        public AudioSource CreateAudioSource(SoundSettings.IdleSFXSettings soundEffectSettings)
        {
            randomSFXAudioSourceSettings.audioSource = audioSourcePlaceHolder.AddComponent<AudioSource>();
            randomSFXAudioSourceSettings.audioSource.playOnAwake = false;
            randomSFXAudioSourceSettings.audioSource.pitch = Random.Range(soundEffectSettings._pitchMin, soundEffectSettings._pitchMax);
            randomSFXAudioSourceSettings.audioSource.volume = Random.Range(soundEffectSettings._volumeMin, soundEffectSettings._volumeMax);

            return randomSFXAudioSourceSettings.audioSource;
        }

        public AudioSource CreateAudioSource(SoundSettings.ActiveSFXSettings soundEffectSettings)
        {
            randomSFXAudioSourceSettings.audioSource = audioSourcePlaceHolder.AddComponent<AudioSource>();
            randomSFXAudioSourceSettings.audioSource.playOnAwake = false;
            randomSFXAudioSourceSettings.audioSource.pitch = Random.Range(soundEffectSettings._pitchMin, soundEffectSettings._pitchMax);
            randomSFXAudioSourceSettings.audioSource.volume = Random.Range(soundEffectSettings._volumeMin, soundEffectSettings._volumeMax);

            return randomSFXAudioSourceSettings.audioSource;
        }

        public AudioSource GetAudioSource(SoundSettings.IdleSFXSettings soundEffectSettings)
        {
            randomSFXAudioSourceSettings.audioSource = audioSourcePlaceHolder.AddComponent<AudioSource>();
            randomSFXAudioSourceSettings.audioSource.pitch = Random.Range(soundEffectSettings._pitchMin, soundEffectSettings._pitchMax);
            randomSFXAudioSourceSettings.audioSource.volume = Random.Range(soundEffectSettings._volumeMin, soundEffectSettings._volumeMax);

            return randomSFXAudioSourceSettings.audioSource;
        }

        public AudioSource GetAudioSource(SoundSettings.ActiveSFXSettings soundEffectSettings)
        {
            randomSFXAudioSourceSettings.audioSource = audioSourcePlaceHolder.AddComponent<AudioSource>();
            randomSFXAudioSourceSettings.audioSource.pitch = Random.Range(soundEffectSettings._pitchMin, soundEffectSettings._pitchMax);
            randomSFXAudioSourceSettings.audioSource.volume = Random.Range(soundEffectSettings._volumeMin, soundEffectSettings._volumeMax);

            return randomSFXAudioSourceSettings.audioSource;
        }

        public AudioClip GetRandomClip(SoundSettings.IdleSFXSettings soundEffectSettings)
        {
            int clipRandomiser = Random.Range(0, soundEffectSettings._clipList.Length);
            AudioClip clip = soundEffectSettings._clipList[clipRandomiser];
            
            return clip;
        }

        public GameObject GetGameObject()
        {
            return audioSourcePlaceHolder;
        }
    }
}
