using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "Flashlight Data", menuName = "Custom/Scriptable objects/Flashlight")]
    public class FlashlightData : ScriptableObject
    {
        [SerializeField] float intensity;
        [SerializeField] float range;
        [SerializeField] Color color;
        [SerializeField, Tooltip("Max capasity in seconds")] float powerCapacity;
        [SerializeField, Range(0, 100)] int flickeringChance;
        [SerializeField, Tooltip("With lower battery the flashlight starts flickering more and the intensity and the intensity lowers"), Range(0, 100)] 
        int lowBatteryPercent;
        [SerializeField] int lowerIntensityOnLowBatteryValue;

        public float Intensity => intensity;
        public float Range => range;
        public Color Color => color;
        public float PowerCapacity => powerCapacity;
        public int FlickeringChance => flickeringChance;
        public int LowBatteryPercent => lowBatteryPercent;
        public int LowerIntensityOnLowBatteryValue => lowerIntensityOnLowBatteryValue;
    }
}
