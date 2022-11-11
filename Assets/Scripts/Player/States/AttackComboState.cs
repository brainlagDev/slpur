using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComboState : FixedState
{
    private static int ComboCounter;
    private AttackData[] AttacksData;
    private bool Repeat;
    private DateTime LastAttack = DateTime.Now;

    public AttackComboState(Player creature, StateMachine<Player> stateMachine, AttackData[] attacksData, float duration) 
        : base(creature, stateMachine, duration)
    {
        AttacksData = attacksData;
    }

    public override void Enter()
    {
        base.Enter();

        Repeat = false;

        // reset attack counter if too much time passed
        if (DateTime.Now > DateTime.Now.AddSeconds(creature.Combat.AttackCountTime))
            ComboCounter = 0;

        creature.CurrentAttack = AttacksData[ComboCounter];
        creature.Animator.Play(AttacksData[ComboCounter].Animation);
    }

    public override void Exit()
    {
        LastAttack = DateTime.Now;
    }

    public override void InputUpdate()
    {
        if (Input.GetKeyDown(creature.Input.LightAttack))
        {
            Repeat = true;
        }
    }

    public override void LogicUpdate()
    {
        if (IsFinished)
        {
            if (ComboCounter + 1 < AttacksData.Length)
                ComboCounter++;
            else
                ComboCounter = 0;

            if (Repeat)
            {
                StateMachine.ChangeState(creature.LightAttackState);
            }
            else
            {
                StateMachine.ChangeState(creature.IdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
