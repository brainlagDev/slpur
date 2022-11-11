using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]

//     _
// .__(.)< (Я чУТОЧКА поменял твой код... не злись если что)
//  \___)
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
public class Player : Creature
{
    #region VARIABLES

    #region Components
    [Header("Compponents")]

    public StateMachine<Player> StateMachine;
    public PlayerSettings       Settings;
    public PlayerInputSettings  Input;
    public PlayerCombatSettings Combat;
    public GameObject           Sprite;
    //Nick | cached the Sanctuary
    public GameObject CurrentSanct;

    [HideInInspector] public Rigidbody2D    Rigidbody;
    [HideInInspector] public Animator       Animator;
    [HideInInspector] public PlayerUI       PlayerUI;
    [HideInInspector] public Inventory      Inventory;

     public CapsuleCollider2D  CapsuleCollider;
    public CircleCollider2D   DuckCollider;

    public string CurrentState;  // for debug

    [Space]
    #endregion

    #region Checks
    [Header("Checks")]

    // Flip
    public bool IsFlip = false;

    // Ground
    public bool IsGrounded = true;
    [SerializeField] private LayerMask GroundCheckLayer;
    public float AirTime = 0;

    // Ledge
    [SerializeField] private LayerMask  LedgeCheckLayer;
    [SerializeField] private float      LedgeCheckDistance;
    public bool         LeftLedge = false;
    public bool         RightLedge = false;
    public Collider2D   GrabCollision;
    public Vector2      HangLedgeOffset;
    public Vector2      GrabLedgeOffset;
    private Transform   LU_LedgeChecker;
    private Transform   LD_LedgeChecker;
    private Transform   RU_LedgeChecker;
    private Transform   RD_LedgeChecker;

    [Space]
    #endregion

    #region Other
    [Header("Other")]

    public int Level;
    public int Spirits;
    public int HealingPotions;

    public bool CanDash = true;
    public bool CanClimb = false;
    //Nick | added sanct check
    public bool CanUseSanctuary = false;

    public InteractableObject InteractableObject = null;
    public GameObject Platform = null;

    [Space]
    #endregion

    #region CombatSystem
    [Header("Combat system")]

    public AttackData CurrentAttack = null;
    public Transform    AttackPoint;
    public LayerMask    AttackLayer;
    public bool         IsBlock = false;
    public bool         IsParry = false;
    public bool         CanKnockback = true;

    [Space]
    #endregion

    #region Animations
    [Header("Animations")]

    [SerializeField] private Dictionary<string, float> Animations;

    [Space]
    #endregion

    #region States
    [Header("States")]

    public AttackComboState  LightAttackState;
    public AttackState  HeavyAttackState;
    public AttackState  ParryState;

    public BlockState   BlockState;
    public ClimbState   ClimbState;
    public DashState    DashState;
    public DieState     DieState;
    public DuckState    DuckState;
    public FallState    FallState;
    public GrabState    GrabState;
    public HealState    HealState;
    public IdleState    IdleState;
    public JumpState    JumpState;
    public LandState    LandState;
    public MenuState    MenuState;
    public MoveState    MoveState;
    public SpawnState   SpawnState;
    public StunnedState StunnedState;

    //Nick | added sanctuary state
    public SanctuaryState SanctuaryState;

    //[Space]
    #endregion

    #endregion  // VARIABLES

    #region MONODEVELOP_CONSTRUCTIONS

    private void OnEnable()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Inventory = GetComponent<Inventory>();
        PlayerUI = GetComponentInChildren<PlayerUI>();

        DuckCollider = GetComponent<CircleCollider2D>();
        CapsuleCollider = GetComponent<CapsuleCollider2D>();

