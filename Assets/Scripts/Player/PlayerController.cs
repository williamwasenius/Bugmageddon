using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 10f;               
    public float runSpeed = 30f;           
    public float acceleration = 10f;
    public float deceleration = 10f;
    private Vector3 currentVelocity;

    [Header("Body Parts")]
    public Transform legs;
    public Transform torso;
    public Transform aimPivot;

    [Header("Weapons")]
    public GameObject weapon1;
    public GameObject weapon2;
    public float wpnMaxRotation = 20f;

    [Header("Ability")]
    public float abilityCooldown = 30f;
    private float abilityCooldownCounter;
    public float abilityDuration = 12f;
    public bool abilityActive = false;

    private bool isShooting = false;
    private bool isRunning = false;

    private Rigidbody rigidBody;
    [SerializeField] private Animator animator;
    private Camera playerCamera;

    private WeaponHandler weapon1Handler;
    private WeaponHandler weapon2Handler;

    private bool armLock = true;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
        InitializeWeapons();
    }

    void Update()
    {
        HandleShooting();
        HandleRun();
        Ability();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void InitializeWeapons()
    {
        if (weapon1 != null) weapon1Handler = weapon1.GetComponentInChildren<WeaponHandler>();
        if (weapon2 != null) weapon2Handler = weapon2.GetComponentInChildren<WeaponHandler>();
    }

    private void HandleShooting()
    {
        if (Input.GetKey(KeyCode.Mouse0) && weapon1Handler != null)
        {
            Debug.Log("shooting weapon 1");
            weapon1Handler.shoot();
        }

        if (Input.GetKey(KeyCode.Mouse1) && weapon2Handler != null)
        {
            Debug.Log("shooting weapon 2");
            weapon2Handler.shoot();
         }

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            Debug.Log("Weapon stance switched");
            armLock = !armLock;
        }

        isShooting = Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1);
    }

    private void HandleRun()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift);
    }

    public void Ability()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && Time.time >= abilityCooldownCounter && !abilityActive)
        {
            abilityActive = true;
            abilityCooldownCounter = Time.time + abilityCooldown;
            StartCoroutine(DeactivateAbilityAfterDuration());
        }
    }

    private IEnumerator DeactivateAbilityAfterDuration()
    {
        yield return new WaitForSeconds(abilityDuration);
        abilityActive = false;
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 targetDirection = new Vector3(horizontal, 0, vertical).normalized;
        float targetSpeed = isRunning ? runSpeed : speed;

        if (targetDirection.magnitude > 0.1f)
        {
            currentVelocity = Vector3.MoveTowards(
                currentVelocity,
                targetDirection * targetSpeed,
                acceleration * Time.fixedDeltaTime
            );

            animator.SetBool("IsWalking", true);
        }
        else
        {
            currentVelocity = Vector3.MoveTowards(
                currentVelocity,
                Vector3.zero,
                deceleration * Time.fixedDeltaTime
            );

            animator.SetBool("IsWalking", false);
        }

        rigidBody.linearVelocity = new Vector3(currentVelocity.x, rigidBody.linearVelocity.y, currentVelocity.z);

        float currentSpeed = new Vector3(rigidBody.linearVelocity.x, 0, rigidBody.linearVelocity.z).magnitude;
        animator.SetFloat("MoveSpeed", currentSpeed);

        HandleLegRotation(targetDirection);
        RotateTowardsMouse();
    }

    private void HandleLegRotation(Vector3 moveDir)
    {
        if (moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            legs.rotation = Quaternion.Slerp(legs.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }

    private void RotateTowardsMouse()
    {
        Ray cameraRay = playerCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));

        if (groundPlane.Raycast(cameraRay, out float rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Vector3 aimTarget = armLock
                ? new Vector3(pointToLook.x, aimPivot.position.y, pointToLook.z)
                : pointToLook;

            float maxAngle = wpnMaxRotation;
            Vector3 forward = aimPivot.transform.forward;

            Vector3 aimDir1 = (aimTarget - weapon1.transform.position).normalized;
            float angle1 = Vector3.Angle(forward, aimDir1);
            if (angle1 > maxAngle)
                aimDir1 = Vector3.RotateTowards(forward, aimDir1, Mathf.Deg2Rad * maxAngle, 0f);

            Vector3 aimDir2 = (aimTarget - weapon2.transform.position).normalized;
            float angle2 = Vector3.Angle(forward, aimDir2);
            if (angle2 > maxAngle)
                aimDir2 = Vector3.RotateTowards(forward, aimDir2, Mathf.Deg2Rad * maxAngle, 0f);

            Quaternion rot1 = Quaternion.LookRotation(aimDir1);
            Quaternion rot2 = Quaternion.LookRotation(aimDir2);

            float rotationSpeed = 10f;
            weapon1.transform.rotation = Quaternion.Slerp(weapon1.transform.rotation, rot1, rotationSpeed * Time.deltaTime);
            weapon2.transform.rotation = Quaternion.Slerp(weapon2.transform.rotation, rot2, rotationSpeed * Time.deltaTime);
        }
    }


}
