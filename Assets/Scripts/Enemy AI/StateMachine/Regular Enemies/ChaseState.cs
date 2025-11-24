using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IEnemyStates
{
    private readonly EnemyStateMachine EnemySM;
    private readonly EnemyCore EnemyCS;

    public ChaseState(EnemyStateMachine enemyStateMachine)
    {
        EnemySM = enemyStateMachine;
        EnemyCS = enemyStateMachine.EnemyCS;
    }

    // ----------------------------------- ENTER / EXIT ----------------------------------- //

    public void EnterState()
    {
        EnemySM.navMeshAgent.isStopped = false;
        EnemySM.navMeshAgent.speed = EnemyCS.chaseSpeed;
    }

    public void ExitState() { }

    // ----------------------------------- UPDATE ----------------------------------- //

    public void UpdateState()
    {
        if (EnemySM.chaseTarget == null)
        {
            ToWanderState();
            return;
        }

        UpdateMovement();
        CheckAttackRange();
    }

    // ----------------------------------- CHASE LOGIC ----------------------------------- //

    private void UpdateMovement()
    {
        EnemySM.navMeshAgent.speed = EnemyCS.chaseSpeed;
        EnemySM.navMeshAgent.isStopped = false;

        if (EnemyCS.isBurster)
        {
            EnemySM.navMeshAgent.destination = EnemySM.bursterTargetPoint.transform.position;
        }
        else
        {
            EnemySM.navMeshAgent.destination = EnemySM.chaseTarget.position;
        }
    }

    private void CheckAttackRange()
    {
        float distance = Vector3.Distance(EnemySM.transform.position, EnemySM.chaseTarget.position);

        if (EnemyCS.isSpitter && distance <= EnemyCS.shootRange)
            {
                ToAttackState("Spitter");
            }
        else if (EnemyCS.isCharger && EnemyCS.minChargeRange <= distance && distance <= EnemyCS.chargeRange && EnemySM.currentChargeCooldown <= 0) 
            {
                ToAttackState("Charger");
            }
        else if (!EnemyCS.isSpitter && distance <= EnemyCS.strikeRange)
            {
                ToAttackState("Other");
            }
    }

    // ----------------------------------- TRANSITIONS ----------------------------------- //

    public void ToAttackState(string unit)
    {
        if (unit == "Spitter")
        {
            EnemySM.ChangeState(EnemySM.rangedAttackState);
        }
        else if (unit == "Charger")
        {
            EnemySM.ChangeState(EnemySM.chargerAttackState);
        }
        else
        {
            EnemySM.ChangeState(EnemySM.meleeAttackState);
        }
    }

    public void ToWanderState()
    {
        EnemySM.ChangeState(EnemySM.wanderState);
    }

    // ----------------------------------- COLLISIONS ----------------------------------- //
    public void OnTriggerEnter(Collider other) { }
    public void OnTriggerExit(Collider other) { }
    public void Ontriggerstay(Collider other) { }
    public void OnCollisionEnter(Collider other) { }
}
