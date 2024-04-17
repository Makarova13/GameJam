using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int initalMaxHP = 5;
        [SerializeField] private int initalCurrentHP = 5;

        private int maxHP;
        private int currentHP;

        public Action<int, int, int> OnDamageTaken;
        public Action<int, int, int> OnHealed;
        public Action OnDeath;

        private void Awake()
        {
            maxHP = initalMaxHP;
            currentHP = initalCurrentHP;
        }

        public int GetMaxHP() => maxHP;
        public int GetCurrentHP() => currentHP;

        public void Damage(int damage)
        {
            currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);

            OnDamageTaken?.Invoke(damage, currentHP, maxHP);

            if (currentHP <= 0)
            {
                OnDeath?.Invoke();
            }
        }

        public void Heal(int amount)
        {
            currentHP = Mathf.Clamp(currentHP + amount, 0, maxHP);

            OnHealed?.Invoke(amount, currentHP, maxHP);
        }
    }
}
