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

    [HideInInspector] public BossPhase1 phase1;
    [HideInInspector] public BossPhase2 phase2;
    [HideInInspector] public BossPhase3 phase3;

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

        phase1 = new BossPhase1(this);
        phase2 = new BossPhase2(this);
        phase3 = new BossPhase3(this);

        bossSummonGuardsAbility = new BossSummonGuardsAbility(this);
        bossArmAttack = new BossArmAttack(this);
        bossTailAttack = new BossTailAttack(this);

        currentState = phase1;
        currentPhaseINT = 1;

        DamageTriggerHandler tailDamageTrigger = tailAttackTails.GetComponent<DamageTriggerHandler>();
        tailDamageTrigger.triggerDamage = tailAttackDamage;

        DamageTriggerHandler armAttackTrigger = armAttack.GetComponent<DamageTriggerHandler>();
        armAttackTrigger.triggerDamage = BossCS.meleeStats.damage;

    }

    private void Update()
    {
        currentState.UpdateState();
        velocity = navMeshAgent.velocity;
        AbilityCooldown();

        if (BossCS.CurrentHealth <= (BossCSSO.maxHealth / 3) * 2)
        {
            Phase2();
        }
        else if (BossCS.CurrentHealth <= BossCSSO.maxHealth / 3)
        {
            Phase3();
        }

    }

    public void ChangeState(IBossStates newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public void StopMoving()
    {
        animator.SetBool("IsMoving", false);
        navMeshAgent.isStopped = true;
        navMeshAgent.destination = currentPosition.position;
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

    public void Phase2()
    {
        currentPhaseINT = 2;
    }

    public void Phase3()
    {
        currentPhaseINT = 3;
    }

}
