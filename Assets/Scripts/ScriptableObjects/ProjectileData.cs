using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Projectile Data", menuName ="Combat/Projectile Data")]
public class ProjectileData : ScriptableObject
{
    public DamageData DamageData;
    public float Speed;
    public float LifeTime;
    public string[] HitTags;
}