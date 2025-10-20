using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public bool powered = false;

    [Header("Weapons & Targeting")]
    public WeaponHandler[] weapons;

    [Header("Turret stats")]
    public float rotationSpeed = 5f;

    [SerializeField] private Transform target;
    [SerializeField] private List<Transform> enemiesInRange = new List<Transform>();
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
            if (target == null) UpdateTarget();

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
        enemiesInRange.RemoveAll(e => e == null); 
        target = null;
        float closestDistance = Mathf.Infinity;
        foreach (Transform enemy in enemiesInRange)
        {
            float distance = Vector3.Distance(transform.position, enemy.position);
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
            Vector3 direction = target.position - transform.position;
            direction.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void FireWeapons()
    {
        if (target != null)
        {
            foreach (WeaponHandler weapon in weapons)
            {
                weapon?.shoot();
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
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    void Unpowered()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !enemiesInRange.Contains(other.transform))
        {
            Debug.Log("bug entered");
            enemiesInRange.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.transform);
            if (other == target)
            {
                target = null;
            }
        }
    }
}
