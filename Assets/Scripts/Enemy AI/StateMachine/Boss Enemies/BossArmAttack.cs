using UnityEngine;
using System.Collections;

public class BossArmAttack : IBossStates
{
    private BossStateMachine BossSM;
    private EnemyCore BossCS;
    public BossArmAttack(BossStateMachine bossStateMachine)
    {
        BossSM = bossStateMachine;
        BossCS = bossStateMachine.BossCS;
    }
    public void EnterState()
    {
        Debug.Log("arm attack state entered");
        BossSM.StartCoroutine(ArmAttack());
    }

    public void ExitState()
    {
    }

    public void UpdateState()
    {
    }

    private IEnumerator ArmAttack()
    {
        Debug.Log("Attack start");
        BossSM.animator.SetBool("IsArmAttacking", true);

        yield return new WaitForSeconds(BossCS.attackDuration);

        BossSM.animator.SetBool("IsArmAttacking", false);
        Debug.Log("Attack end");

        BossSM.ChangeState(BossSM.phase1);

    }

}
