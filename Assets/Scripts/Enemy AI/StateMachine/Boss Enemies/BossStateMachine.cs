using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossStateMachine : MonoBehaviour
{
    // Public Variables
    public float chaseSpeed = 5;

    // Charge ability
    public float chargeForce = 3000;
    public float chargeInterval = 1;
    public float chargeCooldown = 10;
    public float chargeRechargeTime = 0;

    //AcidBarrage Ability
    public GameObject bugProjectile;
    public List<Transform> firingPoints;
    public GameObject alert;
    public int acidBarrageCount = 5;
    public float acidBarrageCooldown = 10;
    public float acidBarrageRechargeTime = 0;

    //Summon Guards Ability
    public GameObject guards;
    public float callGuardsCooldown = 30;
    public float callGuardsRechargeTime = 0;

    //Current Phase Bools
    public bool phase2abilities = false;
    public bool phase3abilities = false;

    // Private References
    private Collider detectionTrigger;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public IBossStates currentState;
    [HideInInspector] public BossPhase1 phase1;
    [HideInInspector] public BossPhase2 phase2;
    [HideInInspector] public BossPhase3 phase3;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    public Rigidbody rigidBody;
    private BossStateMachine stateMachine;

    // Unity Methods
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<BossStateMachine>();
        rigidBody = GetComponent<Rigidbody>();

        phase1 = new BossPhase1();
        phase2 = new BossPhase2();
        phase3 = new BossPhase3();

        phase1.Initialize(this, GetComponent<Enemy>());
        phase2.Initialize(this, GetComponent<Enemy>());
        phase3.Initialize(this, GetComponent<Enemy>());
    }

    private void Start()
    {
        detectionTrigger = GetComponentInChildren<Collider>();
    }

    private void Update()
    {
        currentState?.UpdateState();
    }

    // Trigger Handlers
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered, Phase 1");
            chaseTarget = other.transform;
            chargeRechargeTime = 5;
            acidBarrageRechargeTime = 5;
            currentState = phase1;
        }
    }

    // Phase Management
    public void StartPhase3()
    {
        Debug.Log("Phase 3");
        phase3abilities = true;
        transform.localScale *= 1.25f;
        acidBarrageCount = 10;
        chargeInterval = 0.5f;
        currentState = phase3;
    }

    public void StartPhase2()
    {
        Debug.Log("Phase 2");
        phase2abilities = true;
        chaseSpeed = 10;
        acidBarrageCooldown = 5;
        chargeInterval = 0.75f;
        currentState = phase2;
    }

    // Attacking Player
    public void AttackPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, chaseTarget.position);
        Vector3 directionToPlayer = (chaseTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

        navMeshAgent.speed = chaseSpeed;
        navMeshAgent.destination = chaseTarget.position;
    }

    // Charge Ability
    public IEnumerator ChargeAbility()
    {
        yield return StartCoroutine(ChargeFunction());

        if (phase2abilities) yield return StartCoroutine(ChargeFunction());
        if (phase3abilities) yield return StartCoroutine(ChargeFunction());
    }

    private IEnumerator ChargeFunction()
    {
        alert.SetActive(true);
        yield return new WaitForSeconds(chargeInterval);

        Vector3 dashForce = (stateMachine.chaseTarget.position - transform.position).normalized * chargeForce;
        rigidBody.AddForce(dashForce, ForceMode.Impulse);

        transform.LookAt(new Vector3(chaseTarget.position.x, transform.position.y, chaseTarget.position.z));

        alert.SetActive(false);
    }

    // Calling Guards
    public IEnumerator CallGuards()
    {
        foreach (Transform firingPoint in firingPoints)
        {
            Instantiate(guards, firingPoint.position, firingPoint.rotation);
            yield return new WaitForSeconds(0.2f);
        }
    }

    // Acid Barrage Ability
    public IEnumerator BarrageAbility()
    {
        Debug.Log("Shooting!");
        for (int i = 0; i < acidBarrageCount; i++)
        {
            foreach (Transform firingPoint in firingPoints)
            {
                if (phase3abilities)
                {
                    Instantiate(bugProjectile, firingPoint.position, firingPoint.rotation);
                    Instantiate(bugProjectile, -firingPoint.position, firingPoint.rotation);
                }
                else
                {
                    Instantiate(bugProjectile, firingPoint.position, firingPoint.rotation);
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    // Die Method
    public void Die()
    {
        Destroy(gameObject);
    }
}
