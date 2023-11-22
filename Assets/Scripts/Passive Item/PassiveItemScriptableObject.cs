using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItemScriptableObject", menuName = "ScriptableObjects/Passive")]
public class PassiveItemScriptableObject : ScriptableObject
{
    [SerializeField]
    float multipler;

    public float Multipler { get => multipler; private set => multipler = value; }


}
