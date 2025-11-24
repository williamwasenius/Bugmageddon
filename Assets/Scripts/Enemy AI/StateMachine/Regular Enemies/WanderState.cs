using UnityEngine;
using UnityEngine.AI;

public class WanderState : IEnemyStates
{
    private readonly EnemyStateMachine EnemySM;
    private readonly EnemyCore EnemyCS;

    private float wanderRadius;
    private float wanderTimer;
    private float timer;

    public WanderState(EnemyStateMachine statePatternEnemy, float radius, float initialWanderTime)
    {
        EnemySM = statePatternEnemy;
        EnemyCS = statePatternEnemy.EnemyCS;

        wanderRadius = radius;
        wanderTimer = initialWanderTime;
        timer = wanderTimer;
    }

    // ------------------ ENTER / EXIT ------------------ //

    public void EnterState()
    {

    }

    public void ExitState()
    {

    }

    // ------------------ UPDATE ------------------ //

    public void UpdateState()
    {
        if (EnemyCS.isBurster)
        {
            SeekObjective();
        }
        else
        {
            Wander();
            Look();
        }
    }

    // ------------------ TRANSITIONS ------------------ //

    private void ToAttackState()
    {
        if (EnemyCS.isSpitter)
            EnemySM.ChangeState(EnemySM.rangedAttackState);

        else if (EnemyCS.isCharger)
            EnemySM.ChangeState(EnemySM.chargerAttackState);

        else
            EnemySM.ChangeState(EnemySM.meleeAttackState);
    }

    // ------------------ WANDER / LOOK LOGIC ------------------ //

    private void Look()
    {
        Debug.DrawRay(EnemyCS.modelCenterpoint, EnemySM.transform.forward * EnemyCS.sightRange, Color.red);

        if (Physics.Raycast(EnemyCS.modelCenterpoint, EnemySM.transform.forward, out RaycastHit hit, EnemyCS.sightRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                EnemySM.chaseTarget = hit.transform;
                ToAttackState();
            }
        }
    }

    private void Wander()
    {
        EnemySM.navMeshAgent.speed = EnemyCS.wanderSpeed;
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(EnemySM.transform.position, wanderRadius, -1);
            EnemySM.navMeshAgent.SetDestination(newPos);
            timer = 0f;

            wanderTimer = Random.Range(3f, 7f);
        }
    }

    private void SeekObjective()
    {
        if (EnemySM.bursterTargetPoint != null)
        {
            EnemySM.chaseTarget = EnemySM.bursterTargetPoint.transform;
            ToAttackState();
        }
    }

    // ------------------ UTILITIES ------------------ //

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist + origin;
        NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layermask);
        return navHit.position;
    }

    // ------------------ COLLISIONS ------------------ //

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnemySM.chaseTarget = other.transform;
            EnemySM.ChangeState(EnemySM.chaseState);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (EnemyCS.isBurster && (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Objective")))
        {
            EnemySM.currentState = EnemySM.meleeAttackState;
        }
    }

    public void OnTriggerExit(Collider other) { }
    public void Ontriggerstay(Collider other) { }
    public void OnCollisionEnter(Collider other) { }
}
