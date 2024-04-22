using Assets.Scripts;
using UnityEngine;

namespace Assets.Scripts
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] WeaponData defaultData;
        private int Damage;
        private float Range;
        private bool hasWeapon;
        private void Awake()
        {
            SetData(defaultData);
        }
        public WeaponData CurrentData { get; private set; }

        public void SetData(WeaponData data)
        {
            CurrentData = data;
            Damage = data.Damage;
            Range = data.Range;
            hasWeapon = data.HasWeapon;
        }
    }

}