using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleshImpact : MonoBehaviour
{
    public ParticleSystem blodEffect;
    public float duration = 1f;
    private float currentTime = 0;
    void Start()
    {
        blodEffect = GetComponent<ParticleSystem>();
    }
    void OnEnable()
    {
        blodEffect.Play();
    }
    void OnDisable()
    {
        blodEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= duration)
        {
            currentTime = 0;
            gameObject.SetActive(false);
        }
    }
}
