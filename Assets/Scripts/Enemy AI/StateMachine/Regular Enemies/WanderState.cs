using UnityEngine;
using UnityEngine.AI;

public class WanderState : IEnemyStates
{
    private EnemyStateMachine enemy;
    private float wanderRadius;
    private float wanderTimer;
    private float timer;
    private float timeToNextWander; // Random time until the next wander

    public WanderState(EnemyStateMachine statePatternEnemy, float radius, float initialWanderTime)
    {
        enemy = statePatternEnemy;
        wanderRadius = radius;
        wanderTimer = initialWanderTime;
        this.timer = wanderTimer;

        timeToNextWander = Random.Range(1f, 6f);
    }

    public void ToWanderState()
    {

    }

    public void ToAttackState()
    {
        enemy.currentState = enemy.attackState;
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
            Debug.Log("player entered");
            enemy.chaseTarget = other.transform;
            ToAttackState();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {

    }

    void Look()
    {
        Debug.DrawRay(enemy.enemyPosition.position, enemy.enemyPosition.forward * enemy.sightRange, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(enemy.enemyPosition.position, enemy.enemyPosition.forward, out hit, enemy.sightRange))
        {
            if (hit.collider.CompareTag("Player"))
                {
                    enemy.chaseTarget = hit.transform;
                       ToAttackState();

                }
        }
    }

    void Wander()
    {
        enemy.navMeshAgent.speed = enemy.wanderSpeed;

        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(enemy.transform.position, wanderRadius, -1);
            enemy.navMeshAgent.SetDestination(newPos);
            timer = 0;

            wanderTimer = Random.Range(3f, 7f);
        }
    }

    void SeekObjective()
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
