using UnityEngine;

public class BossPhase2 : IBossStates
{
    private BossStateMachine BossSM;
    private EnemyCore BossCS;
    public BossPhase2(BossStateMachine bossStateMachine)
    {
        BossSM = bossStateMachine;
        BossCS = bossStateMachine.BossCS;
    }
    public void EnterState()
    {
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
}
