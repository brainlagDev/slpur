using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Settings")]
public class EnemyStatsSettings : ScriptableObject
{
    public float AttackRate;
    public float MovementSpeed;
    public float AttackRange;
    public int Spirits;
}
