using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Data", menuName = "Combat/Damage Data")]
public class DamageData : ScriptableObject
{
    public Creature Sender;
    public StunData? StunData;
    public float Damage;
    public bool CanBeBlocked;
    public bool CanBeParried;

    public DamageData From(Creature creature)
    {
        Sender = creature;
        return this;
    }
}
