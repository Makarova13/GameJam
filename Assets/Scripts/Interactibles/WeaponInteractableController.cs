using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInteractableController : BaseInteractibleObject
{
    [SerializeField] PickupController pickupController;
    [SerializeField] WeaponController weaponController;
    public override void Interact()
    {
        weaponController.SetData(pickupController.Weapon_Data);
    }
}
