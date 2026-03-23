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



    public GameObject puppyPrefab;

    private Rigidbody rb;
    private Animator animator;

    private Vector3 startPosition;
    private bool isTurning = false;
    private float targetYRotation;

    public bool breeding = false;

    public float breedDelay = 2f;
    private bool isBreedingInProgress = false;
    KarmaSystem karmaSystem;
    public float karmaFrom;
    public bool isDead = false;
 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        karmaSystem = GameObject.FindGameObjectWithTag("Karma").GetComponent<KarmaSystem>();
    }

void FixedUpdate()
{
    if (isDead) return;


    if (isBreedingInProgress) return;

    if (!breeding)
    {
        if (!isTurning) MoveForward();
        else Turn();

        UpdateAnimation();
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

 

void SpawnPuppy()
{
    Vector3 spawnPos = transform.position + transform.forward * 1.5f;
    Instantiate(puppyPrefab, spawnPos, Quaternion.identity);
}
    public void StartBreeding()
{
    if (isBreedingInProgress) return;

    breeding = true;
    isBreedingInProgress = true;

    rb.linearVelocity = Vector3.zero; // stop movement
    animator.SetBool("Walking", false);
    animator.SetBool("LookUp", true);

    StartCoroutine(BreedRoutine());
}
IEnumerator BreedRoutine()
{
    yield return new WaitForSeconds(breedDelay);

    SpawnPuppy();
    karmaSystem.GetKarma(karmaFrom);

    animator.SetBool("LookUp", false);

    breeding = false;
    isBreedingInProgress = false;
}

public void Die()
{
    if (isDead) return;

    isDead = true;

    // Stop movement
    rb.linearVelocity = Vector3.zero;
    rb.angularVelocity = Vector3.zero;

    // Remove movement constraints so it can fall
    rb.constraints = RigidbodyConstraints.None;

    // Optional: small push so it falls over
    rb.AddTorque(transform.right * 5f, ForceMode.Impulse);

    // Stop animations
    animator.SetBool("Walking", false);
    animator.SetBool("LookUp", false);

    karmaSystem.LoseKarma(10f);

    // Optional: disable animator completely so physics takes over
    animator.enabled = false;
}

   
}
