using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicProjectile : Projectile
{
    public override void Launch(Transform target)
    {
        base.Launch(target);

        if (!Rigidbody.isKinematic)
            Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        Rigidbody.velocity = (target.transform.position - transform.position).normalized * ProjectileData.Speed;
    }
}