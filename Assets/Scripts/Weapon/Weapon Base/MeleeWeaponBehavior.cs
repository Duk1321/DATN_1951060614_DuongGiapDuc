using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script for all melee behavior
/// </summary>


public class MeleeWeaponBehavior : MonoBehaviour
{
    public float destroyAfterSeconds;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }
}
