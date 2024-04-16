using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioTools;

public class SoundEffectsRandomiser : MonoBehaviour
{
    [SerializeField] private SoundSettings[] _soundSettings;
    private AudioManager _audioManager;
    
    void Start()
    {
        _audioManager = GetComponent<AudioManager>();

        _audioManager.PlayRandom(_soundSettings);
    }
}
