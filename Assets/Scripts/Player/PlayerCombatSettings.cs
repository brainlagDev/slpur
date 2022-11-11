using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Combat Settings", menuName = "Combat/Player Combat Settings")]
public class PlayerCombatSettings : ScriptableObject
{
    public float AttackRadius;
    public string[] TagsToAttack;

    public float AttackCountTime;
    public AttackData[] LightAttacks;

    public AttackData HeavyAttackData;

    public AttackData ParryData;

    public float BlockRate;
    public float ParryTime;
    public float GetKnockbackCooldown;
}