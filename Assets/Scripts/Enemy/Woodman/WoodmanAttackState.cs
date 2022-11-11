using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodmanAttackState : State<Woodman>
{
    private bool isAttacking;
    private float attackRange;
    float nextAttackTimer;
    float AttackRate;
    public WoodmanAttackState(Woodman woodman, StateMachine<Woodman> stateMachine) : base(woodman, stateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Woodman entered Ranged attack State");
        //creature.Flip();
        attackRange = creature.AttackRange;
        AttackRate = creature.AttackRate;
    }
    public override void LogicUpdate()
    {
        isAttacking = creature.IsAttacking;
        if (Time.time >= nextAttackTimer)
        {
            creature.EnemyAttack();
            nextAttackTimer = Time.time + 1f / AttackRate;
        }
        if (creature.CheckDistance() < attackRange && !isAttacking)
        {
            creature.AttackAnim();
            StateMachine.ChangeState(creature.move);
        }
        base.LogicUpdate();
    }
}
