using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public abstract class Projectile : PoolObject
{
    protected Rigidbody2D Rigidbody;
    public ProjectileData ProjectileData;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// base.Launch must be called for delayed destroying
    /// </summary>
    public virtual void Launch(Transform target)
    {
        if (ProjectileData.LifeTime != 0)
            StartCoroutine(DelayedDestroy(ProjectileData.LifeTime));
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     foreach (var tag in ProjectileData.HitTags)
    //     {
    //         if (collision.gameObject.CompareTag(tag))
    //         {
    //             collision.gameObject.GetComponent<Creature>().GetDamage(ProjectileData.DamageData);
    //         }
    //     }
    //     Release();
    // }

    private IEnumerator DelayedDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (IsActive)
            Release();
    }
}
