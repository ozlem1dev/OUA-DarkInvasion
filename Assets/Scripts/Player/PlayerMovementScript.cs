using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementScript : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
 
    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Ground Check")]
    public float playerHeight;
  

    bool isGrounded = false;
    bool canJump = true;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public Camera mainCamera;
    public float rotationSpeed = 500f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        //readyToJump = true;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();

        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            // Zýplama kodunu buraya yazýn
            Jump();
            canJump = false;
        }

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            canJump = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }

    private void LateUpdate()
    {
        Rotation();
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        //on ground
        if (isGrounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
   
    private void Rotation()
    {
        // Kameranýn yatay (horizontal) eksendeki pozisyonunu al
        float cameraRotation = mainCamera.transform.eulerAngles.y;

        // Karakteri o yöne döndür
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, cameraRotation, 0f), 1);
    }
}