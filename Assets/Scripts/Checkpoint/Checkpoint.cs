using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;

    [SerializeField] private Animator animator;


    private string BlockPathAnimation = "PathblockAnimation";


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            PlayPathblockAnimation();

            SaveLoadManager.Instance.SavePosition(spawnPosition.position);
        }
    }

    private void PlayPathblockAnimation()
    {
        animator.Play(BlockPathAnimation);
    }
}
