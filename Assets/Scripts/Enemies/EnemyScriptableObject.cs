using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyScripableObject", menuName ="ScripableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    //Enemy Stat
    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value;}

    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value;}

    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value;}
}
