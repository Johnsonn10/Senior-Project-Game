using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.AI;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;
    private Animator animator;

    private Transform enemyTransform;

    [SerializeField] LayerMask groundLayer, playerLayer;
    [SerializeField] float attackRange;

    public BoxCollider boxCollider;

    [SerializeField] private int HP = 100;

    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        enemyTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            if (agent.velocity.magnitude > 0.1f)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

            if (Physics.CheckSphere(transform.position, attackRange, playerLayer))
            {
                Attack();
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie Reaction Hit"))
            {
                agent.SetDestination(enemyTransform.position);
            }
            else
            {
                agent.destination = player.transform.position;
            }
        }
        else
        {
            agent.SetDestination(enemyTransform.position);
        }
    }

    void Attack()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie Attack"))
        {
            animator.SetTrigger("Attack");
            agent.SetDestination(enemyTransform.position);
        }
    }

    void EnableAttack()
    {
        boxCollider.enabled = true;
    }

    void DisableAttack()
    {
        boxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("HIT");
        }
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            isAlive = false;
            int randomValue = Random.Range(0, 2);

            if (randomValue == 0)
            {
                animator.SetTrigger("DIE1");
            }
            else
            {
                animator.SetTrigger("DIE2");
            }
            //Destroy(gameObject);
            StartCoroutine(DestroyZombie());    //maybe call a different method to animate death scene -> destroy
        }
        else
        {
            animator.SetTrigger("DAMAGE");
        }

    }

    private IEnumerator DestroyZombie()
    {
        print("die");
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
