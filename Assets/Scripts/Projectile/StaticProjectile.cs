using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticProjectile : Projectile
{
    public override void Launch(Transform target)
    {
        base.Launch(target);
        Vector2 vel = (target.position - transform.position).normalized;
        Rigidbody.velocity = new Vector2(vel.x, 0) * ProjectileData.Speed;
    }
}
