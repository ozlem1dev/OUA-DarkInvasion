using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerMovementScript : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float runSpeedMultiplier = 2f;
    private float currentMoveSpeed;

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
    Animator _animator;

    public Camera mainCamera;
    public float rotationSpeed = 500f;

    public AudioClip walkingAudio;
    public AudioClip runningAudio;
    private AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        _animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
            Jump();
            canJump = false;
        }

        if (Mathf.Abs(rb.velocity.magnitude) > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                audioSource.clip = runningAudio;
            }
            else
            {
                audioSource.clip = walkingAudio;
            }

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
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
        Vector3 moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentMoveSpeed = moveSpeed * runSpeedMultiplier;
        }
        else
        {
            currentMoveSpeed = moveSpeed;
        }

        rb.AddForce(moveDirection.normalized * currentMoveSpeed * 10f, ForceMode.Force);

        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * currentMoveSpeed * 10f, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > currentMoveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * currentMoveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

        _animator.SetFloat("Speed", rb.velocity.magnitude);
        Debug.Log("Animator hýzý:" + _animator.GetFloat("Speed"));
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void Rotation()
    {
        float cameraRotation = mainCamera.transform.eulerAngles.y;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, cameraRotation, 0f), 1);
    }
}
