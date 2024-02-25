using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using UnityEngine.UIElements;
>>>>>>> debd24afbfef5a5eb7885c250561c8ccc20b0eac

public class PlayerMover : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float Drag;

<<<<<<< HEAD
=======
    [Header("Jumping")]
    public float jupmForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool readyToJump;
    public KeyCode jumpKey = KeyCode.Space;

>>>>>>> debd24afbfef5a5eb7885c250561c8ccc20b0eac
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;

<<<<<<< HEAD
    //private float jupmForce;
    //private float jumpCooldown;
    //private float airMultiplier;


=======
>>>>>>> debd24afbfef5a5eb7885c250561c8ccc20b0eac
    public Transform orientation;

    private float horInput;
    private float verInput;

    private Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
<<<<<<< HEAD
=======
        readyToJump = true;
>>>>>>> debd24afbfef5a5eb7885c250561c8ccc20b0eac
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
<<<<<<< HEAD
=======

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
>>>>>>> debd24afbfef5a5eb7885c250561c8ccc20b0eac
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verInput + orientation.right * horInput;

<<<<<<< HEAD
        rb.AddForce(moveDirection * moveSpeed * 10f,ForceMode.Force);
=======
        if (grounded) { rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force); }
        else if (!grounded) { rb.AddForce(moveDirection * moveSpeed * 10f * airMultiplier, ForceMode.Force); }
>>>>>>> debd24afbfef5a5eb7885c250561c8ccc20b0eac
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
<<<<<<< HEAD
=======

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x,0f,rb.velocity.z);

        rb.AddForce(transform.up * jupmForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
>>>>>>> debd24afbfef5a5eb7885c250561c8ccc20b0eac
}
