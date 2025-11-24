using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BossPhase1 : IBossStates
{
    private BossStateMachine BossSM;
    private EnemyCore BossCS;
    public BossPhase1(BossStateMachine bossStateMachine)
    {
        BossSM = bossStateMachine;
        BossCS = bossStateMachine.BossCS;
    }

    public void EnterState()
    {
        Debug.Log("chase state entered");
        BossSM.navMeshAgent.isStopped = false;
    }

    public void ExitState()
    {
        BossSM.StopMoving();
    }

    public void Phase1()
    {

    }

    public void UpdateState()
    {
        if (BossSM.tailAttackRechargeTime <= 0)
        {
            BossSM.ChangeState(BossSM.bossTailAttack);

        }
        else
        {
            CheckAttackRange();

            BossSM.animator.SetBool("IsMoving", BossSM.velocity.magnitude >= 0.1f);
        }

    }

    public void CheckAttackRange()
    {
        float distance = Vector3.Distance(BossSM.transform.position, BossSM.chaseTargetPosition.position);

        if (distance <= BossCS.meleeStats.strikeRange)
        {
            BossSM.ChangeState(BossSM.bossArmAttack);
        }
        else
        {
            ChaseTarget();
        }
    }
    public void ChaseTarget()
    {
        BossSM.navMeshAgent.destination = BossSM.chaseTargetPosition.position;
    }

}
