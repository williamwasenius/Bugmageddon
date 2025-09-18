    using UnityEngine;
    using UnityEngine.AI;
    using Unity.VisualScripting;

public class AttackState : IEnemyStates
{
    private EnemyStateMachine enemy;
    private Enemy enemyCore;
    private float attackCooldown = 1f; 
    private float nextAttackTime = 0f;

    public AttackState(EnemyStateMachine statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void ToWanderState()
    {
        enemy.currentState = enemy.wanderState;
    }

    public void ToAttackState()
    {

    }

    public void OnTriggerEnter(Collider other)
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (enemy.isDetonator && (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Objective")))
        {
            Detonate();
        }
    }

    public void UpdateState()
        {
            AttackTarget();

            if (enemy.isDetonator == false)
            {
                Look();
            }
        }

    void Look()
    {
        Debug.DrawRay(enemy.enemyPosition.position, enemy.enemyPosition.forward * enemy.sightRange, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(enemy.enemyPosition.position, enemy.enemyPosition.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
        }
    }

    private void AttackTarget()
        {
            float distanceToTarget = Vector3.Distance(enemy.transform.position, enemy.chaseTarget.position);

            if (enemy.isRanged)
            {
                Vector3 directionToPlayer = (enemy.chaseTarget.position - enemy.transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 5f);

                if (distanceToTarget > enemy.attackRange)
                        {
                           
                            enemy.navMeshAgent.speed = enemy.chaseSpeed;
                            enemy.navMeshAgent.destination = enemy.chaseTarget.position;
                        }   
                        else
                        {
                            ShootAtTarget();
                        }
            }


            else 
            {
                 enemy.navMeshAgent.speed = enemy.chaseSpeed;
                 enemy.navMeshAgent.destination = enemy.chaseTarget.position;
            }
        }

        void ShootAtTarget()
        {
            if (Time.time >= nextAttackTime)
            {

                GameObject.Instantiate(enemy.projectile, enemy.shootingPoint.position, enemy.shootingPoint.rotation);

                nextAttackTime = Time.time + attackCooldown;
            }
        }

    private void Detonate()
    {
        GameObject explosion = GameObject.Instantiate(enemy.explosion, enemy.transform.position, Quaternion.identity);

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

        enemyCore.Die();

    }

}
