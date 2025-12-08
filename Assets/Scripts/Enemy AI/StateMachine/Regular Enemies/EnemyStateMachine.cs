using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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

    public bool isCharging;
    public float chargeRechargeTimer;

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
            if (!enemyCS.coreStats.isPreplaced && chaseTarget == null)
            {
                chaseTarget = GameObject.FindGameObjectWithTag("Player").transform;
                ChangeState(chaseState);
            }
            else
            {
                ChangeState(wanderState);
            }
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
            if (chargeRechargeTimer >= 0 && !isCharging)
            {
                chargeRechargeTimer -= Time.deltaTime;
            }
        }
    }

    public void OnHit(GameObject attacker)
    {
        if (!attacker.CompareTag("Enemy") && chaseTarget == null)
        {
            chaseTarget = attacker.transform;
            ChangeState(chaseState);
            AlertNearbyAllies(attacker);
        }
        else
        {
            
        }
    }

   private void AlertNearbyAllies(GameObject attacker)
    {
        int enemyMask = LayerMask.GetMask("Enemy");
        Collider[] allies = Physics.OverlapSphere(gameObject.transform.position, 30, enemyMask);

        foreach (var ally in allies)
        {
            if (ally.TryGetComponent(out EnemyStateMachine sm))
            {
                sm.OnAllyAlert(attacker);
            }
        }
    }

    private void OnAllyAlert(GameObject newTarget)
    {
        chaseTarget = newTarget.transform;
        ChangeState(chaseState);
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
