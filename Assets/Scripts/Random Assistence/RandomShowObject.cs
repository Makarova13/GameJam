using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShowObject : MonoBehaviour
{
    [SerializeField] private float probability = 1;
    private void Awake()
    {
        var sortedNumber = Random.Range(0f, 100f);

        if(probability < sortedNumber)
        {
            gameObject.SetActive(false);
        }
    }
}
