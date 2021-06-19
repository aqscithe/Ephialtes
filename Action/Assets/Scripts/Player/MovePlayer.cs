using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float jumpForce = 20f;
    public float fallForce = 130f;

    private Animator animator = null;

    private float verticalVelocity = 0f;
    private bool isJumping = false;

    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 10f;
    [Range(0f,1f)]
    [SerializeField] float speedReduction = 0.1f;

    [HideInInspector] public bool trapped = false;
    [HideInInspector] public bool dead = false;

    private CharacterController controller = null;
    private HealthManager healthManager = null;
    private Rigidbody rb = null;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        healthManager = GetComponent<HealthManager>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            isJumping = true;
    }

    private void FixedUpdate()
    {
        if (dead)
            return;

        if (trapped)
        {
            animator.SetBool("IsTrapped", trapped);
        }
        else
        {
            SetVerticalVelocity();
            float actualSpeed = GetActualSpeed();

            Vector3 move = GetMoveVec3(actualSpeed);
            controller.Move(move * Time.fixedDeltaTime);

            Vector2 speed = new Vector2(move.x, move.z);
            SetAnimatorValues(ref move, ref speed);
        }
        


    }

    private float GetActualSpeed()
    {
        float actualSpeed = walkSpeed;

        if (Input.GetButton("Run"))
            actualSpeed = runSpeed;

        if (healthManager.health < healthManager.maxHealth)
            actualSpeed -= actualSpeed * speedReduction;

        return actualSpeed;
    }

    private void SetVerticalVelocity()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -fallForce * Time.fixedDeltaTime;

            if (isJumping)
                verticalVelocity = jumpForce;
        }
        else
        {
            verticalVelocity -= fallForce * Time.fixedDeltaTime;
        }

        if (verticalVelocity <= 0f)
            isJumping = false;
    }

    private void SetAnimatorValues(ref Vector3 move, ref Vector2 speed)
    {
        animator.SetFloat("Speed", speed.magnitude);
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(move.x));
        animator.SetFloat("VerticalSpeed", Mathf.Abs(move.z));
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsTrapped", trapped);
    }

    private Vector3 GetMoveVec3(float actualSpeed)
    {
        return new Vector3(Input.GetAxis("Horizontal") * actualSpeed, verticalVelocity, Input.GetAxis("Vertical") * actualSpeed);
    }

    public void Climb(float climbSpeed)
    {
        verticalVelocity = climbSpeed;
    }
}
