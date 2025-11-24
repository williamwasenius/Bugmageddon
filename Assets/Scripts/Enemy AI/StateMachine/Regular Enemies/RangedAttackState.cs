using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class RangedAttackState : IEnemyStates
{
    private readonly EnemyStateMachine enemySM;
    private readonly EnemyCore enemy;
    private readonly EnemyRangedStatsSO rangedStatsSO;

    private bool isAttacking;

    public RangedAttackState(EnemyStateMachine stateMachine)
    {
        enemySM = stateMachine;
        enemy = stateMachine.enemyCS;
        rangedStatsSO = enemy.rangedStats;
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
        if (enemySM.chaseTarget == null)
        {
            ToWanderState();
            return;
        }

        RotateTowardsTarget();

        float distance = Vector3.Distance(enemySM.transform.position, enemySM.chaseTarget.position);

        if (distance > rangedStatsSO.shootRange && !isAttacking)
        {
            ToChaseState();
        }
    }

    // ----------------------------------- ATTACK LOGIC ----------------------------------- //

    private void TryAttack()
    {
        if (isAttacking) return;

        enemySM.nMAgent.isStopped = true;
        enemySM.StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;

        while (enemySM.chaseTarget != null &&
               Vector3.Distance(enemySM.transform.position, enemySM.chaseTarget.position) <= rangedStatsSO.shootRange)
        {
            enemySM.animator.SetBool("IsAttacking", true);

            yield return new WaitForSeconds(rangedStatsSO.ShootDuration);

            enemySM.animator.SetBool("IsAttacking", false);
        }

        ToChaseState();
    }

    private void StopAttack()
    {
        enemySM.animator.SetBool("IsAttacking", false);
        enemySM.nMAgent.isStopped = false;
        isAttacking = false;
    }

    // ----------------------------------- ROTATION ----------------------------------- //

    private void RotateTowardsTarget()
    {
        Vector3 dir = (enemySM.chaseTarget.position - enemySM.transform.position).normalized;
        dir.y = 0;

        if (dir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(dir);
            enemySM.transform.rotation = Quaternion.Slerp(enemySM.transform.rotation, rot, Time.deltaTime * 7f);
        }
    }

    // ----------------------------------- TRANSITIONS ----------------------------------- //

    private void ToChaseState()
    {
        enemySM.ChangeState(enemySM.chaseState);
    }

    public void ToWanderState()
    {
        enemySM.ChangeState(enemySM.wanderState);
    }

    // ----------------------------------- UNUSED CALLBACKS ----------------------------------- //
    public void OnTriggerEnter(Collider other) { }
    public void OnTriggerExit(Collider other) { }
    public void Ontriggerstay(Collider other) { }
    public void OnCollisionEnter(Collision collision) { }
    public void OnCollisionEnter(Collider other) { }
}
