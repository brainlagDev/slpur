using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    public float Health;

    public float MaxHealth;

    public virtual void GetDamage(DamageData damageData) { }

    public virtual void Die() { }
}