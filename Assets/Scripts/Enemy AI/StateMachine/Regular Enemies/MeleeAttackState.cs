using UnityEngine;
using System.Collections;

public class MeleeAttackState : IEnemyStates
{
    private readonly EnemyStateMachine enemySM;
    private readonly EnemyCore enemy;
    private readonly EnemyMeleeStatsSO meleeStatsSO;

    private bool isAttacking;
    private float cooldown;

    public MeleeAttackState(EnemyStateMachine stateMachine)
    {
        enemySM = stateMachine;
        enemy = stateMachine.enemyCS;
        meleeStatsSO = enemy.meleeStats;
    }

    // ----------------------------------- ENTER / EXIT ----------------------------------- //

    public void EnterState()
    {
        if (enemy.bursterStats != null)
        {
            enemy.Die();
            return;
        }

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

        float distance = Vector3.Distance(enemySM.transform.position, enemySM.chaseTarget.position);

        if (distance > meleeStatsSO.strikeRange && !isAttacking)
        {
            ToChaseState();
            return;
        }

        if (!isAttacking && cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0) TryAttack();
        }
    }

    // ----------------------------------- ATTACK LOGIC ----------------------------------- //

    private void TryAttack()
    {
        if (isAttacking) return;

        isAttacking = true;
        cooldown = meleeStatsSO.attackSpeed;

        FaceTarget();

        enemySM.nMAgent.isStopped = true;
        enemySM.StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        enemySM.animator.SetBool("IsAttacking", true);

        yield return new WaitForSeconds(meleeStatsSO.attackDuration);

        enemySM.animator.SetBool("IsAttacking", false);
        isAttacking = false;
    }

    private void StopAttack()
    {
        enemySM.animator.SetBool("IsAttacking", false);
        enemySM.nMAgent.isStopped = false;
        isAttacking = false;
    }

    private void FaceTarget()
    {
        Vector3 dir = enemySM.chaseTarget.position - enemySM.transform.position;
        dir.y = 0;

        if (dir != Vector3.zero)
            enemySM.transform.rotation = Quaternion.LookRotation(dir);
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
