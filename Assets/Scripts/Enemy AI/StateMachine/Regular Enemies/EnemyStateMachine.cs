using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    public EnemyCore enemyCS;
    public Animator animator;
    public NavMeshAgent nMAgent;

    public Transform chaseTarget;

    private IEnemyStates currentState;

    public WanderState wanderState;
    public ChaseState chaseState;

    public MeleeAttackState meleeState;
    public RangedAttackState rangedState;
    public ChargerAttackState chargerState;
    public BursterState bursterState;

    private void Awake()
    {
        enemyCS = GetComponent<EnemyCore>();
        nMAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        wanderState = new WanderState(this, enemyCS.coreStats.wanderRadius, enemyCS.coreStats.wanderIntervals);
        chaseState = new ChaseState(this);

        if (enemyCS.meleeStats) meleeState = new MeleeAttackState(this);
        if (enemyCS.rangedStats) rangedState = new RangedAttackState(this);
        if (enemyCS.chargerStats) chargerState = new ChargerAttackState(this);
        if (enemyCS.bursterStats) bursterState = new BursterState(this);

    }

    private void Start()
    {
        if (enemyCS.bursterStats)
        {
            ChangeState(bursterState);
        }
        else
        {
            if (!enemyCS.coreStats.isPreplaced)
            {
                chaseTarget = GameObject.FindGameObjectWithTag("Player").transform;
                ChangeState(chaseState);
            }
            ChangeState(wanderState);
        }
    }

    public void ChangeState(IEnemyStates newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState?.EnterState();
    }

    private void Update()
    {
        currentState?.UpdateState();
        animator.SetBool("IsMoving", nMAgent.velocity.magnitude >= 0.1f);

        if (enemyCS.chargerStats != null)
        {
            if (enemyCS.chargerStats.chargeRechargeTimer < 0 && !enemyCS.chargerStats.isCharging)
            {
                enemyCS.chargerStats.chargeRechargeTimer -= Time.deltaTime;
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }
    public void OnTriggerExit(Collider other) { }
    public void Ontriggerstay(Collider other) { }
    public void OnCollisionEnter(Collision collision) 
    { 
        currentState.OnCollisionEnter(collision.collider);
    }
}
