using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ChargerAttackState : IEnemyStates
{
    private readonly EnemyStateMachine enemySM;
    private readonly EnemyCore enemyCS;
    private readonly EnemyChargerStatsSO chargerStatsSO;

    private bool isPreparing;

    private Transform chargeTargetPoint;

    public ChargerAttackState(EnemyStateMachine stateMachine)
    {
        enemySM = stateMachine;
        enemyCS = stateMachine.enemyCS;
        chargerStatsSO = enemyCS.chargerStats;

        chargeTargetPoint = new GameObject("ChargePoint").transform;
        chargeTargetPoint.gameObject.hideFlags = HideFlags.HideInHierarchy;
    }

    // ------------------ ENTER / EXIT ------------------ //

    public void EnterState()
    {
        if (chargerStatsSO.chargeRechargeTimer <= 0 && !isPreparing)
            TryCharge();
        else
            ToChaseState();
    }

    public void ExitState()
    {
        StopAllActions();
    }

    // ------------------ UPDATE ------------------ //

    public void UpdateState()
    {
        if (chargerStatsSO.isCharging)
        {
            if (!enemySM.nMAgent.hasPath || enemySM.nMAgent.remainingDistance < 1f)
            {
                EndCharge();
            }
        }
        else if (!isPreparing) 
        {
            ToChaseState();
        }
    }

    // ------------------ CHARGE LOGIC ------------------ //

    private void TryCharge()
    {
        isPreparing = true;

        enemySM.nMAgent.isStopped = true;
        enemySM.nMAgent.destination = enemySM.transform.position;
        enemySM.animator.SetBool("IsPreparing", true);

        Vector3 dir = (enemySM.chaseTarget.position - enemySM.transform.position).normalized;
        chargeTargetPoint.position = enemySM.chaseTarget.position + dir * 20f;

        enemySM.transform.forward = dir;

        enemySM.StartCoroutine(PrepareChargeCoroutine());
    }

    private System.Collections.IEnumerator PrepareChargeCoroutine()
    {
        float duration = enemySM.animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(duration);

        StartCharge();
    }

    private void StartCharge()
    {
        isPreparing = false;
        chargerStatsSO.isCharging = true;

        enemySM.animator.SetBool("IsPreparing", false);
        enemySM.animator.SetBool("IsCharging", true);

        enemySM.nMAgent.isStopped = false;
        enemySM.nMAgent.updateRotation = false;
        enemySM.nMAgent.speed = chargerStatsSO.chargeSpeed;
        enemySM.nMAgent.destination = chargeTargetPoint.position;
    }

    private void EndCharge()
    {
        chargerStatsSO.isCharging = false;
        chargerStatsSO.chargeRechargeTimer = chargerStatsSO.chargeCooldown;

        StopAllActions();
        ToChaseState();
    }

    private void StopAllActions()
    {
        enemySM.animator.SetBool("IsPreparing", false);
        enemySM.animator.SetBool("IsCharging", false);

        enemySM.nMAgent.updateRotation = true;
        enemySM.nMAgent.isStopped = false;
    }

    // ------------------ TRANSITIONS ------------------ //

    private void ToChaseState()
    {
        enemySM.ChangeState(enemySM.chaseState);
    }

    // ------------------ COLLISION ------------------ //

    public void OnCollisionEnter(Collision collision)
    {
        if (chargerStatsSO.isCharging && collision.gameObject.CompareTag("Player"))
            EndCharge();
    }

    public void OnTriggerEnter(Collider other) { }
    public void OnTriggerExit(Collider other) { }
    public void Ontriggerstay(Collider other) { }
    public void OnCollisionEnter(Collider other) { }

}
