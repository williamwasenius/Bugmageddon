using UnityEngine;

public class BossPhase2 : IBossStates
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
        boss.StartPhase3();
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

        if (Time.time >= boss.callGuardsRechargeTime)
        {
            boss.StartCoroutine(boss.CallGuards());
            boss.callGuardsRechargeTime = Time.time + boss.callGuardsCooldown;
        }

        if (stats.CurrentHealth <= (stats.maxHealth * 0.33))
        {
            Phase3();
        }
    }

    public void Initialize(BossStateMachine boss, Enemy stats)
    {
        this.boss = boss;
        this.stats = stats;
    }
}
