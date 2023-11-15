using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScripableObject", menuName ="ScriptableObjects/Weapon")]
public class WeaponScriptableObjects : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value;}

    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value;}

    [SerializeField]
    float speed;
    public float Speed { get=> speed; private set => speed = value;}

    [SerializeField]
    float cooldownDuration;
    public float CooldownDuration { get=> cooldownDuration; private set => cooldownDuration = value;}

    [SerializeField]
    int perrce;
    public int Perrce { get => perrce; private set => perrce = value;}
   
}