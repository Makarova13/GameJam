using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLights : MonoBehaviour
{
    [SerializeField] private AnimationCurve flickering;
    [SerializeField] private float lightIntensity;
    [SerializeField] private Light light;
    [SerializeField] private float curveDuration = 5;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > curveDuration)
        {
            timer = 0;
        }

        light.intensity = lightIntensity * flickering.Evaluate(timer / curveDuration);   
    }
}
