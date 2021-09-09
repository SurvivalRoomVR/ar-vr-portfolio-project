using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationsHelpers : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public EnemyAI enemyAI;
    private float walkingSpeed;
    public AudioClip scream;
    public AudioClip groan;
    public AudioClip footStep;
    public AudioClip die;
    public AudioClip attack;

    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        walkingSpeed = enemyAI.speed;
    }
    public void SlowDownWalking()
    {
        navMeshAgent.speed = walkingSpeed * 0.5f;
    }

    public void ResumeWalking()
    {
        navMeshAgent.speed = walkingSpeed;
    }

    public void StopWalking()
    {
        navMeshAgent.speed = 0;
    }

    public void Scream()
    {
        if (audioSource == null || scream == null)
            return;
        audioSource.PlayOneShot(scream);
    }

    public void StopAudioSource()
    {
        if (audioSource == null)
            return;
        audioSource.Stop();
    }
    public void Groan()
    {
        if (audioSource == null || groan == null)
            return;
        if (!audioSource.isPlaying)
        {
            audioSource.clip = groan;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void Step()
    {
        if (audioSource == null || footStep == null)
            return;
        audioSource.PlayOneShot(footStep);
    }

    public void Die()
    {
        if (audioSource == null || die == null)
            return;
        audioSource.Stop();
        audioSource.PlayOneShot(die);
    }

    public void Attack()
    {
        if (audioSource == null || attack == null)
            return;
        audioSource.Stop();
        audioSource.PlayOneShot(attack);
    }
}
