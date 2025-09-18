using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;

public class BossScript : MonoBehaviour
{
    public float chargeFrequence = 5;
    public float chargeForce = 5000; 
    private float chargeCooldown = 0;

    public bool isEnraged = false;

    public GameObject bugProjectile;
    public List<Transform> firingPoints;
    public GameObject alert;
    public int acidBarrageCount = 5; 
    public float acidBarrageCooldown = 3f; 
    private float acidBarrageCooldownTime = 0;

    private Rigidbody rigidBody;

    private EnemyStateMachine stateMachine;

    void Start()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (stateMachine.chaseTarget == null)
        {
            return;
        }

        if (isEnraged == false)
        {
            if (Time.time >= chargeCooldown)
            {
                StartCoroutine(Charge());
                chargeCooldown = Time.time + chargeFrequence;
            }

            if (Time.time >= acidBarrageCooldownTime)
            {
                StartCoroutine(LaunchAcidBarrage());
                acidBarrageCooldownTime = Time.time + acidBarrageCooldown;

            }
        }
        else
        {
            if (Time.time >= chargeCooldown)
            {
                StartCoroutine(Charge());
                chargeCooldown = Time.time + chargeFrequence;
            }

            if (Time.time >= acidBarrageCooldownTime)
            {
                StartCoroutine(LaunchAcidBarrage());
                acidBarrageCooldownTime = Time.time + acidBarrageCooldown;

            }
        }
    }

    private void Dash(Vector3 movementDirection)
    {
        Vector3 dashForce = movementDirection * chargeForce;
        rigidBody.AddForce(dashForce, ForceMode.Impulse);

        transform.LookAt(new Vector3(stateMachine.chaseTarget.position.x, transform.position.y, stateMachine.chaseTarget.position.z));
    }
    private IEnumerator Charge()
    {

        alert.SetActive(true);

        yield return new WaitForSeconds(1f);

        rigidBody.linearDamping = 0; 
        Vector3 dashForce = ((stateMachine.chaseTarget.position - transform.position).normalized) * chargeForce;
        rigidBody.AddForce(dashForce, ForceMode.Impulse);
        transform.LookAt(new Vector3(stateMachine.chaseTarget.position.x, transform.position.y, stateMachine.chaseTarget.position.z));

        yield return new WaitForSeconds(1f);

        alert.SetActive(false);
        rigidBody.linearDamping = 5;
    }

    private IEnumerator LaunchAcidBarrage()
    {
        for (int i = 0; i < acidBarrageCount; i++)
        {
            foreach (Transform firingPoint in firingPoints)
            {
                GameObject.Instantiate(bugProjectile, firingPoint.transform.position, firingPoint.transform.rotation);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
