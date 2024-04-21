using UnityEngine;
using Assets.Scripts;

public class PickupController : MonoBehaviour
{
    [SerializeField] FlashlightData flashlight_data;
    public FlashlightData Data => flashlight_data;

    [SerializeField] WeaponData weapon_data;
    public WeaponData Weapon_Data => weapon_data;
}
