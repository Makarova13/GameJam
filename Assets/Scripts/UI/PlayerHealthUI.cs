using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Heart heartPrefab;
    [SerializeField] private GameObject heartHolder;

    private Health playerHealth;

    private List<Heart> healthList = new List<Heart>();

    private void Health_OnHealed(int dmg, int currentHP, int maxHP)
    {
        UpdateHearts();
    }

    private void Health_OnDamageTaken(int dmg, int currentHP, int maxHP)
    {
        UpdateHearts();
    }

    private void Start()
    {
        playerHealth = Player.instance.GetHealth();
        playerHealth.OnDamageTaken += Health_OnDamageTaken;
        playerHealth.OnHealed += Health_OnHealed;

        var maxHP = playerHealth.GetMaxHP();

        for (int i = 0; i < maxHP; i++)
        {
            var heart = Instantiate<Heart>(heartPrefab);
            heart.transform.parent = heartHolder.transform;
            healthList.Add(heart);
        }

        UpdateHearts();
    }

    private void UpdateHearts()
    {
        var maxHP = playerHealth.GetMaxHP();
        var currentHP = playerHealth.GetCurrentHP();

        for (int i = 0; i < maxHP; i++)
        {
            healthList[i].Toggle(i < currentHP);
        }
    }


}
