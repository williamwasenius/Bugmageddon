using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IEnemyStates
{
    private readonly EnemyStateMachine enemySM;
    private readonly EnemyCore enemyCS;

    public ChaseState(EnemyStateMachine stateMachine)
    {
        enemySM = stateMachine;
        enemyCS = stateMachine.enemyCS;
    }

    // ----------------------------------- ENTER / EXIT ----------------------------------- //

    public void EnterState()
    {
        enemySM.nMAgent.isStopped = false;
        enemySM.nMAgent.speed = enemyCS.coreStats.chaseSpeed;
    }

    public void ExitState() { }

    // ----------------------------------- UPDATE ----------------------------------- //

    public void UpdateState()
    {
        if (enemySM.chaseTarget == null)
        {
            ToWanderState();
            return;
        }

        UpdateMovement();
        TryAttack();
    }

    // ----------------------------------- CHASE LOGIC ----------------------------------- //

    private void UpdateMovement()
    {
        enemySM.nMAgent.speed = enemyCS.coreStats.chaseSpeed;
        enemySM.nMAgent.isStopped = false;
        enemySM.nMAgent.destination = enemySM.chaseTarget.position;
    }

    private void TryAttack()
    {
        float distance = Vector3.Distance(enemySM.transform.position, enemySM.chaseTarget.position);

        if (enemyCS.chargerStats && CanCharge(distance))
        {
            enemySM.ChangeState(enemySM.chargerState);
        }
        else if (enemyCS.rangedStats && distance <= enemyCS.rangedStats.shootRange)
        {
            enemySM.ChangeState(enemySM.rangedState);
        }
        else if (enemyCS.meleeStats && distance <= enemyCS.meleeStats.strikeRange)
        {
            enemySM.ChangeState(enemySM.meleeState);
        }
    }

    private bool CanCharge(float distance)
    {
        return distance >= enemyCS.chargerStats.minChargeRange &&
               distance <= enemyCS.chargerStats.chargeRange &&
               enemyCS.chargerStats.chargeRechargeTimer <= 0;
    }

    // ----------------------------------- TRANSITIONS ----------------------------------- //

    public void ToWanderState()
    {
        enemySM.ChangeState(enemySM.wanderState);
    }

    // ----------------------------------- COLLISIONS ----------------------------------- //
    public void OnTriggerEnter(Collider other) { }
    public void OnTriggerExit(Collider other) { }
    public void Ontriggerstay(Collider other) { }
    public void OnCollisionEnter(Collider other) { }
}
