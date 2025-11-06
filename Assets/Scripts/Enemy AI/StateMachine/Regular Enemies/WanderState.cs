using UnityEngine;
using UnityEngine.AI;

public class WanderState : IEnemyStates
{
    private EnemyStateMachine enemy;
    private float wanderRadius;
    private float wanderTimer;
    private float timer;

    public WanderState(EnemyStateMachine statePatternEnemy, float radius, float initialWanderTime)
    {
        enemy = statePatternEnemy;
        wanderRadius = radius;
        wanderTimer = initialWanderTime;
        timer = wanderTimer;
    }

    public void ToWanderState() { }

    public void ToAttackState()
    {
        if (enemy.isDetonator)
        {
            enemy.currentState = enemy.meleeAttackState;
        }
        else if (enemy.isRanged)
        {
            enemy.currentState = enemy.rangedAttackState;
        }
        else
        {
            enemy.currentState = enemy.meleeAttackState;
        }
    }

    public void UpdateState()
    {
        if (enemy.isDetonator)
        {
            SeekObjective();
        }
        else
        {
            Wander();
            Look();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.chaseTarget = other.transform;
            ToAttackState();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (enemy.isDetonator && (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Objective")))
        {
            enemy.currentState = enemy.meleeAttackState; 
        }
    }

    private void Look()
    {
        Debug.DrawRay(enemy.currentPosition.position, enemy.currentPosition.forward * enemy.sightRange, Color.red);
        if (Physics.Raycast(enemy.currentPosition.position, enemy.currentPosition.forward, out RaycastHit hit, enemy.sightRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                enemy.chaseTarget = hit.transform;
                ToAttackState();
            }
        }
    }

    private void Wander()
    {
        enemy.navMeshAgent.speed = enemy.wanderSpeed;

        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(enemy.transform.position, wanderRadius, -1);
            enemy.navMeshAgent.SetDestination(newPos);
            timer = 0f;

            wanderTimer = Random.Range(3f, 7f);
        }
    }

    private void SeekObjective()
    {
        if (enemy.objective != null)
        {
            enemy.chaseTarget = enemy.objective.transform;
            ToAttackState();
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}
