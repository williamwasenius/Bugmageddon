using UnityEngine;
using UnityEngine.AI;

public class BursterState : IEnemyStates
{
    private readonly EnemyStateMachine enemySM;
    private readonly EnemyCore enemyCS;

    public BursterState(EnemyStateMachine stateMachine)
    {
        enemySM = stateMachine;
        enemyCS = stateMachine.enemyCS;
    }

    // ----------------------------------- ENTER / EXIT ----------------------------------- //

    public void EnterState()
    {
        Debug.Log("Burster state");
        SeekObjective();
    }

    public void ExitState() { }

    // ----------------------------------- UPDATE ----------------------------------- //

    public void UpdateState()
    {

    }

    // -------------------------------- BURSTER SPECIFIC LOGIC -------------------------------- //

    private void SeekObjective()
    {
            GameObject targetPoint = GameObject.FindGameObjectWithTag("TargetPoint");

            if (targetPoint == null)
                targetPoint = GameObject.FindGameObjectWithTag("Player");

            enemySM.chaseTarget = targetPoint.transform;
            enemySM.nMAgent.destination = enemySM.chaseTarget.position;
    }

    // ----------------------------------- TRANSITIONS ----------------------------------- //



    // ----------------------------------- COLLISIONS ----------------------------------- //
    public void OnTriggerEnter(Collider other) { }
    public void OnTriggerExit(Collider other) { }
    public void Ontriggerstay(Collider other) { }
    public void OnCollisionEnter(Collider other) 
    {
        if (other.CompareTag("Objective") || other.CompareTag("Player"))
        {
            enemyCS.Die();
        }
    }
}
