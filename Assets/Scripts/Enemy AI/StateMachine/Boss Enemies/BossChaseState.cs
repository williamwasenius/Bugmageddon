using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BossChaseState : IBossStates
{
    private BossStateMachine BossSM;
    private EnemyCore BossCS;
    public BossChaseState(BossStateMachine bossStateMachine)
    {
        BossSM = bossStateMachine;
        BossCS = bossStateMachine.BossCS;
    }

    public void EnterState()
    {
        Debug.Log("chase state entered");
        BossSM.UnlockMovement();
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
        if (BossSM.animator.GetBool("IsRoaring"))
        {
            return;
        }

        else if (BossSM.tailAttackRechargeTime <= 0)
        {
            BossSM.ChangeState(BossSM.bossTailAttack);

        }
        else if (BossSM.currentPhaseINT == 2 && BossSM.callGuardsRechargeTime <= 0)
        {
            BossSM.ChangeState(BossSM.bossSummonGuardsAbility);
        }
        else
        {
            BossSM.CheckAttackRange();

            BossSM.animator.SetBool("IsMoving", BossSM.velocity.magnitude >= 0.1f);
        }

    }

}
