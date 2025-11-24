using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour 
{
    private NavMeshAgent agent;

    public EnemyMovement(NavMeshAgent agent) => this.agent = agent;

    public void MoveTo(Vector3 pos, float speed)
    {
        agent.speed = speed;
        agent.isStopped = false;
        agent.SetDestination(pos);
    }

    public void Stop() => agent.isStopped = true;
}
