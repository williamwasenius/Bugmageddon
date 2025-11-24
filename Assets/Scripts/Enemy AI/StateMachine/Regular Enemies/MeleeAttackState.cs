using UnityEngine;
using System.Collections;

public class MeleeAttackState : IEnemyStates
{
    private readonly EnemyStateMachine EnemySM;
    private readonly EnemyCore EnemyCS;
    private float attackCooldown;
    private bool isAttacking;

    public MeleeAttackState(EnemyStateMachine EnemyStateMachine)
    {
        EnemySM = EnemyStateMachine;
        EnemyCS = EnemyStateMachine.EnemyCS;
    }

    // ----------------------------------- ENTER / EXIT ----------------------------------- //

    public void EnterState()
    {
        if (EnemyCS.isBurster)
        {
            Burst();
        }
        else
        {
            TryAttack();
        }
    }

    public void ExitState()
    {
        StopAttack();
    }

    // ----------------------------------- UPDATE ----------------------------------- //

    public void UpdateState()
    {
        if (!isAttacking && attackCooldown != 0)
        {
            attackCooldown -= Time.deltaTime;

            if (attackCooldown <= 0)
            {
                TryAttack();
            }

        }

        if (EnemySM.chaseTarget != null && Vector3.Distance(EnemySM.transform.position, EnemySM.chaseTarget.position) > EnemyCS.strikeRange)
        {
            ToChaseState();
        }

        else if (EnemySM.chaseTarget == null)
        {
            ToWanderState();
            return;
        }
    }

    // ----------------------------------- ATTACK LOGIC ----------------------------------- //

    private void TryAttack()
    {
        isAttacking = true;
        attackCooldown = EnemyCS.attackSpeed;

        Vector3 direction = EnemySM.chaseTarget.position - EnemySM.transform.position;
        direction.y = 0;
        EnemySM.transform.rotation = Quaternion.LookRotation(direction);

        EnemySM.navMeshAgent.isStopped = true;
        EnemySM.StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {

        Debug.Log("Attack start");
        EnemySM.animator.SetBool("IsAttacking", true);

        yield return new WaitForSeconds(EnemyCS.attackDuration);

        EnemySM.animator.SetBool("IsAttacking", false);
        isAttacking = false;
        Debug.Log("Attack end");

    }

    private void StopAttack()
    {
        EnemySM.animator.SetBool("IsAttacking", false);
        EnemySM.navMeshAgent.isStopped = false;
        isAttacking = false;
    }

    private void Burst()
    {
        EnemyCS.Die();
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

    // ----------------------------------- UNUSED CALLBACKS ----------------------------------- //

    public void OnTriggerEnter(Collider other) { }
    public void OnTriggerExit(Collider other) { }
    public void Ontriggerstay(Collider other) { }
    public void OnCollisionEnter(Collision collision) { }
    public void OnCollisionEnter(Collider other) { }
}
