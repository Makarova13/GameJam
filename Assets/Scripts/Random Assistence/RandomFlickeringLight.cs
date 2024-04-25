using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Light))]
    public class RandomFlickeringLight : MonoBehaviour
    {
        [SerializeField] private float probability = 1;

        private Light _light;
        private WaitForSeconds waitForSeconds = new WaitForSeconds(2);
        private float normalIntensity;

        private void Awake()
        {
            _light = GetComponent<Light>();
            normalIntensity = _light.intensity;

            StartCoroutine(TurnOnAndOffRandomly());
        }

        private IEnumerator TurnOnAndOffRandomly()
        {
            while (true)
            {
                var sortedNumber = Random.Range(0f, 100f);

                if (probability < sortedNumber)
                {
                    var number = Random.Range(0f, 5f);

                    for (int i = 0; i < number; i++)
                    {
                        _light.intensity = 0;

                        yield return new WaitForSeconds(Random.Range(0.5f, 2f));

                        _light.intensity = normalIntensity;

                        yield return new WaitForSeconds(Random.Range(0.5f, 5f));
                    }
                }

                yield return waitForSeconds;
            }
        }
    }
}
