using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public Canvas loseCanvas;
    public GameObject player;
    public AudioMixer audioMixer;

    private float startTime;
    public float currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerHealth>().OnZeroHealth += GameOver;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time - startTime;
    }
    public void GameOver()
    {
        loseCanvas.GetComponent<Animator>().CrossFadeInFixedTime("LoseCanvas", 0.1f);
        audioMixer.SetFloat("ZombiesVolume", -80.0f);
        Debug.Log(currentTime);
    }
}
