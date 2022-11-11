using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulEvadeState : State<Ghoul>
{
    private float pushForce = 20f;
    public GhoulEvadeState(Ghoul creature, StateMachine<Ghoul> stateMachine) : base(creature, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered Evade State");
        JumpBack();
    }
    public override void Exit()
    {
        base.Exit();
    }

    private void JumpBack(){
        //creature.rb.velocity = new Vector2(Mathf.Sign(creature.transform.localScale.x) * 0.5f, 0.5f) * pushForce;
        var vec = new Vector2(Mathf.Sign(creature.transform.localScale.x) * 0.5f, 0.5f);
        creature.rb.AddForce(vec * pushForce, ForceMode2D.Impulse);
        creature.StartCoroutine("Delay");
        Debug.Log("Evade");
        
    }
}
