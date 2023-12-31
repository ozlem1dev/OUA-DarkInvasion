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

    public float SpeedZ; //�apraz y�r�meler i�in bunun Z + X toplam h�z�n� almas�n� sa�l�yoruz. �sim kafa kar��t�rmas�n.
    public float SpeedX;
    public float SpeedY;

    public Camera mainCamera;
    public float rotationSpeed = 500f;

    public AudioClip walkingAudio;
    public AudioClip runningAudio;
    public AudioSource audioSource;
    private int groundCounter = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("RightLeft", false);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _animator.SetBool("RightLeft", true);
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                _animator.SetBool("RightLeft", false);
            }
        }


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
        Rotation();
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
            groundCounter++;
            isGrounded = true;
            canJump = true;
            _animator.SetBool("Grounded", true);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            groundCounter--;
            if (groundCounter == 0)
            {
                isGrounded = false;
            }
        }
    }
    //private void OnCollisionStay(Collision collision)
    //{


    //}
    //private void LateUpdate()
    //{
    //    Rotation();
    //}

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

        if (GetComponent<CharacterFire>().isReloading)
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

        //Debug.Log(orientation.forward.x);
        // Buna d�nebiliriz
        if (Math.Abs(orientation.forward.z) > Math.Abs(orientation.forward.x))
        {
            if (Math.Abs(orientation.forward.z) >= Math.Abs(orientation.right.x))
            {
                if (rb.velocity.z < 0)
                {
                    SpeedZ = orientation.forward.z * -1 * (Math.Abs(rb.velocity.x) + Math.Abs(rb.velocity.z));
                }
                else
                {
                    SpeedZ = orientation.forward.z * (Math.Abs(rb.velocity.x) + Math.Abs(rb.velocity.z));
                }

                SpeedX = orientation.right.x * rb.velocity.x;
            }
            else
            {
                if (rb.velocity.x < 0)
                {
                    SpeedX = orientation.right.x * -1 * (Math.Abs(rb.velocity.x) + Math.Abs(rb.velocity.z));
                }
                else
                {
                    SpeedX = orientation.right.x * (Math.Abs(rb.velocity.x) + Math.Abs(rb.velocity.z));
                }

                SpeedZ = orientation.forward.z * rb.velocity.z;
            }
        }
        else
        {
            if (Math.Abs(orientation.forward.x) >= Math.Abs(orientation.right.z))
            {
                if (rb.velocity.x < 0)
                {
                    SpeedZ = orientation.forward.x * -1 * (Math.Abs(rb.velocity.x) + Math.Abs(rb.velocity.z));
                }
                else
                {
                    SpeedZ = orientation.forward.x * (Math.Abs(rb.velocity.x) + Math.Abs(rb.velocity.z));
                }

                SpeedX = orientation.right.z * rb.velocity.z;
            }
            else
            {
                if (rb.velocity.z < 0)
                {
                    SpeedX = orientation.right.z * -1 * (Math.Abs(rb.velocity.x) + Math.Abs(rb.velocity.z));
                }
                else
                {
                    SpeedX = orientation.right.z * (Math.Abs(rb.velocity.x) + Math.Abs(rb.velocity.z));
                }

                SpeedZ = orientation.forward.x * rb.velocity.x;
            }

        }

        SpeedY = orientation.forward.y * rb.velocity.y;

        //SpeedZ = orientation.forward.z * (Math.Abs(rb.velocity.z) + Math.Abs(rb.velocity.x));

        _animator.SetFloat("SpeedZ", SpeedZ);
        _animator.SetFloat("SpeedX", SpeedX);

        if (SpeedX <= 0.01 && SpeedY <= 0.01 && SpeedZ <= 0.01)
        {
            _animator.SetBool("Moving", false);
        }
        else { _animator.SetBool("Moving", true); }

        //Debug.Log("SpeedZ:" + _animator.GetFloat("SpeedZ"));
        //Debug.Log("Z velocity:" + rb.velocity.z);
        //Debug.Log("SpeedX:" + _animator.GetFloat("SpeedX"));
        //Debug.Log("X velocity:" + rb.velocity.x);

    }

    private void Jump()
    {
        _animator.SetBool("Grounded", false);
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void Rotation()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Kameran�n hareket y�n�n� ve h�z�n� hesaplama
        Vector3 cameraMovement = new Vector3(horizontalInput, 0f, verticalInput) * 0f * Time.deltaTime;

        // Kameran�n yeni pozisyonunu g�ncelleme
        mainCamera.transform.position += cameraMovement;


        float cameraRotation = mainCamera.transform.eulerAngles.y;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, cameraRotation, 0f), 5000f);


    }
}
