using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int HP=100;
    [SerializeField] private int attackDamage = 10;

    private Animator animator;

    private NavMeshAgent navAgent;

    private Player mainPlayer;

    public Transform player;

    public LayerMask Ground, Player;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    public float sightRange=20f, attackRange=12f;
    private float tempSightRange;
    public bool playerInSightRange, playerInAttackRange;

    public Transform bulletSpawn;

    void Start()
    {
        tempSightRange = sightRange;
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        mainPlayer = FindObjectOfType<Player>();
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if(HP <= 0)
        {
            animator.SetTrigger("Die");
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
        else
        {
            animator.SetTrigger("Damage");
        }
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        if (!playerInSightRange && !playerInAttackRange)
        {
            animator.SetBool("PATROLLING",true);
            animator.SetBool("CHASING", false);
            animator.SetBool("ATTACKING", false);
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange) 
        {
            animator.SetBool("CHASING", true);
            animator.SetBool("PATROLLING", false);
            animator.SetBool("ATTACKING", false);


            sightRange = 2*tempSightRange;
            ChasePlayer();
        }
        else
        {
            sightRange = tempSightRange;
        }

        if (playerInAttackRange && playerInSightRange) 
        {
            animator.SetBool("ATTACKING", true);
            animator.SetBool("PATROLLING", false);
            animator.SetBool("CHASING", false);
            
            AttackPlayer();
        } 
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            navAgent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 3f, Ground))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        navAgent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        navAgent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        mainPlayer.TakeDamage(attackDamage);

        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);
    }
}
