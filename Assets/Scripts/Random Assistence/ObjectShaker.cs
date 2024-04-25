using UnityEngine;

public class ObjectShaker : MonoBehaviour
{
    public float shakeIntensity = 0.1f;
    public Transform objectTransform;

    private Vector3 originalPosition;

    void Start()
    {
        if (objectTransform == null)
        {
            objectTransform = transform;
        }

        originalPosition = objectTransform.localPosition;
    }

    void Update()
    {
        objectTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeIntensity;
    }
}
