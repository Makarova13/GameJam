using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Dash : MonoBehaviour
    {
        public static Dash instance;

        private float DashTime;
        private float DashSpeed;
        private float WalkSpeed;
        private float DashTimer;
        private bool isDashing;
        private bool hasTimer;

        private void Awake()
        {
            if (instance != null) return;
            instance = this;

            // Initialize
            Initialize();
        }
        public void Player_Dash() // Called in the player.cs
        {
            if (isDashing || hasTimer) return;
            StartCoroutine(DashRoutine());
        }

        private IEnumerator DashRoutine() // Start Timer While Dashing
        {
            isDashing = true;
            Player.instance.Speed = DashSpeed;
            yield return new WaitForSeconds(DashTime);
            isDashing = false;
            Player.instance.Speed = WalkSpeed;
            StartCoroutine(DashTimerRoutine());
        }

        private IEnumerator DashTimerRoutine() // Start Timer After Dashing
        {
            hasTimer = true;
            yield return new WaitForSeconds(DashTimer);
            hasTimer = false;
        }

        private void Initialize() // Initialize all values
        {
            DashTime = 0.5f;
            DashSpeed = 140f;
            DashTimer = 10.0f;
            WalkSpeed = 60f;
            hasTimer = false;
            isDashing = false;
        }
    }
}
