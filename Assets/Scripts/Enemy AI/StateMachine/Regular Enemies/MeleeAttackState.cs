using UnityEngine;

public class MeleeAttackState : IEnemyStates
{
    private EnemyStateMachine enemy;
    private bool isAttacking = false;

    public MeleeAttackState(EnemyStateMachine statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        if (enemy.chaseTarget == null)
        {
            enemy.navMeshAgent.speed = enemy.wanderSpeed;
            ToWanderState();
            return;
        }

        if (isAttacking) return;

        enemy.navMeshAgent.speed = enemy.chaseSpeed;
        enemy.navMeshAgent.isStopped = false;
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (isAttacking) return;

        else if (enemy.isDetonator && collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Objective"))
        {
            Collider[] hitColliders = Physics.OverlapSphere(enemy.transform.position, enemy.explosionRadius);

            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.isTrigger)
                    continue;

                IDamageable targetDamageable = hitCollider.GetComponent<IDamageable>();
                if (targetDamageable != null)
                {
                    targetDamageable.TakeDamage(enemy.detonatorDamage - targetDamageable.Armor);
                }
            }
            enemy.enemyCoreScript.Die();
        }
        else if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Objective"))
        {
            StartAttack();
        }
    }

    private void StartAttack()
    {
        isAttacking = true;
        enemy.navMeshAgent.isStopped = true;
        enemy.animator.SetBool("IsAttacking", true);
        enemy.StartCoroutine(ResetAfterAttack());
    }

    private System.Collections.IEnumerator ResetAfterAttack()
    {
        float attackLength = enemy.animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(attackLength);

        enemy.animator.SetBool("IsAttacking", false);
        enemy.navMeshAgent.isStopped = false;
        isAttacking = false;
    }

    public void ToWanderState()
    {
        if (isAttacking) return;
        enemy.animator.SetBool("IsAttacking", false);
        enemy.navMeshAgent.isStopped = false;
        enemy.currentState = enemy.wanderState;
    }

    public void ToAttackState() { }
    public void OnTriggerEnter(Collider other) { }
}
