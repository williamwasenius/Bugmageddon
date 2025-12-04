using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BossDeathState : IBossStates
{
    private BossStateMachine BossSM;
    private EnemyCore BossCS;
    public BossDeathState(BossStateMachine bossStateMachine)
    {
        BossSM = bossStateMachine;
        BossCS = bossStateMachine.BossCS;
    }

    public void EnterState()
    {
        Debug.Log("death state entered");
        BossSM.navMeshAgent.isStopped = true;
        BossSM.navMeshAgent.updateRotation = false;
        BossSM.StartCoroutine(BossSM.DeathThroes());
    }

    public void ExitState()
    {

    }

    public void Phase1()
    {

    }

    public void UpdateState()
    {

    }

}
