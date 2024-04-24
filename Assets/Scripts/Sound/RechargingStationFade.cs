using AudioTools;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RechargingStationFade : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private string exposedParameter;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float fadeInVolume;
    [SerializeField] private float fadeOutVolume;

    private Player player;

    private void Update()
    {
        if(player == null)
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player) && !musicSource.isPlaying)
        {
            this.player = player;
            FadeInMusic();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(player == null)
        {
            return;
        }
        
        if(player.gameObject == other.gameObject && musicSource.isPlaying)
        {
            player = null;
            FadeOutMusic();
        }
    }

    private void FadeInMusic()
    {
        musicMixer.SetFloat(exposedParameter, -80);
        StartCoroutine(FadeMixerGroup.StartFade(musicMixer, exposedParameter, fadeInVolume, fadeDuration));
        musicSource.loop = true;
        musicSource.Play();
    }

    private void FadeOutMusic()
    {
        musicMixer.SetFloat(exposedParameter, Mathf.Log10(1) * 20);
        StartCoroutine(FadeMixerGroup.StartFade(musicMixer, exposedParameter, fadeOutVolume, fadeDuration));
        StartCoroutine(WaitUntilFade());
        musicSource.loop = false;
        musicSource.Stop();
    }

    private IEnumerator WaitUntilFade()
    {
        yield return new WaitForSeconds(fadeDuration);
    }
}
