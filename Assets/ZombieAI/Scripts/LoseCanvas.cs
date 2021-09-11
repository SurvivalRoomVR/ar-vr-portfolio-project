using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCanvas : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayLaugh()
    {
        audioSource.Play();
    }
}
