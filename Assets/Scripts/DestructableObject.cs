using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : Creature
{
    private int Spirits;
    public override void GetDamage(DamageData damageData)
    {
        Health -= damageData.Damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        if (Spirits != 0)
            GameManager.Instance.Player.Spirits += Spirits;
        Destroy(gameObject);
    }
}