        LU_LedgeChecker = GameObject.Find("LU_LedgeChecker").transform;
        LD_LedgeChecker = GameObject.Find("LD_LedgeChecker").transform;
        RU_LedgeChecker = GameObject.Find("RU_LedgeChecker").transform;
        RD_LedgeChecker = GameObject.Find("RD_LedgeChecker").transform;
    }

    void Awake()
    {
    }

    void Start()
    {
        #region Filling animations dictionary
        Animations = new Dictionary<string, float>();
        AnimationClip[] clips = Animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            Animations.Add(clip.name, clip.length);
        }
        #endregion

        #region Initializing state machine and states

        StateMachine = new StateMachine<Player>();

        LightAttackState = new AttackComboState(
            this, StateMachine, 
            Combat.LightAttacks, 
            Animations["AttackLight"]);
        HeavyAttackState = new AttackState(
            this, StateMachine, 
            Combat.HeavyAttackData, 
            Animations["AttackHeavy"]);
        ParryState = new AttackState(
            this, StateMachine, 
            Combat.ParryData, 
            Animations["Parry"]);

        BlockState  = new BlockState(this, StateMachine);
        ClimbState  = new ClimbState(this, StateMachine);
        DashState   = new DashState(this, StateMachine, Animations["Dash"]);
        DieState    = new DieState(this, StateMachine, Animations["Die"]);
        DuckState   = new DuckState(this, StateMachine);
        FallState   = new FallState(this, StateMachine);
        GrabState   = new GrabState(this, StateMachine);
        HealState   = new HealState(this, StateMachine, Animations["Heal"]);
        IdleState   = new IdleState(this, StateMachine);
        JumpState   = new JumpState(this, StateMachine);
        LandState   = new LandState(this, StateMachine, Animations["Land"]);
        MenuState   = new MenuState(this, StateMachine);
        MoveState   = new MoveState(this, StateMachine);
        SpawnState  = new SpawnState(this, StateMachine, Animations["Spawn"]);
        StunnedState = new StunnedState(this, StateMachine, Animations["Stun"]);
        SanctuaryState = new SanctuaryState(this, StateMachine);

        StateMachine.Initialize(SpawnState);

        #endregion
    }

    void Update()
    {
        
        StateMachine.CurrentState.InputUpdate();
        StateMachine.CurrentState.LogicUpdate();

        CheckGround();

        CheckLedge();

        CurrentState = StateMachine.CurrentState.GetType().ToString();  // for debugging (shown in inspector)
    }
    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, Combat.AttackRadius);
    }
    void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #region Trigger and collision events

    // Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Interactable":
                InteractableObject interactable = collision.GetComponent<InteractableObject>();
                if (InteractableObject != interactable)
                {
                    PlayerUI.ShowInteractable(interactable);
                    InteractableObject = interactable;
                }
                break;
            case "Ladder":
                CanClimb = true;
                break;
            //Nick | changed the way of projectile damage to restrict force apply on the player
            case "EnemyProjectile":
                var obj = collision.gameObject.GetComponent<Projectile>();
                var damage = obj.ProjectileData.DamageData;
                GetDamage(damage);
                StartCoroutine(SlowDownEffect(1.5f));
                obj.Release();
                break;

            //Nick | added sanct
            case "Sanctuary":
                CurrentSanct = collision.gameObject;
                CanUseSanctuary = true;
                break;
            default:
                break;
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Interactable":
                InteractableObject interactable = collision.GetComponent<InteractableObject>();
                if (InteractableObject == interactable)
                {
                    PlayerUI.HideInteractable();
                    InteractableObject = null;
                }
                break;
            case "Ladder":
                CanClimb = false;
                break;
            case "Sanctuary":
                CanUseSanctuary = true;
                CurrentSanct = null;
                break;
            default:
                break;
        }
    }

    // Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                GetKnockback(collision.gameObject.GetComponent<Enemy>().CombatSettings.KnockbackData);
                break;
            case "OneWayPlatform":
                Platform = collision.gameObject;
                break;
            default:
                break;
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "OneWayPlatform":
                Platform = null;
                break;
            default:
                break;
        }
    }

    #endregion

    #endregion // MONODEVELOP_CONSTRUCTIONS

    #region METHODS

    public void Flip()
    {
        IsFlip = !IsFlip;
        Vector2 scale = Sprite.transform.localScale;
        scale.x *= -1;
        Sprite.transform.localScale = scale;
    }

    // Must be called as event in animation timeline
    public void DealDamage()
    {
        Collider2D[] damagedCreatures =
            Physics2D.OverlapCircleAll(AttackPoint.position, Combat.AttackRadius, AttackLayer);
        foreach (var creature in damagedCreatures)
        {
            foreach (var tag in Combat.TagsToAttack)
            {
                if (creature.CompareTag(tag))
                {
                    creature.gameObject.GetComponent<Creature>().
                        GetDamage(CurrentAttack.DamageData.From(this));
                }
            }
        }
    }

    public override void GetDamage(DamageData damageData)
    {
        // cannot get any damage if is already dead
        if (StateMachine.CurrentState == DieState)
            return;

        if (IsParry && damageData.CanBeParried)
        {
            if (damageData.Sender != null)
            {
                StateMachine.ChangeState(ParryState);
            }
            else  // means that damage came from projectile
            {
                // some things...
            }
        }
        else if (IsBlock && damageData.CanBeBlocked)
        {
            Health -= damageData.Damage * Combat.BlockRate;
            // little knockback?
        }
        else
        {
            if (damageData.StunData != null)
            {
                int chance = Random.Range(1, 100 + 1);  //1 2 3 4 5 6 7 8 10
                if (chance <= damageData.StunData.Chance)            //1 2 3 4
                {
                    StunnedState.SetStunData(damageData.StunData);
                    StateMachine.ChangeState(StunnedState);
                }
            }
            Health -= damageData.Damage;
        }

        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
    }

    private void GetKnockback(DamageData knockbackData)
    {
        if (CanKnockback)
        {
            GetDamage(knockbackData);
            StartCoroutine(KnockbackCooldowning(Combat.GetKnockbackCooldown));
        }
    }

    public override void Die()
    {
        StateMachine.ChangeState(DieState);
    }

    private void CheckGround()
    {
        RaycastHit2D ray = Physics2D.Raycast
            (CapsuleCollider.bounds.center,
            Vector2.down,
            CapsuleCollider.bounds.extents.y + 0.1f, 
            GroundCheckLayer.value);

        if (ray.collider != null)
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }

    private void CheckLedge()
    {
        #region Left ledge
        RaycastHit2D luRay = Physics2D.Raycast(
            LU_LedgeChecker.position,
            Vector2.left,
            LedgeCheckDistance,
            LedgeCheckLayer.value);

        RaycastHit2D ldRay = Physics2D.Raycast(
            LD_LedgeChecker.position,
            Vector2.left,
            LedgeCheckDistance,
            LedgeCheckLayer.value);

        if (luRay.collider == null && ldRay.collider != null)
        {
            LeftLedge = true;
            GrabCollision = ldRay.collider;
            return;
        }
        else
        {
            LeftLedge = false;
            GrabCollision = null;
        }
        #endregion

        #region Right ledge
        RaycastHit2D ruRay = Physics2D.Raycast(
            RU_LedgeChecker.position,
            Vector2.right,
            LedgeCheckDistance,
            LedgeCheckLayer.value);

        RaycastHit2D rdRay = Physics2D.Raycast(
            RD_LedgeChecker.position,
            Vector2.right,
            LedgeCheckDistance,
            LedgeCheckLayer.value);

        if (ruRay.collider == null && rdRay.collider != null)
        {
            RightLedge = true;
            GrabCollision = rdRay.collider;
            return;
        }
        else
        {
            RightLedge = false;
            GrabCollision = null;
        }
        #endregion
    }

    public void DisablePlatform()
    {
        StartCoroutine(DisablingPlatform());
    }

    public void Interact()
    {
        if (InteractableObject != null)
        {
            InteractableObject.Interact();
        }
    }

    public void AddItem(Item item)
    {

    }

    #endregion  // METHODS

    #region IENUMERATORS

    IEnumerator SlowDownEffect(float time){
        Settings.MoveSpeed /= 2;
        yield return new WaitForSeconds(time);
        Settings.MoveSpeed *= 2;
        yield return null;
    }

    public IEnumerator Parrying()
    {
        IsParry = true;
        yield return new WaitForSeconds(Combat.ParryTime);
        IsParry = false;
    }

    private IEnumerator KnockbackCooldowning(float cooldown)
    {
        CanKnockback = false;
        yield return new WaitForSeconds(cooldown);
        CanKnockback = true;
    }

    private IEnumerator DisablingPlatform()
    {
        TilemapCollider2D platformCollider = Platform.GetComponent<TilemapCollider2D>();

        Physics2D.IgnoreCollision(platformCollider, CapsuleCollider);
        Physics2D.IgnoreCollision(platformCollider, DuckCollider);
        yield return new WaitForSeconds(.25f);
        Physics2D.IgnoreCollision(platformCollider, CapsuleCollider, false);
    }

    #endregion  // IENUMERATORS
}