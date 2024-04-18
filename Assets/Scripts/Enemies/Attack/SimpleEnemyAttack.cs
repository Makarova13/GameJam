using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float cd = 2;    
    [SerializeField] private Collider col;
    private float currentCD = 0;

    private void Update()
    {
        currentCD -= Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        var player = Player.instance;
        if (other?.gameObject == player.gameObject)
        {
            player.GetHealth().Damage(damage);
            col.enabled = false;
        }
    }
    public bool IsReady()
    {
        return currentCD <= 0;
    }
    public void Attack()
    {
        col.enabled = true;
        currentCD = cd;
    }
}
