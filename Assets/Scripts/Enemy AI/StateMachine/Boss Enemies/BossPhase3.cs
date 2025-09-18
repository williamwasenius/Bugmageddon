using UnityEngine;
using UnityEngine.AI;
using Unity.VisualScripting;

public class BossPhase3 : IBossStates
{
    private BossStateMachine boss;
    private Enemy stats;

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
        boss.AttackPlayer();

        if (Time.time >= boss.chargeCooldown)
        {
            boss.StartCoroutine(boss.ChargeAbility());
            boss.chargeCooldown = Time.time + boss.chargeRechargeTime;
        }

        if (Time.time >= boss.acidBarrageRechargeTime)
        {
            boss.StartCoroutine(boss.BarrageAbility());
            boss.acidBarrageRechargeTime = Time.time + boss.acidBarrageCooldown;
        }

        if (stats.CurrentHealth <= 0)
        {
            boss.Die();
        }
    }
    public void Initialize(BossStateMachine boss, Enemy stats)
    {
        this.boss = boss;
        this.stats = stats;
    }
}
