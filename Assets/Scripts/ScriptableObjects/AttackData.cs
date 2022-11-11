using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Data", menuName = "Combat/Attack Data")]
public class AttackData : ScriptableObject
{
    public DamageData DamageData;
    public string Animation;
    public float Cooldown;
}
