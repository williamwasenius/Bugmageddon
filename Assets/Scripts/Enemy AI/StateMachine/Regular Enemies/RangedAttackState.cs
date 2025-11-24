using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class RangedAttackState : IEnemyStates
{
    private readonly EnemyStateMachine EnemySM;
    private readonly EnemyCore EnemyCS;
    private bool isAttacking;

    public RangedAttackState(EnemyStateMachine EnemyStateMachine)
    {
        EnemySM = EnemyStateMachine;
        EnemyCS = EnemyStateMachine.EnemyCS;
    }

    // ----------------------------------- ENTER / EXIT ----------------------------------- //

    public void EnterState()
    {
        TryAttack();
    }

    public void ExitState()
    {
        StopAttack();
    }

    // ----------------------------------- UPDATE ----------------------------------- //

    public void UpdateState()
    {
        if (EnemySM.chaseTarget == null)
        {
            ToWanderState();
            return;
        }

        float distance = Vector3.Distance(EnemySM.transform.position, EnemySM.chaseTarget.position);

        RotateTowardsTarget();

        if (distance > EnemyCS.shootRange)
        {
            ToChaseState();
        }
    }

    // ----------------------------------- ATTACK LOGIC ----------------------------------- //

    private void TryAttack()
    {
        if (isAttacking) return;
        EnemySM.navMeshAgent.isStopped = true;
        EnemySM.StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;

        while (EnemySM.chaseTarget != null &&
               Vector3.Distance(EnemySM.transform.position, EnemySM.chaseTarget.position) <= EnemyCS.shootRange)
        {
            EnemySM.animator.SetBool("IsAttacking", true);

            yield return new WaitForSeconds(EnemyCS.attackSpeed);

            EnemySM.animator.SetBool("IsAttacking", false);

        }

        ToChaseState();
    }

    private void StopAttack()
    {
        EnemySM.animator.SetBool("IsAttacking", false);
        EnemySM.navMeshAgent.isStopped = false;
        isAttacking = false;
    }

    // ----------------------------------- TRANSITIONS ----------------------------------- //

    private void ToChaseState()
    {
        EnemySM.ChangeState(EnemySM.chaseState);
    }

    public void ToWanderState()
    {
        EnemySM.ChangeState(EnemySM.wanderState);
    }

    // ----------------------------------- ROTATION / EVENTS ----------------------------------- //

    private void RotateTowardsTarget()
    {
        Vector3 dir = (EnemySM.chaseTarget.position - EnemySM.transform.position).normalized;
        if (dir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(dir);
            EnemySM.transform.rotation = Quaternion.Slerp(EnemySM.transform.rotation, rot, Time.deltaTime * 5f);
        }
    }

    public void OnTriggerEnter(Collider other) { }
    public void OnTriggerExit(Collider other) { }
    public void Ontriggerstay(Collider other) { }
    public void OnCollisionEnter(Collision collision) { }
    public void OnCollisionEnter(Collider other) { }

}
