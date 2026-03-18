using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class DogMovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveForce = 10f;
    public float maxSpeed = 3f;
    public float walkDistance = 3f;
    public float turnSpeed = 200f;

    [Header("Breeding")]
    public float breedMoveSpeed = 2f;
    public float breedDistance = 1.5f;
    public GameObject puppyPrefab;

    private Rigidbody rb;
    private Animator animator;

    private Vector3 startPosition;
    private bool isTurning = false;
    private float targetYRotation;

    public bool breeding = false;
    private bool isBreedingActive = false;
    private bool hasStartedBreeding = false;

    private Transform targetMate;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        if (!breeding)
        {
            if (!isTurning) MoveForward();
            else Turn();

            UpdateAnimation();
        }
        else
        {
            if (targetMate == null)
            {
                targetMate = FindClosestMate();
                if (targetMate == null) return;
            }

            BreedTowardsTarget();
        }
    }

    void MoveForward()
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

    void Turn()
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

    void UpdateAnimation()
    {
        animator.SetBool("Walking", !isTurning && rb.linearVelocity.magnitude > 0.1f);
    }

    void BreedTowardsTarget()
    {
        Vector3 direction = (targetMate.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetMate.position);

        // Slight random offset to prevent overlapping rigidbodies
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
                animator.SetBool("LookUp", true);

                // Only spawn puppy if mate is adult dog
                if (targetMate.GetComponent<DogMovement>() != null)
                    Invoke(nameof(SpawnPuppy), 1f);

                // Notify puppy mate if needed
                PuppyMovement pup = targetMate.GetComponent<PuppyMovement>();
                if (pup != null)
                    pup.breeding = true;

                // End breeding act
                Invoke(nameof(EndBreeding), 2f);
            }
        }
    }

    void SpawnPuppy()
    {
        if (puppyPrefab == null || targetMate == null) return;

        // Only one adult spawns puppy
        if (GetInstanceID() < targetMate.GetInstanceID())
        {
            Vector3 spawnPos = (transform.position + targetMate.position) / 2f;
            spawnPos.y += 0.1f; // small offset to avoid overlap
            Instantiate(puppyPrefab, spawnPos, Quaternion.identity);
        }
    }

    Transform FindClosestMate()
    {
        GameObject[] dogs = GameObject.FindGameObjectsWithTag("Dog");
        GameObject[] puppies = GameObject.FindGameObjectsWithTag("Puppy");

        Transform closest = null;
        float minDistance = Mathf.Infinity;

        // Check adult dogs
        foreach (GameObject dog in dogs)
        {
            if (dog.transform == transform) continue;
            DogMovement other = dog.GetComponent<DogMovement>();
            if (other == null || other.isBreedingActive) continue;

            float dist = Vector3.Distance(transform.position, dog.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = dog.transform;
            }
        }

        // Check puppies ready to breed
        foreach (GameObject pup in puppies)
        {
            PuppyMovement puppy = pup.GetComponent<PuppyMovement>();
            if (puppy == null || puppy.isDead || !puppy.isReadyToBreed) continue;

            float dist = Vector3.Distance(transform.position, pup.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = pup.transform;
            }
        }

        return closest;
    }

    void EndBreeding()
    {
        breeding = false;
        isBreedingActive = false;
        targetMate = null;
        hasStartedBreeding = false;
        animator.SetBool("LookUp", false);
    }
}
