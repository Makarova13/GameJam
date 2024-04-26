using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SimpleChargingLoop : MonoBehaviour
{
    [SerializeField] private AudioSource chargerLoopSource;
    [SerializeField] private float volumeValueChanger;
    [SerializeField] private float soundDistance;

    private Player player;
    private bool isPlayerNear = false;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    { 
        if (isPlayerNear && !chargerLoopSource.isPlaying)
        {
            chargerLoopSource.Play();
        }
        else if(!isPlayerNear && chargerLoopSource.isPlaying)
        {
            chargerLoopSource.Stop();
        }
        if (isPlayerNear)
        {
            VolumeChanger();
        }
    }

    private void VolumeChanger()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < soundDistance && distance > 0)
        {
            chargerLoopSource.volume = Mathf.Clamp(volumeValueChanger / distance, 0f, 1f);
        }
        else
        {
            chargerLoopSource.volume = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isPlayerNear = true;

            Debug.Log("Player is near");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isPlayerNear = false;
        }
    }
}
