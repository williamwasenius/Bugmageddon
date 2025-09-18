using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 

    [Header("Movement Settings")]
    public float speed = 2;
    public float dashSpeedMultiplier = 50;

    [Header("Weapons")]
    public GameObject weapon1;
    public GameObject weapon2;

    [Header("Dash")]
    public float dashCooldown = 2;
    private float dashCooldownCounter;

    [Header("Ability")]
    public float abilityCooldown = 30;
    private float abilityCooldownCounter;
    public float abilityDuration = 12;
    private float abilityDurationCounter;
    public bool abilityActive = false;

    private bool isShooting = false;
    private Vector3 movement;

    private Rigidbody rigidBody;
    // [SerializeField] private Animator animator;
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

       /* if (horizontal != 0 || vertical != 0)
        {
            Debug.Log("is moving");
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        } */

        movement = new Vector3(horizontal, 0, vertical);

        rigidBody.AddForce(movement * speed, ForceMode.VelocityChange);

        RotateTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        Ray cameraRay = playerCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(cameraRay, out float rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));

            Vector3 weaponLookAtTarget = armLock ? new Vector3(pointToLook.x, weapon1.transform.position.y, pointToLook.z) : pointToLook;

            weapon1.transform.LookAt(weaponLookAtTarget);
            weapon2.transform.LookAt(weaponLookAtTarget);
        }
    }


    private void Dash(Vector3 movementDirection)
    {
        Vector3 dashForce = movementDirection * dashSpeedMultiplier;
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