using UnityEngine;
using UnityEngine.AI;

public class TaxManAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public float health;
    private GameObject hitBox;
    Animator animator;
    public GameObject money;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        hitBox = GameObject.FindGameObjectWithTag("Hitbox");
        animator = GetComponent<Animator>();
        animator.SetBool("Walking",true);
    }
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange) Patroling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInAttackRange && playerInSightRange) AttackPlayer();

    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(new Vector3(player.position.x, gameObject.transform.position.y, player.position.z));
    }
    
    private void AttackPlayer()
    {
        //agent.SetDestination(new Vector3(player.position.x, gameObject.transform.position.y, player.position.z));
        transform.LookAt(new Vector3(player.position.x, gameObject.transform.position.y, player.position.z));

        

        if (!alreadyAttacked)
        {
            animator.SetTrigger("Hit");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            DestroyEnemy();
        }

    }
    public void DestroyEnemy()
    {
        Instantiate(money, gameObject.transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
    public void ActivateHitbox()
    {
        hitBox.SetActive(true);
    }
    public void DeActivateHitbox()
    {
        hitBox.SetActive(false);
    }

}
