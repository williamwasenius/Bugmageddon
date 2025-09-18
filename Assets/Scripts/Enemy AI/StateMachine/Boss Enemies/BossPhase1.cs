using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;

public class BossPhase1 : IBossStates
{
    private BossStateMachine boss;
    private Enemy stats;

    public void Phase1()
    {

    }

    public void Phase2()
    {
        boss.StartPhase2();
    }

    public void Phase3()
    {

    }

    public void UpdateState()
    {
        boss.AttackPlayer();


        if (Time.time >= boss.chargeCooldown)
        {
            boss.StartCoroutine(boss.ChargeAbility());
            boss.chargeCooldown = Time.time + boss.chargeRechargeTime; 
        } 

        if (Time.time >= boss.acidBarrageRechargeTime)
        {
            boss.acidBarrageRechargeTime = Time.time + boss.acidBarrageCooldown;
            boss.StartCoroutine(boss.BarrageAbility());
        }

        if (stats.CurrentHealth <= (stats.maxHealth * 0.66))
        {
            Phase2();
        }
    }

    public void Initialize(BossStateMachine boss, Enemy stats)
    {
        this.boss = boss;
        this.stats = stats;
    }

}
