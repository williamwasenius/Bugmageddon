using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    [Header("General Stats")]
    public float sightRange = 50f;
    public float wanderSpeed = 5f;
    public float chaseSpeed = 10f;
    public Transform currentPosition;
    public Vector3 velocity;

    [Header("Ranged")]
    public bool isRanged;
    public GameObject projectile;
    public Transform shootingPoint;
    public WeaponHandler weaponHandler;
    public float attackRange = 15f;
    public float followRange = 25f;

    [Header("Detonator")]
    public bool isDetonator;
    public float detonatorDamage = 500;
    public float explosionRadius = 5;
    public GameObject explosion;
    public GameObject targetPoint;

    [Header("Charger")]
    public bool isCharger;

    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public IEnemyStates currentState;
    [HideInInspector] public WanderState wanderState;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    public Animator animator;

    [HideInInspector] public MeleeAttackState meleeAttackState;
    [HideInInspector] public RangedAttackState rangedAttackState;
    [HideInInspector] public ChargerAttackState chargerAttackState;

    public Enemy enemyCoreScript;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyCoreScript = GetComponent<Enemy>();

        wanderState = new WanderState(this, 10f, 5f);
        meleeAttackState = new MeleeAttackState(this);
        rangedAttackState = new RangedAttackState(this);

        currentState = wanderState;
    }

    private void Start()
    {
        targetPoint = GameObject.FindGameObjectWithTag("TargetPoint");
        weaponHandler = GetComponentInChildren<WeaponHandler>();
        if (isRanged)
        {
            weaponHandler.projectile = projectile;
        }
    }

    private void Update()
    {
        currentState.UpdateState();
        velocity = navMeshAgent.velocity;

        animator.SetBool("IsMoving", velocity.magnitude >= 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(collision);
    }
}
