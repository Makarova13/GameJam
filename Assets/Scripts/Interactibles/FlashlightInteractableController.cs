using Assets.Scripts;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class FlashlightInteractableController : BaseInteractibleObject
{
    [SerializeField] PickupController pickupController;
    [SerializeField] FlashLightController flashlightController;

    public override void Interact() // SetData of the flashlight
    {
        flashlightController.SetData(pickupController.Data);
    }
}
