using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public Canvas loseCanvas;
    public GameObject player;
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerHealth>().OnZeroHealth += GameOver;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GameOver()
    {
        loseCanvas.GetComponent<Animator>().CrossFadeInFixedTime("LoseCanvas", 0.1f);
        audioMixer.SetFloat("ZombiesVolume", -80.0f);
    }
}
