using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMover : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float Drag;
    private float sprintmultiplier = 1f;
    public float SprintMultiplier = 1.25f;
    public KeyCode SprintKey = KeyCode.LeftShift;
    public bool IsSprinting;
    public float TotalMovingSpeed;

    [Header("Jumping")]
    public float jupmForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool readyToJump;
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;

    public Transform orientation;

    private float horInput;
    private float verInput;
    
    

    private Vector3 moveDirection;
    public GunScript gun;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        
        PlayerInput();
        SpeedControl();

        if (grounded) { rb.drag = Drag; }
        else { rb.drag = 0; }

    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        horInput = Input.GetAxisRaw("Horizontal");
        verInput = Input.GetAxisRaw("Vertical");

        IsSprinting = Input.GetKey(SprintKey) && verInput > 0;


        if (IsSprinting && !gun.IsAiming)
        {
            sprintmultiplier = SprintMultiplier;
            gun.IsSprinting = true;
        }
        else
        {
            sprintmultiplier = 1f;
            gun.IsSprinting = false;
        }


        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verInput + orientation.right * horInput;

        var totalSpeed = moveSpeed * sprintmultiplier;

        rb.AddForce(moveDirection * totalSpeed * 10f,ForceMode.Force);
        if (grounded) { rb.AddForce(moveDirection * totalSpeed * 10f, ForceMode.Force); }
        else if (!grounded) { rb.AddForce(moveDirection * totalSpeed * 10f * airMultiplier, ForceMode.Force); }

        TotalMovingSpeed = rb.velocity.magnitude / moveSpeed;
    }

    private void SpeedControl()
    {
        var totalSpeed = moveSpeed * sprintmultiplier;

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * totalSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x,0f,rb.velocity.z);

        rb.AddForce(transform.up * jupmForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
