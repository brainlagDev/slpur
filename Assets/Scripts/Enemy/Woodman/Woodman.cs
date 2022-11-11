using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodman : Enemy
{
    #region States
    public StateMachine<Woodman> MovementSM;
    public WoodmanAttackState rangedAttack;
    public WoodmanMeleeAttackState meleeAttack;
    public WoodmanMoveState move;
    public WoodmanIdleState idle;
    #endregion

    //Make an event after position change
    [HideInInspector] public bool ChangedPos = false;
    private float _distance = 0;
    public Vector2 dir;
    public List<Transform> _movePoints;
    private int _lastPoint = 0;
    public PoolObject ProjectilePrefab;
    private Pool _projectilePool;

    private float nextAttackTimer;
    private float meleeRangeDistance;

    public void TeleportOnNewPosition(){
        var newPoint = Random.Range(0, 4);
        while(newPoint == _lastPoint){
            newPoint = Random.Range(0, 4);
        }
        _lastPoint = newPoint;
        StartCoroutine(SmoothTeleport(_movePoints[newPoint]));
    }
    public void Flip(){
        var direction = Target.transform.position.x - rb.position.x;
        _distance = direction;
        if(_distance > 0.01f){
            Debug.Log("Player is on the Right");
            
            Vector2 scale = transform.localScale;
            scale.x = -1f;
            transform.localScale = scale;
        }
        else {
            Debug.Log("Player is on the Left");
            Vector2 scale = transform.localScale;
            scale.x = 1f;
            transform.localScale = scale;
        }
    }
    public void EnemyAttack()
    {
        Debug.Log("Enemy Attack triggered");
        Projectile projectile = _projectilePool.Get(AttackPoint).GetComponent<Projectile>();
        projectile.Launch(Target);
    }
    
    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, CombatSettings.AttackRadius);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SpawnPos = this.transform;
    }
    void Start()
    {
        _projectilePool = new Pool(ProjectilePrefab, 3, 10, "ProjectilePool");
        MovementSM = new StateMachine<Woodman>();
        idle = new WoodmanIdleState(this, MovementSM);
        move = new WoodmanMoveState(this, MovementSM);
        meleeAttack = new WoodmanMeleeAttackState(this, MovementSM);
        rangedAttack = new WoodmanAttackState(this, MovementSM);

        MovementSM.Initialize(rangedAttack);
    }

    void Update()
    { 
        dir = ((Vector2)Target.transform.position - rb.position).normalized;
        MovementSM.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        MovementSM.CurrentState.PhysicsUpdate();
    }

    IEnumerator SmoothTeleport(Transform point){
        yield return new WaitForSeconds(1.5f);
        this.gameObject.transform.position = point.position;
        ChangedPos = true;
        yield break;
    }
}
