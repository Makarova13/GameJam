using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FadeByDistance : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float soundDistance;
    [SerializeField] private float volumeValueChanger;
    [SerializeField] private string mixerExposedParameter;

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        VolumeChanger();
    }

    private void VolumeChanger()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance < soundDistance && distance > 0)
        {
            float newVolume = Mathf.Clamp(volumeValueChanger / distance, 0f, 1f);
            audioMixer.SetFloat(mixerExposedParameter, Mathf.Log10(newVolume) * 20);
        }
        else
        {
            audioMixer.SetFloat(mixerExposedParameter, -80);
        }
    }
}
