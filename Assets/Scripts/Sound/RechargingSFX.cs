using AudioTools;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEditor;
using Unity.VisualScripting;

public class RechargingSFX : MonoBehaviour
{
    [Header("External Components")]
    [SerializeField] private SoundSettings.RechargingStationAudioSourceSettings rechargingAudioSourceSettings;
    [SerializeField] private SoundSettings.RechargingStationIdleSFX idleSFX;
    [SerializeField] private SoundSettings.RechargingStationActiveSFX activeSFX;
    [SerializeField] private SoundSettings.IdleSFXSettings idleRandomSFX;

    [Header("External Scripts")]
    [SerializeField] private AudioSourceManager audioSourceManager;
    [SerializeField] private RechargingStation rechargingStation;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer sfxMixer;

    [Header("Debug Value")]
    [SerializeField] private int numberOfComponents;
    [SerializeField] private AudioSource SFXAudioSource;

    private SoundSettings.AudioSourceSettings audioSourceSettings;

    private bool SFXPlaying = false;

    private void Start()
    {
        PlaySFX();
    }

    void Update()
    {
        if (CanGenerateAudioSource())
        {
            audioSourceSettings.audioSource = audioSourceManager.CreateAudioSources(rechargingAudioSourceSettings);
        }

        if(audioSourceSettings.audioSource.clip == null)
        {
            audioSourceSettings.audioSource = audioSourceManager.GetAudioSource();

            PlayClip(idleSFX.audioClip);
        }

        if (!PlayerInteracting())
        {
            SFXPlaying = true;
        }

        if (!PlayerInteracting() && !IsAudioSourcePlaying(idleSFX.audioClip))
        {
            audioSourceSettings.audioSource = audioSourceManager.GetAudioSource();
            audioSourceSettings.audioSource.Stop();

            PlayClip(idleSFX.audioClip);
        }

        if(PlayerInteracting() && !IsAudioSourcePlaying(activeSFX.audioClip))
        {
            audioSourceSettings.audioSource = audioSourceManager.GetAudioSource();
            audioSourceSettings.audioSource.Stop();

            PlayClip(activeSFX.audioClip);
        }
    }

    private void PlayClip(AudioClip clip)
    {
        audioSourceSettings.audioSource.clip = clip;
        audioSourceSettings.audioSource.Play();
    }

    private void PlaySFX()
    {
        if (!PlayerInteracting())
        {
            SFXPlaying = true;
            StartCoroutine(PlayRandomClip(idleRandomSFX));
        }
        SFXPlaying = false;
    }

    private bool IsAudioSourcePlaying(AudioClip clip)
    {
        if (audioSourceSettings.audioSource.clip.name == clip.name)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CanGenerateAudioSource()
    {
        if (IsEmpty(audioSourceManager.GetGameObject()))
        {
            return true;
        }
        else if (!IsEmpty(audioSourceManager.GetGameObject()) && !IsAudioSourcePlaying(idleSFX.audioClip) && !IsAudioSourcePlaying(activeSFX.audioClip))
        {
            Component[] allComponents = audioSourceManager.GetGameObject().GetComponents<Component>();

            Destroy(allComponents[numberOfComponents - 1]);

            return true;
        }
        else
        {
            return false;
        }
    }

    private bool PlayerInteracting()
    {
        if (rechargingStation.GetIsInteracting())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator PlayRandomClip(SoundSettings.IdleSFXSettings idleSFXSettings)
    {
        while (SFXPlaying)
        {
            yield return new WaitForSeconds(Random.Range(idleSFXSettings.delayMin, idleSFXSettings.delayMax));

            SFXAudioSource.pitch = Random.Range(idleSFXSettings._pitchMin, idleSFXSettings._pitchMax);
            SFXAudioSource.volume = Random.Range(idleSFXSettings._volumeMin, idleSFXSettings._volumeMax);
            int clipRandomiser = Random.Range(0, idleSFXSettings._clipList.Length);
            AudioClip clip = idleSFXSettings._clipList[clipRandomiser];

            SFXAudioSource.clip = clip;
            SFXAudioSource.Play();
        }
    }
    public bool IsEmpty(GameObject gameObject)
    {
        Component[] allComponents = gameObject.GetComponents<Component>();

        if(allComponents.Length == numberOfComponents)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
