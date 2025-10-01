using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement Settings")]
    public float speed;
    public float acceleration;
    public float deceleration;
    private Vector3 currentVelocity;

    [Header("Body Parts")]
    public Transform legs;
    public Transform torso;

    [Header("Weapons")]
    public GameObject weapon1;
    public GameObject weapon2;

    [Header("Dash")]
    public float dashCooldown = 2;
    private float dashCooldownCounter;
    public float dashForceMultiplier = 50;

    [Header("Ability")]
    public float abilityCooldown = 30;
    private float abilityCooldownCounter;
    public float abilityDuration = 12;
    private float abilityDurationCounter;
    public bool abilityActive = false;

    private bool isShooting = false;
    private Vector3 movement;

    private Rigidbody rigidBody;
   [SerializeField] private Animator animator;
    private Camera playerCamera;

    private WeaponHandler weapon1Handler;
    private WeaponHandler weapon2Handler;

    private bool armLock = true;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerCamera = Camera.main;

        InitializeWeapons();
    }

    void Update()
    {
        HandleShooting();

        HandleDash();

        Ability();

        //  AdjustSpeedForShooting();

    }

    void FixedUpdate()
    {
        InitializeWeapons();

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
            weapon1Handler.shoot();
        }

        if (Input.GetKey(KeyCode.Mouse1) && weapon2Handler != null)
        {
            weapon2Handler.shoot();
        }

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            Debug.Log("Weapon stance switched");
            armLock = !armLock; 
        }

        isShooting = Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1);
    }

    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= dashCooldownCounter)
        {
            Dash(movement);
            dashCooldownCounter = Time.time + dashCooldown;
        }
    }

    private void Ability()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && Time.time >= abilityCooldownCounter && !abilityActive)
        {
            abilityDurationCounter = Time.time + abilityDuration;
            abilityActive = true;
            StartCoroutine(DeactivateAbilityAfterDuration());
            abilityCooldownCounter = Time.time + abilityCooldown;
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

        if (targetDirection.magnitude > 0.1f)
        {
            currentVelocity = Vector3.MoveTowards(
                currentVelocity,
                targetDirection * speed,
                acceleration * Time.fixedDeltaTime
            );

            //animator.SetBool("IsWalking", true);
        }
        else
        {
            currentVelocity = Vector3.MoveTowards(
                currentVelocity,
                Vector3.zero,
                deceleration * Time.fixedDeltaTime
            );

            //animator.SetBool("IsWalking", false);
        }

        rigidBody.linearVelocity = new Vector3(currentVelocity.x, rigidBody.linearVelocity.y, currentVelocity.z);

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

            torso.LookAt(new Vector3(pointToLook.x, torso.position.y, pointToLook.z));

            Vector3 weaponLookAtTarget = armLock
                ? new Vector3(pointToLook.x, weapon1.transform.position.y, pointToLook.z)
                : pointToLook;

            weapon1.transform.LookAt(weaponLookAtTarget);
            weapon2.transform.LookAt(weaponLookAtTarget);
        }
    }



    private void Dash(Vector3 movementDirection)
    {
        Vector3 dashForce = movementDirection * dashForceMultiplier;
        rigidBody.AddForce(dashForce, ForceMode.Impulse);
    }

    public float DashCooldownProgress()
    {
        return Mathf.Clamp01((dashCooldownCounter - Time.time) / dashCooldown);
    }


    private void AdjustSpeedForShooting()
    {
        speed = isShooting ? 1f : 2f;
    }
}