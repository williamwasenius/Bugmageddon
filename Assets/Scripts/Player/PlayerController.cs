using System.Collections;
using UnityEngine;

interface IInteractable
{
    void Activate();
}

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 10f;
    public float runSpeed = 30f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    public float legRotationSpeed = 10f;
    private Vector3 currentVelocity;

    [Header("Ground Settings")]
    public LayerMask groundMask;  
    public float groundRayDistance = 3f;
    public Transform groundRayOriginPoint;

    [Header("Body Parts")]
    public Transform legs;
    public Transform torso;
    public Transform aimPivot;

    [Header("Weapons")]
    public GameObject weapon1;
    public GameObject weapon2;
    public bool armLock = false;
    public float wpnMaxRotation = 20f;
    public float wpnRotationSpeed = 10f;

    [Header("Ability")]
    public float abilityCooldown = 30f;
    private float abilityCooldownCounter;
    public float abilityDuration = 12f;
    public bool abilityActive = false;

    private bool isRunning = false;
    private bool isAccelerated = false;

    private Rigidbody rigidBody;
    [SerializeField] private Animator animator;
    private Camera playerCamera;

    private WeaponHandler weapon1Handler;
    private WeaponHandler weapon2Handler;
    private IInteractable currentInteractable;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
    }

    void Update()
    {
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
            currentInteractable.Activate();

        if (animator.GetBool("IsStomping"))
            return;

        if (Input.GetKeyDown(KeyCode.F) && !isAccelerated)
            StartCoroutine(StompRoutine());

        HandleShooting();
        HandleRun();
        Ability();
    }

    void FixedUpdate()
    {
        if (animator.GetBool("IsStomping"))
        {
            rigidBody.linearVelocity = Vector3.zero;
            return;
        }

        HandleMovement();

        LockToGround();
    }

    private void LockToGround()
    {
        Vector3 origin = groundRayOriginPoint.position + Vector3.up * 0.5f;
        Debug.DrawRay(origin, transform.up * -groundRayDistance, Color.red);
        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, groundRayDistance, groundMask))
        {
            Vector3 pos = rigidBody.position;
            pos.y = hit.point.y;
            rigidBody.position = pos;
        }

        Vector3 v = rigidBody.linearVelocity;
        v.y = 0;
        rigidBody.linearVelocity = v;
    }

    private IEnumerator StompRoutine()
    {
        rigidBody.linearVelocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        animator.SetBool("IsStomping", true);

        float stompTime = 1f;
        yield return new WaitForSeconds(stompTime);

        animator.SetBool("IsStomping", false);
    }

    public void AssignWeapon(WeaponHandler newWeapon) 
    { 
        if (weapon1Handler == null) 
            weapon1Handler = newWeapon; 
        else if (weapon2Handler == null) 
            weapon2Handler = newWeapon; }

    private void HandleShooting()
    {
        if (weapon1Handler != null)
        {
            if (weapon1Handler.chargedWeapon)
            {
                if (Input.GetKey(KeyCode.Mouse0)) weapon1Handler.StartCharging();
                if (Input.GetKeyUp(KeyCode.Mouse0)) weapon1Handler.ReleaseShot();
            }
            else if (Input.GetKey(KeyCode.Mouse0))
                weapon1Handler.AutoFire();
        }

        if (weapon2Handler != null)
        {
            if (weapon2Handler.chargedWeapon)
            {
                if (Input.GetKey(KeyCode.Mouse1)) weapon2Handler.StartCharging();
                if (Input.GetKeyUp(KeyCode.Mouse1)) weapon2Handler.ReleaseShot();
            }
            else if (Input.GetKey(KeyCode.Mouse1))
                weapon2Handler.AutoFire();
        }
    }

    private void HandleRun()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift);
        animator.SetBool("IsRunning", isRunning);
    }

    public void Ability()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && Time.time >= abilityCooldownCounter && !abilityActive)
        {
            abilityActive = true;
            abilityCooldownCounter = Time.time + abilityCooldown;
            StartCoroutine(DeactivateAbility());
        }
    }

    private IEnumerator DeactivateAbility()
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

        isAccelerated = currentSpeed > speed;

        HandleLegRotation(targetDirection);
        RotateTowardsMouse();
    }

    private void HandleLegRotation(Vector3 moveDir)
    {
        Vector3 velocityDir = new Vector3(rigidBody.linearVelocity.x, 0, rigidBody.linearVelocity.z);
        if (velocityDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocityDir, Vector3.up);
            legs.rotation = Quaternion.Slerp(legs.rotation, targetRotation, legRotationSpeed * Time.deltaTime);
        }
    }

    private void RotateTowardsMouse()
    {
        Ray cameraRay = playerCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));

        if (groundPlane.Raycast(cameraRay, out float rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Vector3 aimTarget = !armLock ? pointToLook : new Vector3(pointToLook.x, aimPivot.position.y, pointToLook.z);

            float maxAngle = wpnMaxRotation;
            Vector3 forward = aimPivot.transform.forward;

            Vector3 aimDir1 = (aimTarget - weapon1.transform.position).normalized;
            float angle1 = Vector3.Angle(forward, aimDir1);
            if (angle1 > maxAngle) aimDir1 = Vector3.RotateTowards(forward, aimDir1, Mathf.Deg2Rad * maxAngle, 0f);

            Vector3 aimDir2 = (aimTarget - weapon2.transform.position).normalized;
            float angle2 = Vector3.Angle(forward, aimDir2);
            if (angle2 > maxAngle) aimDir2 = Vector3.RotateTowards(forward, aimDir2, Mathf.Deg2Rad * maxAngle, 0f);

            weapon1.transform.rotation = Quaternion.Slerp(weapon1.transform.rotation, Quaternion.LookRotation(aimDir1), wpnRotationSpeed * Time.deltaTime);
            weapon2.transform.rotation = Quaternion.Slerp(weapon2.transform.rotation, Quaternion.LookRotation(aimDir2), wpnRotationSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        currentInteractable = other.GetComponent<IInteractable>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteractable>() == currentInteractable)
            currentInteractable = null;
    }
}
