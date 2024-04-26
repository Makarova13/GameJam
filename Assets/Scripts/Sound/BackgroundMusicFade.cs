using AudioTools;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using static AudioTools.SoundSettings;

public class BackgroundMusicFade : MonoBehaviour
{
    [SerializeField] private SoundSettings.BackgroundMusicFadeSettings mainLoopSettings;
    [SerializeField] private SoundSettings.BackgroundMusicFadeSettings darkLoopSettings;
    [SerializeField] private SoundSettings.BackgroundMusicFadeSettings battleLoopSettings;

    private FlashLightController flashLightController;
    [SerializeField] private RechargingStation rechargingStation;

    private bool isFlashlightOn = true;
    private bool isBattling = false;

    private void Start()
    {
        flashLightController = FindObjectOfType<FlashLightController>();
        rechargingStation = FindObjectOfType<RechargingStation>();

        ResetVolumes(darkLoopSettings, battleLoopSettings);
    }

    void Update()
    {
        if(flashLightController.GetCurrentPower() <= 0)
        {
            isFlashlightOn = false;
        }

        if (rechargingStation.GetIsInteracting())
        {
            isFlashlightOn = true;
        }

        if (isFlashlightOn && !isBattling && battleLoopSettings.audioSource.isPlaying && !mainLoopSettings.audioSource.isPlaying)
        {
            FadeOutLoop(battleLoopSettings);
            FadeInLoop(mainLoopSettings);
        }
        else if(!isFlashlightOn && !isBattling && battleLoopSettings.audioSource.isPlaying && !darkLoopSettings.audioSource.isPlaying)
        {
            FadeOutLoop(battleLoopSettings);
            FadeInLoop(darkLoopSettings);
        }

        if (isFlashlightOn && !mainLoopSettings.audioSource.isPlaying && !isBattling)
        {
            FadeOutLoop(darkLoopSettings);
            FadeInLoop(mainLoopSettings);
        }
        else if(!isFlashlightOn && !darkLoopSettings.audioSource.isPlaying && !isBattling)
        {
            FadeOutLoop(mainLoopSettings);
            FadeInLoop(darkLoopSettings);
        }

        if(!isFlashlightOn && isBattling && !battleLoopSettings.audioSource.isPlaying)
        {
            FadeOutLoop(darkLoopSettings);
            FadeInLoop(battleLoopSettings);
        }
        else if(isFlashlightOn && isBattling && !battleLoopSettings.audioSource.isPlaying)
        {
            FadeOutLoop(mainLoopSettings);
            FadeInLoop(battleLoopSettings);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            isBattling = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            isBattling = false;
        }
    }

    private void FadeInLoop(SoundSettings.BackgroundMusicFadeSettings backgroundMusicFadeSettings)
    {
        backgroundMusicFadeSettings.audioMixer.SetFloat(backgroundMusicFadeSettings.exposedParameter, -80);
        StartCoroutine(FadeMixerGroup.StartFade(backgroundMusicFadeSettings.audioMixer, backgroundMusicFadeSettings.exposedParameter, backgroundMusicFadeSettings.fadeInVolume, backgroundMusicFadeSettings.fadeDuration));
        backgroundMusicFadeSettings.audioSource.loop = true;
        backgroundMusicFadeSettings.audioSource.Play();
    }

    private void FadeOutLoop(SoundSettings.BackgroundMusicFadeSettings backgroundMusicFadeSettings)
    {
        backgroundMusicFadeSettings.audioMixer.SetFloat(backgroundMusicFadeSettings.exposedParameter, Mathf.Log10(1) * 20);
        StartCoroutine(FadeMixerGroup.StartFade(backgroundMusicFadeSettings.audioMixer, backgroundMusicFadeSettings.exposedParameter, backgroundMusicFadeSettings.fadeOutVolume, backgroundMusicFadeSettings.fadeDuration));
        StartCoroutine(WaitUntilFade(backgroundMusicFadeSettings));
    }

    private IEnumerator WaitUntilFade(SoundSettings.BackgroundMusicFadeSettings backgroundMusicFadeSettings)
    {
        yield return new WaitForSeconds(backgroundMusicFadeSettings.fadeDuration);

        backgroundMusicFadeSettings.audioSource.loop = false;
        backgroundMusicFadeSettings.audioSource.Stop();
    }

    private void ResetVolumes(SoundSettings.BackgroundMusicFadeSettings darkLoopSettings, SoundSettings.BackgroundMusicFadeSettings battleLoopSettings)
    {
        darkLoopSettings.audioMixer.SetFloat(darkLoopSettings.exposedParameter, -80);
        battleLoopSettings.audioMixer.SetFloat(battleLoopSettings.exposedParameter, -80);
    }
}
