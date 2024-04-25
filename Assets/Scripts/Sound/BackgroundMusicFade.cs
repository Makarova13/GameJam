using AudioTools;
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
    [SerializeField] private SoundSettings.BackgroundMusicFadeSettings chargerLoopSettings;
    [SerializeField] private SoundSettings.BackgroundMusicFadeSettings battleLoopSettings;

    [SerializeField] private FlashLightController flashLightController;
    [SerializeField] private RechargingStation rechargingStation;

    private bool isFlashlightOn = true;

    private void Start()
    {
        ResetVolumes(darkLoopSettings, chargerLoopSettings, battleLoopSettings);
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

        if (isFlashlightOn && !mainLoopSettings.audioSource.isPlaying && !battleLoopSettings.audioSource.isPlaying)
        {
            FadeOutLoop(darkLoopSettings);
            FadeInLoop(mainLoopSettings);
        }

        if(!isFlashlightOn && !darkLoopSettings.audioSource.isPlaying && !battleLoopSettings.audioSource.isPlaying)
        {
            FadeOutLoop(mainLoopSettings);
            FadeInLoop(darkLoopSettings);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ChargingStation")
        {
            FadeInLoop(chargerLoopSettings);
        }

        if (other.tag == "Enemy" && isFlashlightOn)
        {
            FadeOutLoop(mainLoopSettings);
            FadeInLoop(battleLoopSettings);
        }
        else if (other.tag == "Enemy" && !isFlashlightOn)
        {
            FadeOutLoop(darkLoopSettings);
            FadeInLoop(battleLoopSettings);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "ChargingStation")
        {
            FadeOutLoop(chargerLoopSettings);
        }

        if (other.tag == "Enemy" && isFlashlightOn)
        {
            FadeInLoop(mainLoopSettings);
            FadeOutLoop(battleLoopSettings);
        }
        else if (other.tag == "Enemy" && !isFlashlightOn)
        {
            FadeInLoop(darkLoopSettings);
            FadeOutLoop(battleLoopSettings);
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

    private void ResetVolumes(SoundSettings.BackgroundMusicFadeSettings darkLoopSettings, SoundSettings.BackgroundMusicFadeSettings chargerLoopSettings, SoundSettings.BackgroundMusicFadeSettings battleLoopSettings)
    {
        darkLoopSettings.audioMixer.SetFloat(darkLoopSettings.exposedParameter, -80);
        chargerLoopSettings.audioMixer.SetFloat(chargerLoopSettings.exposedParameter, -80);
        battleLoopSettings.audioMixer.SetFloat(battleLoopSettings.exposedParameter, -80);
    }
}
