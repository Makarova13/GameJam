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
    [SerializeField] private SoundSettings.RechargingStationIdleSFX idleSFX;
    [SerializeField] private SoundSettings.RechargingStationActiveSFX activeSFX;
    [SerializeField] private SoundSettings.IdleSFXSettings idleRandomSFX;
    [SerializeField] private SoundSettings.ActiveSFXSettings activeRandomSFX;

    [Header("External Scripts")]
    [SerializeField] private AudioSourceManager audioSourceManager;
    [SerializeField] private RechargingStation rechargingStation;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer sfxMixer;

    [Header("Debug Value")]
    [SerializeField] private int numberOfComponents;

    private SoundSettings.AudioSourceSettings audioSourceSettings;

    private bool idlePlaying = false;
    private bool activePlaying = false;

    private bool generateAudioSource = true;

    void Update()
    {

        if (rechargingStation.GetIsInteracting())
        {
            idlePlaying = false;
            activePlaying = true;
        }
        else
        {
            activePlaying = false;
            idlePlaying = true;
        }

        if(IsEmpty(audioSourceManager.GetGameObject()))
        {
            generateAudioSource = true;
        }
        else if (!IsEmpty(audioSourceManager.GetGameObject()) && !idlePlaying && !activePlaying)
        {
            Component[] allComponents = audioSourceManager.GetGameObject().GetComponents<Component>();

            Destroy(allComponents[numberOfComponents - 1]);

            generateAudioSource = true;
        }
        else
        {
            generateAudioSource = false;
        }

        if(generateAudioSource)
        {
            if (idlePlaying)
            {
                audioSourceSettings.audioSource = audioSourceManager.CreateAudioSource(idleSFX);

                audioSourceSettings.audioSource.clip = idleSFX.audioClip;
                audioSourceSettings.audioSource.Play();
                StartCoroutine(PlayRandomClip(idleRandomSFX));
            }
            else if (activePlaying)
            {
                audioSourceSettings.audioSource = audioSourceManager.CreateAudioSource(activeSFX);

                audioSourceSettings.audioSource.clip = activeSFX.audioClip;
                audioSourceSettings.audioSource.Play();
                StartCoroutine(PlayRandomClip(activeRandomSFX));
            }
        }
        else if(!generateAudioSource && !IsEmpty(audioSourceManager.GetGameObject()))
        {
            if (idlePlaying)
            {
                audioSourceSettings.audioSource = audioSourceManager.GetAudioSource(idleSFX);

                audioSourceSettings.audioSource.Stop();
                audioSourceSettings.audioSource.clip = idleSFX.audioClip;
                audioSourceSettings.audioSource.Play();
                StartCoroutine(PlayRandomClip(idleRandomSFX));
            }
            else if(activePlaying)
            {
                audioSourceSettings.audioSource = audioSourceManager.GetAudioSource(activeSFX);

                audioSourceSettings.audioSource.Stop();
                audioSourceSettings.audioSource.clip = activeSFX.audioClip;
                audioSourceSettings.audioSource.Play();
                StartCoroutine(PlayRandomClip(activeRandomSFX));
            }
        }
        
    }

    private IEnumerator PlayRandomClip(SoundSettings.ActiveSFXSettings activeSFXSettings)
    {
        yield return new WaitForSeconds(Random.Range(activeSFXSettings.delayMin, activeSFXSettings.delayMax));
        
        if (!idlePlaying)
        {
            audioSourceSettings.audioSource.PlayOneShot(GetRandomClip(activeSFXSettings));
        }
    }

    private IEnumerator PlayRandomClip(SoundSettings.IdleSFXSettings idleSFXSettings)
    {
        yield return new WaitForSeconds(Random.Range(idleSFXSettings.delayMin, idleSFXSettings.delayMax));

        if (!activePlaying)
        {
            audioSourceSettings.audioSource.PlayOneShot(GetRandomClip(idleSFXSettings));
        }
    }

    public AudioClip GetRandomClip(SoundSettings.IdleSFXSettings soundEffectSettings)
    {
        int clipRandomiser = Random.Range(0, soundEffectSettings._clipList.Length);
        AudioClip clip = soundEffectSettings._clipList[clipRandomiser];

        return clip;
    }

    public AudioClip GetRandomClip(SoundSettings.ActiveSFXSettings soundEffectSettings)
    {
        int clipRandomiser = Random.Range(0, soundEffectSettings._clipList.Length);
        AudioClip clip = soundEffectSettings._clipList[clipRandomiser];

        return clip;
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
