using Assets.Scripts;
using System.Collections;
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
    private bool isOn;

    private Coroutine flashLightRoutine;

    private void Awake()
    {
        SetData(defaultData);
        SetIsOn(true);
    }

    public FlashlightData CurrentData { get; private set; }

    public void ToggleLight()
    {
        SetIsOn(!isOn);
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
                    mainLight.intensity = 0;
                    yield return new WaitForSeconds(Random.Range(0.01f, 0.04f));
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
}
