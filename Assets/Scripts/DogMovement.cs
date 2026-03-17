using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class DogMovement : MonoBehaviour
{

   public float moveForce = 10f;
    public float maxSpeed = 3f;
    public float walkDistance = 3f;
    public float turnSpeed = 200f;

    private Rigidbody rb;
    private Animator animator;

    private Vector3 startPosition;
    private bool isTurning = false;
    private float targetYRotation;
    public bool breeding;
    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        startPosition = transform.position;
        breeding = false;

        // Keep dog upright but allow Y rotation
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        if (!breeding)
        {
            if (!isTurning)
        {
            MoveForward();
        }
        else
        {
            Turn();
        }

        UpdateAnimation();
        }
        else
        {
            Breed();
        }

    }

    void MoveForward()
    {
        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * moveForce, ForceMode.Acceleration);
        }

        float distance = Vector3.Distance(startPosition, transform.position);

        if (distance >= walkDistance)
        {
            isTurning = true;

            // Stop movement cleanly
            rb.linearVelocity = Vector3.zero;

            targetYRotation = transform.eulerAngles.y + Random.Range(45, 100);
        }
    }

    void Turn()
    {
        float currentY = transform.eulerAngles.y;

        float newY = Mathf.MoveTowardsAngle(
            currentY,
            targetYRotation,
            turnSpeed * Time.fixedDeltaTime
        );

        transform.rotation = Quaternion.Euler(0, newY, 0);

        if (Mathf.Abs(Mathf.DeltaAngle(newY, targetYRotation)) < 1f)
        {
            transform.rotation = Quaternion.Euler(0, targetYRotation, 0);

            isTurning = false;
            startPosition = transform.position;
        }
    }

    void UpdateAnimation()
    {
        bool isWalking = !isTurning && rb.linearVelocity.magnitude > 0.1f;
        animator.SetBool("Walking", isWalking);
    }
    public void Breed()
    {
        animator.SetBool("LookUp", true);

    }
}
