using UnityEngine;
using System.Collections;

public class BossTailAttack : IBossStates
{
    private BossStateMachine BossSM;
    private EnemyCore BossCS;
    public BossTailAttack(BossStateMachine bossStateMachine)
    {
        BossSM = bossStateMachine;
        BossCS = bossStateMachine.BossCS;
    }
    public void EnterState()
    {
        Debug.Log("tail attack state entered");
        BossSM.tailAttackRechargeTime = BossSM.tailAttackCooldown;
        BossSM.StartCoroutine(PerformTailAttack()); 
    }

    public void ExitState()
    {
    }

    public void Phase1()
    {
    }

    public void Phase2()
    {
    }

    public void Phase3()
    {
    }

    public void UpdateState()
    {
    }

    private IEnumerator PerformTailAttack()
    {
        BossSM.animator.SetBool("IsTailAttacking", true);

        BossSM.navMeshAgent.updateRotation = false;

        BossSM.tailAttackContainer.SetActive(true);

        BossSM.tailAttackContainer.transform.position = BossSM.chaseTargetPosition.transform.position;

        yield return new WaitForSeconds(11);

        BossSM.tailAttackContainer.SetActive(false);

        BossSM.animator.SetBool("IsTailAttacking", false);

        BossSM.navMeshAgent.updateRotation = true;

        BossSM.ChangeState(BossSM.phase1);

    }


}
