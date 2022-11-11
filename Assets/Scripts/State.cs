using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T>
{
    protected T creature;
    protected StateMachine<T> StateMachine;
    protected State(T creature, StateMachine<T> stateMachine)
    {
        this.creature = creature;
        this.StateMachine = stateMachine;
    }
    public virtual void Enter()
    {

    }

    public virtual void InputUpdate() 
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}
