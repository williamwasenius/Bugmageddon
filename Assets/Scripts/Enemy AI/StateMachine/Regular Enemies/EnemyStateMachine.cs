using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    public EnemyCore enemy;
    public Animator animator;
    public NavMeshAgent agent;

    private IEnemyStates currentState;

    public WanderState wanderState;
    public ChaseState chaseState;

    public MeleeAttackState meleeState;
    public RangedAttackState rangedState;
    public ChargerAttackState chargerState;

    private void Awake()
    {
        enemy = GetComponent<EnemyCore>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        wanderState = new WanderState(this, enemy.coreStats.wanderRadius, enemy.coreStats.wanderIntervals);
        chaseState = new ChaseState(this);

        if (enemy.meleeStats) meleeState = new MeleeAttackState(this);
        if (enemy.rangedStats) rangedState = new RangedAttackState(this);
        if (enemy.chargerStats) chargerState = new ChargerAttackState(this);
    }

    private void Start()
    {
        ChangeState(wanderState);
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
        animator.SetBool("IsMoving", agent.velocity.magnitude >= 0.1f);
    }
}
