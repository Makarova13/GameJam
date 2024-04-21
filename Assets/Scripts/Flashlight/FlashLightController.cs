﻿using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightController : MonoBehaviour
{
    [SerializeField] Light mainLight;
    [SerializeField] FlashlightData defaultData;

    private float flickeringChance;
    private float currentPower;
    private float powerCapacity;
    private int lowBatteryPercent;
    private float lowerIntensityOnLowBatteryValue;
    private WaitForSeconds waitForSeconds = new WaitForSeconds(1);
    private bool isOn
    {
        set
        {
            if (value != _isOn)
            {
                TryFireEvent(value);
            }
            _isOn = value;
        }
        get => _isOn;
    }
    private bool _isOn;   

    private Dictionary<Direction, Vector3> directionRotation;

    private Coroutine flashLightRoutine;

    public static event System.Action OnStartLighting;    
    public static event System.Action OnStoptLighting;

    private void Awake()
    {
        directionRotation = new()
            {
                { Direction.Up, new Vector3(0, 0, -45) },
                { Direction.Down, new Vector3(0, 180, -45) },
                { Direction.Right, new Vector3(0, 90, -45) },
                { Direction.Left, new Vector3(0, -90, -45) },
            };

        SetData(defaultData);
        SetIsOn(true);
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public FlashlightData CurrentData { get; private set; }

    public void ToggleLight()
    {
        SetIsOn(!isOn);
    }

    private void Update()
    {
        //fillImage.fillAmount = currentPower/powerCapacity;
    }

    public void SetIsOn(bool isOn)
    {
        this.isOn = isOn;
        if (isOn)
        {
            mainLight.intensity = CurrentData.Intensity;
        }
        else
        {
            mainLight.intensity = 0;
        }

        if(flashLightRoutine != null)
        {
            StopCoroutine(flashLightRoutine);
        }

        flashLightRoutine = StartCoroutine(Routine());
    }

    public void Rotate(Direction direction)
    {
        gameObject.transform.rotation = Quaternion.Euler(directionRotation[direction]);
    }

    public void SetData(FlashlightData data)
    {
        CurrentData = data;
        mainLight.intensity = data.Intensity;
        mainLight.range = data.Range;
        mainLight.color = data.Color;
        powerCapacity = data.PowerCapacity;
        currentPower = powerCapacity;
        flickeringChance = data.FlickeringChance;
        lowBatteryPercent = data.LowBatteryPercent;
        lowerIntensityOnLowBatteryValue = data.LowerIntensityOnLowBatteryValue;
    }

    private IEnumerator Routine()
    {
        while (currentPower > 0)
        {
            mainLight.intensity = isOn ? CurrentData.Intensity : 0;

            if (!isOn)
            {
                yield break;
            }

            currentPower--;

            if (currentPower / powerCapacity * 100 < lowBatteryPercent)
            {
                mainLight.intensity = lowerIntensityOnLowBatteryValue;

                int chance = Random.Range(0, 100);

                if (chance <= flickeringChance)
                {
                    int count = Random.Range(1, 3);

                    for (int i = 0; i < count; i++)
                    {
                        mainLight.intensity = 0;

                        yield return new WaitForSeconds(Random.Range(0.005f, 0.01f));

                        mainLight.intensity = lowerIntensityOnLowBatteryValue;
                    }
                }
            }

            yield return waitForSeconds;
        }

        mainLight.intensity = 0;
        yield break;
    }

    public void Recharge()
    {
        currentPower = powerCapacity;
    }
    private void TryFireEvent(bool isLighting)
    {
        if (currentPower <= 0 && isLighting)
            return;

        if (isLighting)
            OnStartLighting?.Invoke();
        else
            OnStoptLighting?.Invoke();
    }
}
