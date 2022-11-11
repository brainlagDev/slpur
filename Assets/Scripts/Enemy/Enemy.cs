 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Enemy : Creature
{
    // 1. ADD AWAKE
    // 2. REMEMBER TRANSFORM AT SPAWN
    // 3. REINSTANCIATE WHEN PLAYER DIES
    //
    //
    #region Variables
    [Header("Components")]
    [Space]
    
    #region Combat
    [Header("Combat")]

    [SerializeField] protected Transform Target;
    [SerializeField] protected EnemyStatsSettings stats;
    [SerializeField] protected LayerMask AttackLayer;
    [SerializeField] protected Transform AttackPoint;
    public EnemyCombatSettings CombatSettings;

    [Space]
    #endregion

    #region Line of sight
    [Header("Line of sight")]

    [SerializeField] private   GameObject CastPoint;
    [SerializeField] private float _rayLength = 15f;
    public LayerMask IgnoreLayer;
    
    [Space]
    #endregion
    
    #region Others

    [HideInInspector] public Rigidbody2D rb;
    [SerializeField] protected Animator Animator;
    protected Animator anim;
    public Transform SpawnPos;

    #endregion

    #region Booleans

    [HideInInspector] public bool IsAttacking;
    [HideInInspector] public bool SeesPlayer;

    #endregion

    #region Stats
    public float AttackRate => stats.AttackRate;
    public float MovementSpeed => stats.MovementSpeed;
    public float AttackRange => stats.AttackRange;
    public int SpiritsReward => stats.Spirits;
    #endregion

    #endregion
    
    #region Methods
    public override void GetDamage(DamageData damageData)
    {
        Health -= damageData.Damage;
        if(Health <= 0)
        {
            Health = 0;
            Die();
        }
    }
    public override void Die()
    {
        //GameManager.Instance.Spirits += SpiritsReward;
        this.gameObject.SetActive(false);
    }
    public virtual void Move()
    {
        
        var direction = ((Vector2)Target.transform.position - rb.position).normalized;
        var force = direction * MovementSpeed * Time.fixedDeltaTime;

        if (direction.x > 0.01f)
        {
            transform.localScale = new Vector3(-1.8f, 1.8f, 1.8f);
        }
        if (direction.x < -0.01f)
        {
            transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        }
        rb.velocity = new Vector2(force.x, 0);
    }
    public void SetAnimInt(int index)
    {
        anim.SetInteger("AnimState", index);
    }

    public float CheckDistance()
    {
        var distance = Vector2.Distance(rb.position, Target.position);
        return distance;
    }

    public void Respawn()
    {
        this.gameObject.transform.position = SpawnPos.position;
        this.gameObject.SetActive(true);
        Health = MaxHealth;
    }

    public virtual void AttackAnim()
    {
        if(!IsAttacking)
            Animator.SetTrigger("Attack0");
    }
    public virtual void SetIsAttacking(int i)
    {
        if (i == 1)
        {
            IsAttacking = true;
        }
        else IsAttacking = false;
    }
    public void Attack(){
        Collider2D[] damagedCreatures = Physics2D.OverlapCircleAll(AttackPoint.position, CombatSettings.AttackRadius);
        foreach (var creature in damagedCreatures)
        {
            foreach (var tag in CombatSettings.TagsToAttack)
            {
                if (creature.CompareTag(tag))
                {
                    Debug.Log("Attack");
                    creature.gameObject.GetComponent<Creature>().GetDamage(CombatSettings.LightAttackData);
                }
            }
        }
    }

    public bool CheckPlayer(){
        Vector2 cast = new Vector2(CastPoint.transform.position.x, CastPoint.transform.position.y);

        RaycastHit2D hit = Physics2D.Raycast(cast, Vector2.left * Mathf.Sign(this.transform.localScale.x), _rayLength, ~IgnoreLayer);

        Debug.DrawRay(cast, Vector2.left * _rayLength * Mathf.Sign(this.transform.localScale.x), Color.blue);
        if(hit.collider != null){
            if(hit.collider.gameObject.tag == "Player"){
                //Debug.Log("Sees Player");
                return true;
            }
            else{
                return false;
            }
        }
        else return false;
        
    }
    #endregion
    
    #region Monodevelop
    private void Awake()
    {
        //SpawnPos = this.transform;
        
        Target = FindObjectOfType<Player>().transform;
    }
    #endregion
}

