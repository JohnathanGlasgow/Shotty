using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float acceleration;
    public float decceleration;
    public float velPower;
    public Vector2 moveInput;
    private Rigidbody2D rb;
    public float gravityStrength = 9.8f; 
    
    public float movement;

    [SerializeField]
    private InputActionReference movementInput, jump;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");

        //Vector2 movement = new Vector2(horizontalInput, verticalInput);

        moveInput = movementInput.action.ReadValue<Vector2>();

        //rb.velocity = moveInput * speed;
    }

    void FixedUpdate()
    {
        ApplyGravity();

        float targetSpeed = moveInput.x * moveSpeed;

        float speedDif = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = speedDif * accelRate;

        rb.AddForce(movement * Vector2.right);

    }

    void ApplyGravity()
    {
        Vector2 gravityForce = Vector2.down * gravityStrength;

        rb.velocity += gravityForce * Time.fixedDeltaTime;

    
    }

    /*
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }*/
}
