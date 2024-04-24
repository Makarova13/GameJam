using AudioTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioTools.SoundSettings;

namespace AudioTools
{
    public class AudioSourceManager : MonoBehaviour
    {
        [SerializeField] private GameObject audioSourcePlaceHolder;

        private SoundSettings.AudioSourceSettings audioSourceSettings;

        public AudioSource CreateAudioSources(SoundSettings.RechargingStationAudioSourceSettings rechargingStationSetting)
        {
            audioSourceSettings.audioSource = transform.parent.GetComponent<AudioSource>();
            audioSourceSettings.audioSource.outputAudioMixerGroup = audioSourceSettings.audioMixerGroup;
            audioSourceSettings.audioSource.loop = rechargingStationSetting.loop;
            audioSourceSettings.audioSource.playOnAwake = rechargingStationSetting.playOnAwake;
            audioSourceSettings.audioSource.spatialBlend = rechargingStationSetting.spatialBlend;
            audioSourceSettings.audioSource.minDistance = rechargingStationSetting.minDistance;
            audioSourceSettings.audioSource.maxDistance = rechargingStationSetting.maxDistance;

            return audioSourceSettings.audioSource;
        }

        public AudioSource GetAudioSource()
        {
            audioSourceSettings.audioSource = audioSourcePlaceHolder.GetComponent<AudioSource>();

            return audioSourceSettings.audioSource;
        }

        public AudioSource GetAudioSourceFromObject(GameObject obj, int audioSource)
        {
            AudioSource[] allAudioSources = obj.GetComponents<AudioSource>();

            return allAudioSources[audioSource];
        }

        public GameObject GetGameObject()
        {
            return audioSourcePlaceHolder;
        }
    }
}
