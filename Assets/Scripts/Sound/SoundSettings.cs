using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioTools
{
    /// <summary>
    /// Adjustable Audio Clip settings.
    /// </summary>
    
    [Serializable]
    public class SoundSettings
    {
        public AudioClip[] _clipList;
        [Range(0, 3)] public float _pitchMin;
        [Range(0, 3)] public float _pitchMax;
        [Range(0, 1)] public float _volumeMin;
        [Range(0, 1)] public float _volumeMax;
        public float _timer;
    }
}
