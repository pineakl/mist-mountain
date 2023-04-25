using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit Data SO", menuName = "Scriptable Object/Unit Data")]
public class UnitData : ScriptableObject
{
    public bool Mutable = true;
    public int MaxHealth;
    public int Health;
    [HideInInspector] public Transform UnitTransform;
    [HideInInspector] public Vector3 UnitPosition;
}
