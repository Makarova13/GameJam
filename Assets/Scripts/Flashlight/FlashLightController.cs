using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class FlashLightController : MonoBehaviour
    {
        [SerializeField] Light mainLight;
        [SerializeField] FlashlightData defaultData;

        private float flickeringChance;
        private float currentPower;
        private int lowBatteryPercent;
        private float lowerIntensityOnLowBatteryValue;
        private WaitForSeconds waitForSeconds = new WaitForSeconds(1);

        private void Awake()
        {
            SetData(defaultData);
            SetIsOn(true);
        }

        public FlashlightData CurrentData { get; private set; }

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
