using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BossStateMachine : MonoBehaviour
{
    [Header("General Data")]
    public GameObject chaseTarget;
    public int currentPhaseINT = 1;

    [Header("Script References")]
    public EnemyCore BossCS;
    public EnemyCoreStatsSO BossCSSO;
    public EnemyMeleeStatsSO BossMSSO;
    public AttackTriggerActivator AttackTriggerActivator;

    [Header("Component References")]
    public Animator animator;

    [Header("Arm Attack")]
    public GameObject armAttack;

    [Header("Summon Guard Ability")]
    public float callGuardsCooldown = 30;
    public float callGuardsRechargeTime = 0;

    [Header("Tail Attack Ability")]
    public GameObject tailAttackContainer;
    public GameObject tailAttackTails;
    public float tailAttackDamage;
    public float tailAttackDelay = 2;
    public float tailAttackCooldown = 30;
    public float tailAttackRechargeTime = 0;

    // ======== HIDDEN DATA ======== //

    [HideInInspector] public IBossStates currentState;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Transform currentPosition;
    [HideInInspector] public Transform chaseTargetPosition;
    [HideInInspector] public Vector3 velocity;

    // ======== BOSS PHASE STATES ======== //

    [HideInInspector] public BossChaseState chaseState;
    [HideInInspector] public BossDeathState deathState;
    public enum BossPhase
    {
        Phase1,
        Phase2,
        Phase3
    }

    [HideInInspector] public bool abilityLocked = false;

    // ======== BOSS PHASE STATE SPECIFIC BOOLS ======== //

    public BossPhase currentPhase = BossPhase.Phase1;
    private bool phase2Triggered = false;
    private bool phase3Triggered = false;

    public bool movementLocked = false;
    [HideInInspector] public bool isDefeated = false;

    // ======== BOSS ABILITY STATES ======== //

    [HideInInspector] public BossSummonGuardsAbility bossSummonGuardsAbility;
    [HideInInspector] public BossArmAttack bossArmAttack;
    [HideInInspector] public BossTailAttack bossTailAttack;


    // Unity Methods
    private void Awake()
    {

    }

    private void Start()
    {
        BossCS = GetComponent<EnemyCore>();
        BossCSSO = BossCS.coreStats;
        BossMSSO = BossCS.meleeStats;
        animator = GetComponentInChildren<Animator>();
        chaseTarget = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = BossCS.coreStats.chaseSpeed;
        chaseTargetPosition = chaseTarget.transform;
        currentPosition = BossCS.transform;

        chaseState = new BossChaseState(this);
        deathState = new BossDeathState(this);

        bossSummonGuardsAbility = new BossSummonGuardsAbility(this);
        bossArmAttack = new BossArmAttack(this);
        bossTailAttack = new BossTailAttack(this);

        currentState = chaseState;
        currentPhaseINT = 1;

        DamageTriggerHandler tailDamageTrigger = tailAttackTails.GetComponent<DamageTriggerHandler>();
        tailDamageTrigger.triggerDamage = tailAttackDamage;

        DamageTriggerHandler armAttackTrigger = armAttack.GetComponent<DamageTriggerHandler>();
        armAttackTrigger.triggerDamage = BossCS.meleeStats.damage;

    }

    private void Update()
    {
        if (isDefeated)
            return;

        if (!abilityLocked)
        {
            currentState.UpdateState();
            UpdatePhase(); 
        }


        AbilityCooldown();
        velocity = navMeshAgent.velocity;
    }

    public void ChangeState(IBossStates newState)
    {
        currentState.ExitState();
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            animator.SetBool(parameter.name, false);
        }
        currentState = newState;
        currentState.EnterState();
    }

    public void StopMoving()
    {
        movementLocked = true;
        animator.SetBool("IsMoving", false);
        navMeshAgent.isStopped = true;
        navMeshAgent.updateRotation = false;
        navMeshAgent.velocity = Vector3.zero; 
        navMeshAgent.ResetPath();           
        navMeshAgent.destination = currentPosition.position;
    }

    public void UnlockMovement()
    {
        movementLocked = false;
        navMeshAgent.isStopped = false;
        navMeshAgent.updateRotation = true;
        navMeshAgent.velocity = Vector3.zero;
    }

    private void UpdatePhase()
    {
        float hp = BossCS.CurrentHealth;
        float max = BossCSSO.maxHealth;

        if (!isDefeated && hp <= 0)
        {
            isDefeated = true;
            ChangeState(deathState);
            return;
        }

        if (!phase2Triggered && hp <= max * 0.66f)
        {
            Debug.Log("Phase 2 started");
            phase2Triggered = true;
            currentPhase = BossPhase.Phase2;
            ChangeState(bossSummonGuardsAbility);
        }

        if (!phase3Triggered && hp <= max * 0.33f)
        {
            Debug.Log("Phase 3 started");
            phase3Triggered = true;
            currentPhase = BossPhase.Phase3;
            ChangeState(bossSummonGuardsAbility);
        }
    }


    public void AbilityCooldown()
    {
        if (tailAttackRechargeTime > 0)
        {
            tailAttackRechargeTime -= Time.deltaTime;
        }

        if (callGuardsRechargeTime > 0)
        {
            callGuardsRechargeTime -= Time.deltaTime;
        }

    }

    public void CheckAttackRange()
    {
        float distance = Vector3.Distance(transform.position, chaseTargetPosition.position);

        if (distance <= BossCS.meleeStats.strikeRange)
        {
            ChangeState(bossArmAttack);
        }
        else
        {
            ChaseTarget();
        }
    }
    public void ChaseTarget()
    {
        navMeshAgent.destination = chaseTargetPosition.position;
    }
    public IEnumerator DeathThroes()
    {
        animator.SetBool("DeathThroes", true);
        navMeshAgent.enabled = false;
        yield return new WaitForSeconds(BossCSSO.deathDuration);
        animator.SetBool("IsDefeated", true);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DestructibleRock"))
        {
            Destroy(collision.gameObject);
        }
    }


}
