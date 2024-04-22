using AudioTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioTools
{
    public static class FadeMixerGroup
    {
        public static IEnumerator StartFade(AudioMixer audioMixer, string exposedMixerParam, float targetVolume, float fadeDuration)
        {
            float currentTime = 0;
            float currentVol;

            audioMixer.GetFloat(exposedMixerParam, out currentVol);
            currentVol = Mathf.Pow(10, currentVol / 20);
            float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);
            while (currentTime < fadeDuration)
            {
                currentTime += Time.deltaTime;
                float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / fadeDuration);
                audioMixer.SetFloat(exposedMixerParam, Mathf.Log10(newVol) * 20);

                yield return null;
            }
            yield break;
        }
    }
}
