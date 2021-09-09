using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIState { idle, chasing, attack, dying, none };
public class EnemyAI : MonoBehaviour
{
    public Transform target;
    NavMeshAgent navMeshAgent;

    public float distanceThreshold = 8f;
    public float speed = 1f;

    

    public AIState aiState = AIState.idle;
    public Animator animator;
    public float attackThreshold = 1.5f;
    public EnemyHealth health;
    public bool backToIdle = false;
    public bool scream = false;

    void OnEnable()
    {
        InitializeEnemy();
        ReviveIdling();
    }
    void InitializeEnemy()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Think());        
        navMeshAgent.speed = speed;
        health = GetComponent<EnemyHealth>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (health.value == 0 && aiState != AIState.dying)
        {
            StopCoroutine(Think());
            aiState = AIState.dying;
            navMeshAgent.isStopped = true;
            animator.CrossFadeInFixedTime("Die", 0.1f);
            // Destroy(gameObject, 4f);
            StartCoroutine(Die(4f));   
        }
    }

    IEnumerator Die(float dyingTime)
    {
        yield return new WaitForSeconds(dyingTime);
        gameObject.SetActive(false);
        yield break;
    }

    public void ReviveIdling()
    {
        animator.CrossFadeInFixedTime("Idle", 0.1f);
        aiState = AIState.idle;
        navMeshAgent.speed = speed;
    }

    public void ReviveChasing()
    {
        animator.CrossFadeInFixedTime("Walk", 0.1f);
        animator.SetBool("Chasing", true);
        navMeshAgent.speed = speed;
        aiState = AIState.chasing;
    }


    IEnumerator Think()
    {
        while (true)
        {
            switch (aiState)
            {
                case AIState.idle:
                    float dist = Vector3.Distance(target.position, transform.position);
                    if (dist < distanceThreshold)
                    {
                        if (scream)
                        {
                            animator.CrossFadeInFixedTime("Scream", 0.1f);
                        }
                        aiState = AIState.chasing;
                        animator.SetBool("Chasing", true);
                        
                    }
                    navMeshAgent.SetDestination(transform.position);
                    break;
                case AIState.chasing:
                    dist = Vector3.Distance(target.position, transform.position);
                    if (dist > distanceThreshold && backToIdle)
                    {
                        aiState = AIState.idle;
                        animator.SetBool("Chasing", false);
                    }
                    if (dist < attackThreshold)
                    {
                        aiState = AIState.attack;
                        animator.SetBool("Attacking", true);
                    }
                    navMeshAgent.SetDestination(target.position);
                    break;
                case AIState.attack:
                    dist = Vector3.Distance(target.position, transform.position);
                    if (dist > attackThreshold)
                    {
                        aiState = AIState.chasing;
                        animator.SetBool("Attacking", false);
                    }
                    // navMeshAgent.SetDestination(transform.position);
                    navMeshAgent.SetDestination(target.position);
                    break;
                case AIState.dying:
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
