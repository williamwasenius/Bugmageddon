using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public bool powered = false;

    [Header("Turret Components")]
    public WeaponHandler[] weapons;
    public GameObject turretCore;

    [Header("Turret stats")]
    public float rotationSpeed = 5f;

    [SerializeField] private GameObject target;
    [SerializeField] private List<GameObject> enemiesInRange = new List<GameObject>();
    public Collider detectionTrigger;

    [Header("Idling")]
    private float idleDirection = 1f;
    private float idleCurrentAngle = 0f;
    private Quaternion idleStartRotation;
    public float idleSweepAngle = 45f;
    public float idleSpeed = 30f;

    void Start()
    {
        idleStartRotation = transform.rotation;
    }

    void Update()
    {
        if (powered)
        {
            if (target == null || !target.activeInHierarchy) UpdateTarget();

            if (target != null)
            {
                TrackTarget();
                FireWeapons();
            }
            else
            {
                Idle();
            }
        }
        else
        {
            Unpowered();
        }
    }

    void UpdateTarget()
    {
        enemiesInRange.RemoveAll(e => e == null || !e.activeInHierarchy); 
        target = null;

        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemiesInRange)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = enemy;
            }
        }
    }

    void TrackTarget()
    {
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            turretCore.transform.rotation = Quaternion.Slerp(turretCore.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void FireWeapons()
    {
        if (target != null)
        {
            foreach (WeaponHandler weapon in weapons)
            {
                weapon?.TryFire();
            }
        }
    }

    void Idle()
    {
        float deltaAngle = idleSpeed * Time.deltaTime * idleDirection;
        idleCurrentAngle += deltaAngle;

        if (Mathf.Abs(idleCurrentAngle) >= idleSweepAngle)
        {
            idleDirection *= -1f;
            idleCurrentAngle = Mathf.Clamp(idleCurrentAngle, -idleSweepAngle, idleSweepAngle);
        }

        Quaternion targetRotation = idleStartRotation * Quaternion.Euler(0, idleCurrentAngle, 0);
        turretCore.transform.rotation = Quaternion.Slerp(turretCore.transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    void Unpowered()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !enemiesInRange.Contains(other.gameObject))
        {
            Debug.Log("bug entered");
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
            if (other == target)
            {
                target = null;
            }
        }
    }
}
