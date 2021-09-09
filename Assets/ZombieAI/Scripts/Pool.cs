using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pool : MonoBehaviour
{
    GameObject[] gameObjects;
    int m_numObjects = 0;
    public GameObject go;
    public int initialQuantity = 10;
    // [SerializeField]
    private int alreadyInPool = 0;
    // [SerializeField]
    private int totalInPool = 0;

    void Start()
    {
        CreatePool(go, initialQuantity);
    }

    private void CheckPool()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            gameObjects[i] = transform.GetChild(i).gameObject;
            alreadyInPool++;
        }
    }

    private void CreatePool(GameObject go, int numObjects)
    {
        m_numObjects = numObjects + transform.childCount;
        gameObjects = new GameObject[m_numObjects];
        CheckPool();
        for (int i = alreadyInPool; i < m_numObjects; i++)
        {
            gameObjects[i] = GameObject.Instantiate(go);
            gameObjects[i].transform.parent = gameObject.transform;
            gameObjects[i].SetActive(false);
        }
        totalInPool = transform.childCount;
    }

    public GameObject ActivateNext(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < m_numObjects; i++)
        {
            if (!gameObjects[i].activeSelf)
            {

                gameObjects[i].transform.position = position;
                gameObjects[i].transform.rotation = rotation;
                gameObjects[i].SetActive(true);
                return (gameObjects[i]);
            }
        }
        return null;
    }
}
