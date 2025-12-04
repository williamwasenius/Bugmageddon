using UnityEngine;
using System.Collections;


public class BossSummonGuardsAbility : IBossStates
{
    private BossStateMachine BossSM;
    private EnemyCore BossCS;
    public BossSummonGuardsAbility(BossStateMachine bossStateMachine)
    {
        BossSM = bossStateMachine;
        BossCS = bossStateMachine.BossCS;
    }
    public void EnterState()
    {
        BossSM.callGuardsRechargeTime = BossSM.callGuardsCooldown;
        BossSM.StartCoroutine(SummonGuards());
        BossSM.animator.SetBool("IsRoaring", true);
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
    private IEnumerator SummonGuards()
    {
        float duration = BossSM.animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(duration);
        BossSM.animator.SetBool("IsRoaring", false);
        BossSM.ChangeState(BossSM.chaseState);

    }



}
