using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

namespace Assets.Scripts
{
    public class FlashLightController : MonoBehaviour
    {
        [SerializeField] Light mainLight;
        [SerializeField] FlashlightData defaultData;

        public enum Direction
        {
            Up,
            Down,
            Right,
            Left,
        }

        public FlashlightData CurrentData { get; private set; }

        private float flickeringChance;
        private float currentPower;
        private int lowBatteryPercent;
        private float lowerIntensityOnLowBatteryValue;
        private WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        private Dictionary<Direction, Vector3> directionRotation;

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

        public void SetIsOn(bool isOn)
        {
            if (isOn)
            {
                mainLight.intensity = CurrentData.Intensity;
            }
            else
            {
                mainLight.intensity = 0;
            }

            StartCoroutine(Routine());
        }

        public void SetData(FlashlightData data)
        {
            CurrentData = data;
            mainLight.intensity = data.Intensity;
            mainLight.range = data.Range;
            mainLight.color = data.Color;
            currentPower = data.PowerCapacity;
            flickeringChance = data.FlickeringChance;
            lowBatteryPercent = data.LowBatteryPercent;
            lowerIntensityOnLowBatteryValue = data.LowerIntensityOnLowBatteryValue;
        }

        public void Rotate(Direction direction)
        {
            gameObject.transform.rotation = Quaternion.Euler(directionRotation[direction]);
        }

        private IEnumerator Routine()
        {
            while (currentPower > 0)
            {
                currentPower--;
                mainLight.intensity = CurrentData.Intensity;

                if (currentPower / CurrentData.PowerCapacity * 100 < lowBatteryPercent)
                {
                    mainLight.intensity = lowerIntensityOnLowBatteryValue;

                    int chance = Random.Range(0, 100);

                    if (chance <= flickeringChance)
                    {
                        mainLight.intensity = 0;
                        yield return new WaitForSeconds(Random.Range(0.01f, 0.04f));
                    }
                }

                yield return waitForSeconds;
            }

            mainLight.intensity = 0;
            yield break;
        }
    }
}
