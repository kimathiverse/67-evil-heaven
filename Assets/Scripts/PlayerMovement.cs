using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;

    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    public float jumpBoost;
    public float moveSpeedBoost;
    public float jumpDecrease;
    public float speedDecrease;


    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public bool canMove;
    private float fov;
    private float startFov;
    public float fovChange;
    private bool boosted;
    private bool decreased;
    private Coroutine boostCoroutine;
    private Coroutine decreaseCoroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        canMove = true;
        fov = 60;
        startFov = fov;
        boosted = false;
        decreased = false;
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.fieldOfView = fov;
        if(!canMove) return;
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        if (grounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }
    }

    private void FixedUpdate()
    {
        if(!canMove) return;
        MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if(Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
        
            
            

    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
 public void SpeedBoost()
{
    if (!boosted)
    {
        jumpForce += jumpBoost;
        moveSpeed += moveSpeedBoost;
        fov += fovChange;
        boosted = true;

        // Stop existing coroutine if running
        if (boostCoroutine != null) StopCoroutine(boostCoroutine);
        boostCoroutine = StartCoroutine(BoostCountDown());
    }
    else
    {
        // Restart coroutine without stacking values
        if (boostCoroutine != null) StopCoroutine(boostCoroutine);
        boostCoroutine = StartCoroutine(BoostCountDown());
    }
}

public void SpeedDecrease()
{
    if (!decreased)
    {
        jumpForce -= jumpDecrease;
        moveSpeed -= speedDecrease;
        fov -= fovChange;
        decreased = true;

        if (decreaseCoroutine != null) StopCoroutine(decreaseCoroutine);
        decreaseCoroutine = StartCoroutine(DecreaseCountDown());
    }
    else
    {
        if (decreaseCoroutine != null) StopCoroutine(decreaseCoroutine);
        decreaseCoroutine = StartCoroutine(DecreaseCountDown());
    }
}

IEnumerator BoostCountDown()
{
    yield return new WaitForSeconds(60);

    jumpForce -= jumpBoost;
    moveSpeed -= moveSpeedBoost;
    fov -= fovChange;
    boosted = false;
    boostCoroutine = null;
}

IEnumerator DecreaseCountDown()
{
    yield return new WaitForSeconds(20);

    jumpForce += jumpDecrease;
    moveSpeed += speedDecrease;
    fov += fovChange;
    decreased = false;
    decreaseCoroutine = null;
}
}
