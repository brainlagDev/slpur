using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackState : FixedState
{
    public bool IsReady 
    {
        get 
        {
            if (DateTime.Now >= LastAttackTime.AddSeconds(AttackData.Cooldown))
                return true;
            else
                return false;
        }
    }
    protected AttackData AttackData;
    private DateTime LastAttackTime;

    public AttackState(Player creature, StateMachine<Player> stateMachine, AttackData attackData, float duration) 
        : base(creature, stateMachine, duration)
    {
        AttackData = attackData;
    }

    public override void Enter()
    {
        base.Enter();

        creature.CurrentAttack = AttackData;
        creature.Animator.Play(AttackData.Animation);
        Debug.Log("Play animation: " + AttackData.Animation);  // debug
    }

    public override void Exit()
    {
        LastAttackTime = DateTime.Now;
    }

    public override void InputUpdate()
    {
    }

    public override void LogicUpdate()
    {
        if (IsFinished)
            StateMachine.ChangeState(creature.IdleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
