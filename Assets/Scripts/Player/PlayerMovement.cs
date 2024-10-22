using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <remarks>
/// responsibilities of this script include Jumping, Shotgun Knockback, ground friction, and gravity.
/// Parts of code is taken from this youtube video - https://www.youtube.com/watch?v=KbtcEVCM7bw
/// </remarks>

/// <summary>
/// This script manages player movement, and physics handling.
/// </summary>

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float acceleration;
    public float decceleration;
    public float velPower;
    public float frictionAmount = 0.2f;

    public float jumpForce;

    private bool isGrounded;

    public Vector2 moveInput;
    private Rigidbody2D rb;
    public float gravityStrength = 9.8f; 
    
    public float movement;

    public LayerMask groundLayer;

    public Transform groundCheckPoint;
    public Vector2 groundCheckSize;

    public float jumpInput;
    public bool canJump = true;

    public Transform crosshairTransform;

    private float shootInput;
    public Vector2 shootAngle;
    public bool canShoot = true;
    public float shootForce;

    [SerializeField]
    private InputActionReference movementInput, jump, shoot;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        shootAngle = crosshairTransform.rotation * Vector2.left;

        moveInput = movementInput.action.ReadValue<Vector2>();

        jumpInput = jump.action.ReadValue<float>();

        shootInput = shoot.action.ReadValue<float>();

        if(Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer)){
            isGrounded = true;
        }else
        {
            isGrounded = false;
        }

        if(jumpInput == 1 && isGrounded && canJump)
        {
            Jump();
        }

        if(jumpInput == 0 && isGrounded && canJump == false )
        {
            canJump = true;
        }

        if(shootInput == 0 && canShoot == false)
        {
            canShoot = true;
        }
    }

    void FixedUpdate()
    {
        ApplyGravity();

        if(moveInput.x > 0.01f && rb.velocity.x < moveSpeed)
        {
            Move();
        }

        if(moveInput.x < -0.01f && rb.velocity.x > -moveSpeed)
        {
            Move();
        }
        Friction();
    }

    void ApplyGravity()
    {
        Vector2 gravityForce = Vector2.down * gravityStrength;

        rb.velocity += gravityForce * Time.fixedDeltaTime;

    
    }

    void Move()
    {
        float targetSpeed = moveInput.x * moveSpeed;

        float speedDif = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = speedDif * accelRate;

        rb.AddForce(movement * Vector2.right);
    }

    void Friction()
    {
        if(isGrounded && Mathf.Abs(moveInput.x) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));

            amount *= Mathf.Sign(rb.velocity.x);

            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    void Jump()
    {
        canJump = false;
        isGrounded = false;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        AudioManager.PlaySound(0); //jump sound
    }

    public void Shoot()
    {
        canShoot = false;
        AudioManager.PlaySound(1); //shoot sound
        CameraShake.Shake(0.25f, 0.5f);
        // if moving downwards set vert velocity to 0
        if(rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        //rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(shootAngle * shootForce, ForceMode2D.Impulse);
    }

    public void ResetMovement()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }
}
