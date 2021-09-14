using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas loseCanvas;
    public GameObject player;
    public AudioMixer audioMixer;
    public Text timerText;

    private float startTime;
    public float currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerHealth>().OnZeroHealth += GameOver;
        startTime = Time.time;
        audioMixer.SetFloat("ZombiesVolume", 0f);
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
        string minutes = ((int)currentTime / 60).ToString();
        string seconds = ((int)currentTime % 60).ToString("D2");
        string decimals = ((int)(currentTime * 100) % 100).ToString("D2");

        timerText.text = $"{minutes}:{seconds}.{decimals}";
        Cursor.lockState = CursorLockMode.None;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
