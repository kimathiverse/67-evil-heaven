using UnityEngine;

public class PuppyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveForce = 5f;
    public float maxSpeed = 2f;
    public float walkDistance = 2f;
    public float turnSpeed = 150f;

    [Header("Breeding")]
    public float breedMoveSpeed = 2f;
    public float breedDistance = 1.5f;

    private Rigidbody rb;
    private Animator animator;

    private Vector3 startPosition;
    private bool isTurning = false;
    private float targetYRotation;

    public bool breeding = false;          // internal state during act
    public bool isReadyToBreed = false;    // set when player gives bone
    public bool isDead = false;

    private Transform targetDog;
    private bool hasStartedBreeding = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        if (isDead) return;

        if (!breeding && !isReadyToBreed)
        {
            Wander();
        }
        else
        {
            // If ready to breed, dog will approach puppy
            if (targetDog == null)
            {
                targetDog = FindClosestDogOrAdult();
                if (targetDog == null) return;
            }

            BreedTowardsTarget();
        }
    }

    void Wander()
    {
        if (!isTurning)
        {
            if (rb.linearVelocity.magnitude < maxSpeed)
                rb.AddForce(transform.forward * moveForce, ForceMode.Acceleration);

            if (Vector3.Distance(startPosition, transform.position) >= walkDistance)
            {
                isTurning = true;
                rb.linearVelocity = Vector3.zero;
                targetYRotation = transform.eulerAngles.y + Random.Range(45, 100);
            }
        }
        else
        {
            float currentY = transform.eulerAngles.y;
            float newY = Mathf.MoveTowardsAngle(currentY, targetYRotation, turnSpeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Euler(0, newY, 0);

            if (Mathf.Abs(Mathf.DeltaAngle(newY, targetYRotation)) < 1f)
            {
                transform.rotation = Quaternion.Euler(0, targetYRotation, 0);
                isTurning = false;
                startPosition = transform.position;
            }
        }

        animator.SetBool("Walking", !isTurning && rb.linearVelocity.magnitude > 0.1f);
    }

    void BreedTowardsTarget()
    {
        if (targetDog == null) return;

        Vector3 direction = (targetDog.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetDog.position);

        // Add slight separation if overlapping
        if (distance < 0.5f)
            direction += Random.insideUnitSphere * 0.1f;

        if (distance > breedDistance)
        {
            rb.MovePosition(transform.position + direction * breedMoveSpeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.fixedDeltaTime);
            animator.SetBool("Walking", true);
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
            animator.SetBool("Walking", false);
            transform.rotation = Quaternion.LookRotation(direction);

            if (!hasStartedBreeding)
            {
                hasStartedBreeding = true;
                breeding = true;
                animator.SetBool("LookUp", true);

                // Delay death for puppy after breeding act
                Invoke(nameof(Die), 2f);
            }
        }
    }

    Transform FindClosestDogOrAdult()
    {
        GameObject[] dogs = GameObject.FindGameObjectsWithTag("Dog");
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        // Look for adults
        foreach (GameObject dog in dogs)
        {
            DogMovement d = dog.GetComponent<DogMovement>();
            if (d == null ) continue;

            float distance = Vector3.Distance(transform.position, dog.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = dog.transform;
            }
        }

        return closest;
    }

    public void GiveBone()
    {
        // Called by player
        isReadyToBreed = true;
    }

    void Die()
    {
        isDead = true;
        breeding = false;
        isReadyToBreed = false;

        rb.linearVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.None;
        rb.AddTorque(Vector3.right * 5f, ForceMode.Impulse);

        animator.SetBool("Walking", false);
        animator.SetBool("LookUp", false);
    }
 }

