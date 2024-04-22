using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Custom/Scriptable objects/Weapon")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField, Tooltip("Damage that the weapons does")] int damage;
        [SerializeField, Tooltip("Range To Hit Enemy")] float range;
        [SerializeField, Tooltip("Has weapon")] bool hasWeapon = true;

        public int Damage => damage;
        public float Range => range;
        public bool HasWeapon => hasWeapon;
    }

}