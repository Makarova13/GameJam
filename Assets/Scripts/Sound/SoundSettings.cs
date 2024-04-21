using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioTools
{
    /// <summary>
    /// Adjustable Audio Clip settings.
    /// </summary>
    
    public class SoundSettings
    {

        [Serializable]
        public struct IdleSFXSettings
        {
            public AudioClip[] _clipList;
            [Range(0, 3)] public float _pitchMin;
            [Range(0, 3)] public float _pitchMax;
            [Range(0, 1)] public float _volumeMin;
            [Range(0, 1)] public float _volumeMax;
            public float delayMin;
            public float delayMax;
        }

        [Serializable]
        public struct ActiveSFXSettings
        {
            public AudioClip[] _clipList;
            [Range(0, 3)] public float _pitchMin;
            [Range(0, 3)] public float _pitchMax;
            [Range(0, 1)] public float _volumeMin;
            [Range(0, 1)] public float _volumeMax;
            public float delayMin;
            public float delayMax;
        }

        [Serializable]
        public struct AudioSourceSettings
        {
            public AudioSource audioSource;
        }

        [Serializable]
        public struct RandomSFXAudioSourceSettings
        {
            public AudioSource audioSource;
        }

        [Serializable]
        public struct RechargingStationIdleSFX
        {
            public AudioClip audioClip;
            [Range(0,3)] public float pitch;
            [Range(0,1)] public float volume;
        }

        [Serializable]
        public struct RechargingStationActiveSFX
        {
            public AudioClip audioClip;
            [Range(0,3)] public float pitch;
            [Range(0,1)] public float volume;
        }
    }
}
