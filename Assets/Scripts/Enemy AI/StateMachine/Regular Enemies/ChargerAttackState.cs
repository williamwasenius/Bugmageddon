using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ChargerAttackState : IEnemyStates
{
    private readonly EnemyStateMachine EnemySM;
    private readonly EnemyCore EnemyCS;

    private bool isPreparing;

    private Transform chargeTargetPoint;

    public ChargerAttackState(EnemyStateMachine sm)
    {
        EnemySM = sm;
        EnemyCS = sm.EnemyCS;

        chargeTargetPoint = new GameObject("ChargePoint").transform;
        chargeTargetPoint.gameObject.hideFlags = HideFlags.HideInHierarchy;
    }

    // ------------------ ENTER / EXIT ------------------ //

    public void EnterState()
    {
        if (EnemySM.currentChargeCooldown <= 0 && !isPreparing)
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
        if (EnemySM.isCharging)
        {
            if (!EnemySM.navMeshAgent.hasPath || EnemySM.navMeshAgent.remainingDistance < 1f)
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

        EnemySM.navMeshAgent.isStopped = true;
        EnemySM.navMeshAgent.destination = EnemySM.currentPosition.position;
        EnemySM.animator.SetBool("IsPreparing", true);

        Vector3 dir = (EnemySM.chaseTarget.position - EnemySM.transform.position).normalized;
        chargeTargetPoint.position = EnemySM.chaseTarget.position + dir * 20f;

        EnemySM.transform.forward = dir;

        EnemySM.StartCoroutine(PrepareChargeCoroutine());
    }

    private System.Collections.IEnumerator PrepareChargeCoroutine()
    {
        float duration = EnemySM.animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(duration);

        StartCharge();
    }

    private void StartCharge()
    {
        isPreparing = false;
        EnemySM.isCharging = true;

        EnemySM.animator.SetBool("IsPreparing", false);
        EnemySM.animator.SetBool("IsCharging", true);

        EnemyCS.chargerCollider.enabled = true;

        EnemySM.navMeshAgent.isStopped = false;
        EnemySM.navMeshAgent.updateRotation = false;
        EnemySM.navMeshAgent.speed = EnemyCS.chargeSpeed;
        EnemySM.navMeshAgent.destination = chargeTargetPoint.position;
    }

    private void EndCharge()
    {
        EnemySM.isCharging = false;
        EnemySM.currentChargeCooldown = EnemyCS.chargeCooldown;

        StopAllActions();
        ToChaseState();
    }

    private void StopAllActions()
    {
        EnemySM.animator.SetBool("IsPreparing", false);
        EnemySM.animator.SetBool("IsCharging", false);

        EnemyCS.chargerCollider.enabled = false;

        EnemySM.navMeshAgent.updateRotation = true;
        EnemySM.navMeshAgent.isStopped = false;
    }

    // ------------------ TRANSITIONS ------------------ //

    private void ToChaseState()
    {
        EnemySM.ChangeState(EnemySM.chaseState);
    }

    // ------------------ COLLISION ------------------ //

    public void OnCollisionEnter(Collision collision)
    {
        if (EnemySM.isCharging && collision.gameObject.CompareTag("Player"))
            EndCharge();
    }

    public void OnTriggerEnter(Collider other) { }
    public void OnTriggerExit(Collider other) { }
    public void Ontriggerstay(Collider other) { }
    public void OnCollisionEnter(Collider other) { }

}
