using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float initailValue = 100f;
    public float value;
    public EnemyHealth parentReference;
    public float damageMultiplier = 1.0f;
    public EnemyAI enemyAI;
    public AudioSource audioSource;
    public AudioClip bulletImpact;


    public delegate void OnZeroHealthHandler();
    public event OnZeroHealthHandler OnZeroHealth;


    private Pool fleshImpactEffect;

    void Start()
    {
        fleshImpactEffect = GameObject.FindGameObjectWithTag("FleshImpactPool").GetComponent<Pool>(); ;
    }

    void OnEnable()
    {
        value = initailValue;
    }

    public void TakeDamage(float damage)
    {
        damage *= damageMultiplier;
        if (parentReference != null)
        {
            parentReference.TakeDamage(damage);
            return;
        }
        value -= damage;
        value = value < 0 ? 0 : value;
        if (value == 0)
        {
            OnZeroHealth?.Invoke();
        }
        if (enemyAI.aiState == AIState.idle)
        {
            enemyAI.aiState = AIState.chasing;
            enemyAI.animator.SetBool("Chasing", true);
        }
        // if (audioSource && bulletImpact)
        //     audioSource.PlayOneShot(bulletImpact);
    }

    public void TakeDamage(float damage, RaycastHit hit)
    {
        TakeDamage(damage);
        fleshImpactEffect.ActivateNext(hit.point, Quaternion.LookRotation(hit.normal));
    }
}
