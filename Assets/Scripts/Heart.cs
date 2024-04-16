using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private GameObject fill;

    public void Toggle(bool isFull)
    {
        fill.SetActive(isFull);
    }
}
