using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Pool ProjectilePool;
    public PoolObject ProjectilePrefab;
    public float SpawnCooldown;
    public Transform SpawnPoint;
    public Transform Target;

    private void Start()
    {
        ProjectilePool = new Pool(ProjectilePrefab, 1, 10, "Projectiles");
        StartCoroutine(SpawningProjectile());
    }

    private void Shoot()
    {
        ProjectilePool.Get(SpawnPoint).GetComponent<Projectile>().Launch(Target);
    }

    private IEnumerator SpawningProjectile()
    {
        Shoot();
        yield return new WaitForSeconds(SpawnCooldown);
        StartCoroutine(SpawningProjectile());
    }
}