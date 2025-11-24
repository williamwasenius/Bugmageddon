using UnityEngine;
using UnityEngine.AI;

public class WanderState : IEnemyStates
{
    private readonly EnemyStateMachine enemySM;
    private readonly EnemyCore enemyCS;
    private readonly EnemyCoreStatsSO coreStats;

    private float wanderTimer;
    private float timer;

    public WanderState(EnemyStateMachine stateMachine, float radius, float initialWanderTime)
    {
        enemySM = stateMachine;
        enemyCS = stateMachine.enemyCS;
        coreStats = enemyCS.coreStats;

        wanderTimer = initialWanderTime;
        timer = wanderTimer;
    }

    // ------------------ ENTER / EXIT ------------------ //

    public void EnterState()
    {
        enemySM.nMAgent.isStopped = false;
        enemySM.nMAgent.speed = coreStats.wanderSpeed;
    }

    public void ExitState() { }

    // ------------------ UPDATE ------------------ //

    public void UpdateState()
    {
        Wander();
        LookForTarget();
    }

    // ------------------ WANDER LOGIC ------------------ //

    private void Wander()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(enemySM.transform.position, coreStats.wanderRadius);
            enemySM.nMAgent.SetDestination(newPos);

            timer = 0f;
            wanderTimer = Random.Range(coreStats.wanderIntervals * 0.5f, coreStats.wanderIntervals * 1.5f);
        }
    }

    // ------------------ DETECTION ------------------ //

    private void LookForTarget()
    {
        Ray ray = new Ray(enemySM.transform.position + Vector3.up, enemySM.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * coreStats.sightRange, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, coreStats.sightRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                enemySM.chaseTarget = hit.transform;
                ToChaseState();
            }
        }
    }

    // ------------------ TRANSITIONS ------------------ //

    private void ToChaseState()
    {
        enemySM.ChangeState(enemySM.chaseState);
    }

    // ------------------ UTILITIES ------------------ //

    private static Vector3 RandomNavSphere(Vector3 origin, float dist)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist + origin;
        NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, NavMesh.AllAreas);
        return navHit.position;
    }

    // ------------------ COLLISION CALLBACKS ------------------ //

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player entered");
            enemySM.chaseTarget = other.transform;
            ToChaseState();
        }
    }

    public void OnTriggerExit(Collider other) { }
    public void Ontriggerstay(Collider other) { }
    public void OnCollisionEnter(Collision collision) { }
    public void OnCollisionEnter(Collider other) { }
}
