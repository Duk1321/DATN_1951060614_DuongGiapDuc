using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    //current stat
    float currenMoveSpeed;
    float currentHealth;
    float currentDamage;

    void Awake()
    {
        currenMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if(currentHealth <= 0) 
        {
            Kill();
        }
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStat player = collision.gameObject.GetComponent<PlayerStat>();
            player.TakeDamage(currentDamage);
        }
    }
}
