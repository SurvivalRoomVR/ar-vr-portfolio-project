using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyAI enemyAI;
    public float damage = 10;

    // Update is called once per frame

    void OnCollisionEnter(Collision collision)
    {
        PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
        if (health && enemyAI.aiState == AIState.attack && collision.gameObject.tag == "Player")
        {
            health.TakeDamage(damage);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
        if (health && enemyAI.aiState == AIState.attack && collision.gameObject.tag == "Player")
        {
            health.TakeDamage(damage);
        }
    }
}
