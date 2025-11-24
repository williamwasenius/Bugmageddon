using UnityEngine;
using UnityEngine.AI;
using Unity.VisualScripting;

public class BossPhase3 : IBossStates
{
    private BossStateMachine BossSM;
    private EnemyCore BossCS;
    public BossPhase3(BossStateMachine bossStateMachine)
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
