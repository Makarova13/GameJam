using UnityEngine;
using Assets.Scripts;

public class PickupController : MonoBehaviour
{
    [SerializeField] FlashlightData flashlight_data;
    public FlashlightData Data => flashlight_data;
}
