using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : Enemy
{

    #region States
    public StateMachine<Ghoul> movementSM;
    public GhoulIdleState idle;
    public GhoulChaseState chase;
    public GhoulAttackState attack;
    public GhoulEvadeState evade;
    #endregion

    #region Variables
    private float nextAttackTimer;
    #endregion

    #region Functions
    public override void Move()
    {
        base.Move();
    }
    public override void GetDamage(DamageData damageData)
    {
        if(!damageData.CanBeBlocked){
            movementSM.ChangeState(evade);
        }
        else
            base.GetDamage(damageData);
    }
    public void EnemyAttack()
    {
        if (Time.time >= nextAttackTimer)
        {
            AttackAnim();
            nextAttackTimer = Time.time + 1f / AttackRate;
        }
    }
    public bool CheckIsAttacking()
    {
        return IsAttacking;
    }

    void Death(){
        Debug.Log("Event triggered");
        transform.position = SpawnPos.position;
        movementSM.ChangeState(idle);
    }
    #endregion

    #region Monodevelop Callbacks
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        movementSM = new StateMachine<Ghoul>();

        GameManager.Instance.OnPlayerDeath += Death;

        idle = new GhoulIdleState(this, movementSM);
        chase = new GhoulChaseState(this, movementSM);
        attack = new GhoulAttackState(this, movementSM);
        evade = new GhoulEvadeState(this, movementSM);

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
    void OnDestroy(){
        GameManager.Instance.OnPlayerDeath -= Death;
    }
    #endregion

    public IEnumerator Delay(){
        yield return new WaitForSeconds(1.5f);
        movementSM.ChangeState(chase);
        yield return null;
    }
}
