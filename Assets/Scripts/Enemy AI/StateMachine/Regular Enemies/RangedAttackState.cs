using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class RangedAttackState : IEnemyStates
{
    private EnemyStateMachine enemy;
    private bool isAttacking = false;

    private float attackAnimationDuration = 1.5f;

    public RangedAttackState(EnemyStateMachine statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        if (enemy.chaseTarget == null)
        {
            ToWanderState();
            return;
        }

        float distance = Vector3.Distance(enemy.transform.position, enemy.chaseTarget.position);

        Vector3 direction = (enemy.chaseTarget.position - enemy.transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        if (distance <= enemy.attackRange)
        {
            enemy.navMeshAgent.isStopped = true;

            if (!isAttacking)
                enemy.StartCoroutine(AttackLoop());
        }
        else
        {
            enemy.navMeshAgent.isStopped = false;
            enemy.navMeshAgent.destination = enemy.chaseTarget.position;
            enemy.animator.SetBool("IsAttacking", false);
        }
    }

    private IEnumerator AttackLoop()
    {
        isAttacking = true;

        while (enemy.chaseTarget != null)
        {
            float distance = Vector3.Distance(enemy.transform.position, enemy.chaseTarget.position);

            if (distance > enemy.attackRange)
                break; 

            enemy.animator.SetBool("IsAttacking", true);

            yield return new WaitForSeconds(attackAnimationDuration);

            enemy.animator.SetBool("IsAttacking", false);

        }

        enemy.animator.SetBool("IsAttacking", false);
        enemy.navMeshAgent.isStopped = false;
        isAttacking = false;
    }

    public void ToWanderState()
    {
        StopAll();
        enemy.currentState = enemy.wanderState;
    }

    private void StopAll()
    {
        enemy.animator.SetBool("IsAttacking", false);
        enemy.navMeshAgent.isStopped = false;
        isAttacking = false;
    }

    public void ToAttackState() { }
    public void OnTriggerEnter(Collider other) { }
    public void OnCollisionEnter(Collision collision) { }
}
