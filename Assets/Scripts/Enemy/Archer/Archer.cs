using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
    public StateMachine<Archer> movementSM;
    public ArcherIdleState idle;
    public ArcherRunawayState runaway;
    public ArcherAttackState attack;

    public PoolObject ProjectilePrefab;
    private Pool ProjectilePool;

    private float nextAttackTimer;

    public void EnemyAttack()
    {
        Debug.Log("Enemy Attack triggered");
        Projectile projectile = ProjectilePool.Get(AttackPoint).GetComponent<Projectile>();
        projectile.Launch(Target);
    }
    public override void Move()
    {
        base.Move();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        ProjectilePool = new Pool(ProjectilePrefab, 3, 10, "ProjectilePool");

        movementSM = new StateMachine<Archer>();

        idle = new ArcherIdleState(this, movementSM);
        runaway = new ArcherRunawayState(this, movementSM);
        attack = new ArcherAttackState(this, movementSM);

        movementSM.Initialize(idle);
    }

    void Update()
    {
        movementSM.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        SeesPlayer = CheckPlayer();
        movementSM.CurrentState.PhysicsUpdate();
    }
}
