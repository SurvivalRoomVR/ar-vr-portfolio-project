using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Pool enemyPool;
    public int quantity = 100;
    [SerializeField]
    private int count = 0;
    public float spawnRate = 3f;
    public float dropRadius = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DropEnemy(spawnRate));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DropEnemy (float time)
    {
        for (; count < quantity;)
        {
            Vector2 randomXZ= Random.insideUnitCircle * dropRadius;
            Vector3 dropPosition = new Vector3(randomXZ.x + transform.position.x, transform.position.y, randomXZ.y + transform.position.z);
            GameObject go = enemyPool.ActivateNext(dropPosition, transform.rotation);
            if (go != null)
            {
                if (go.GetComponent<EnemyAI>() != null)
                    go.GetComponent<EnemyAI>().ReviveChasing();
                count++;
            }
            yield return new WaitForSeconds(time);
        }
    }
}
