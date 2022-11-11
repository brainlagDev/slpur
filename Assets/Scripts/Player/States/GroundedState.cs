using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : State<Player>
{
    protected float Horizontal = 0;
    protected float Vertical = 0;

    public GroundedState(Player creature, StateMachine<Player> stateMachine) : base(creature, stateMachine) { }

    public override void Enter()
    {
    }

    /// <summary>
    /// Call <c>base.InputUpdate()</c> for all input update
    /// </summary>
    public override void InputUpdate()
    {
        #region Base movement

        Horizontal = creature.Input.Horizontal();
        Vertical = creature.Input.Vertical();

        // Jumping
        if (Input.GetKey(KeyCode.Space))
        {
            StateMachine.ChangeState(creature.JumpState);
        }

        // Dashing
        if (Input.GetKey(KeyCode.LeftShift)
            && creature.CanDash)
        {
            StateMachine.ChangeState(creature.DashState);
        }

        // Climbing
        if (Vertical > 0 && 
            creature.CanClimb)
        {
            StateMachine.ChangeState(creature.ClimbState);
        }

        // Ducking
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StateMachine.ChangeState(creature.DuckState);
        }

        // Platform falling
        if (Vertical < 0
            && creature.Platform != null)
        {
            creature.DisablePlatform();
        }
        
        #endregion

        #region Combat system

        // Attack
        if (Input.GetKeyDown(creature.Input.LightAttack))
        {
            StateMachine.ChangeState(creature.LightAttackState);
        }

        // Heavy attack
        if (Input.GetKeyDown(creature.Input.HeavyAttack) &&
            creature.HeavyAttackState.IsReady)
        {
            StateMachine.ChangeState(creature.HeavyAttackState);
        }

        // Block / parry
        if (Input.GetKeyDown(creature.Input.Block))
        {
            StateMachine.ChangeState(creature.BlockState);
        }

        #endregion

        #region Other input

        //Nick 
        //Sanctuary
        if(Input.GetKeyDown(KeyCode.F)){
            StateMachine.ChangeState(creature.SanctuaryState);
        }

        // Using healing potion
        if (Input.GetKey(creature.Input.Heal))
        {
            StateMachine.ChangeState(creature.HealState);
        }

        // Interaction
        if (Input.GetKey(creature.Input.Interact))
        {
            creature.Interact();
        }

        // Menu
        if (Input.GetKey(creature.Input.Menu))
        {
            creature.PlayerUI.ShowMenu();
        }

        #endregion
    }

    /// <summary>
    /// Call <c>base.LogicUpdate()</c> for flip and fall update
    /// </summary>
    public override void LogicUpdate()
    {
        if (!creature.IsGrounded)
        {
            StateMachine.ChangeState(creature.FallState);
        }

        if (Horizontal < 0 && !creature.IsFlip)
        {
            creature.Flip();
        }
        else if (Horizontal > 0 && creature.IsFlip)
        {
            creature.Flip();
        }
    }

    public override void PhysicsUpdate()
    {
    }

    public override void Exit()
    {
    }
}
