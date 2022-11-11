using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealState : FixedState
{
    public HealState(Player creature, StateMachine<Player> stateMachine, float duration) : base(creature, stateMachine, duration)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (creature.HealingPotions > 0)
        {
            creature.Animator.Play("Heal");
            creature.HealingPotions--;
            if (creature.Health + creature.Settings.PotionRegen > creature.MaxHealth)
            {
                creature.Health = creature.MaxHealth;
            }
            else
            {
                creature.Health += creature.Settings.PotionRegen;
            }
        }
        else
        {
            creature.Animator.Play("HealSearch");
        }
    }

    public override void Exit()
    {
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
