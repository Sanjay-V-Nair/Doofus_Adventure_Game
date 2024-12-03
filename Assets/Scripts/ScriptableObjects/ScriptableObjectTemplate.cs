using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="TestScriptableObject", menuName = "ScriptableObjects/TestScriptableObject")]
public class ScriptableObjectTemplate : ScriptableObject
{
    [SerializeField] public float tileLife;
    [SerializeField] public float tileSpawnGap;
    [SerializeField] public float bulletSpeed;
}
