using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Combat Settings", menuName = "Combat/Enemy Combat Settings")]
public class EnemyCombatSettings : ScriptableObject
{
    public float AttackRadius;
    public string[] TagsToAttack;
    public DamageData LightAttackData;
    public DamageData HeavyAttackData;
    public DamageData KnockbackData;
}
