using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInteractableController : BaseInteractibleObject
{
    public override void Interact()
    {
        Debug.Log("Pickup");
    }
}
