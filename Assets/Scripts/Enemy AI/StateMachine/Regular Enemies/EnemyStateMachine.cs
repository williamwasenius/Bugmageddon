using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    // Public Variables
    public float sightRange = 50f;
    public float wanderSpeed = 5f;
    public float chaseSpeed = 10f;
    public Transform enemyPosition;
    public Vector3 TargetPosition;

    public bool isRanged;
    public GameObject projectile;
    public Transform shootingPoint;
    private Collider detectionTrigger;

    public bool isDetonator;
    public float detonatorDamage = 500;
    public float explosionRadius = 5;
    public GameObject explosion;
    public GameObject objective;

    // State Variables
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public IEnemyStates currentState;
    [HideInInspector] public WanderState wanderState;
    [HideInInspector] public NavMeshAgent navMeshAgent;

    // Wander Behavior Variables
    [SerializeField] public float wanderRadius = 10f;
    [SerializeField] public float wanderTimer = 5f;

    // Attack Behavior Variables
    [HideInInspector] public AttackState attackState;
    [SerializeField] public float attackRange = 15f;
    [SerializeField] public float followRange = 25f;

    // Unity Methods

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        attackState = new AttackState(this);
        wanderState = new WanderState(this, wanderRadius, wanderTimer);

        currentState = wanderState;
    }

    private void Start()
    {
        detectionTrigger = GetComponentInChildren<Collider>();
        objective = GameObject.FindGameObjectWithTag("Objective");
    }

    private void Update()
    {
        currentState.UpdateState();
    }

    // Trigger & Collision Handlers

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    public void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(collision);
    }
}
