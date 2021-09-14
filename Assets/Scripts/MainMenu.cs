using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Animator animator;
    public Image fadeOutImage;
    public AudioSource audioSource;
    public AudioClip scream;
    public AudioClip die;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DoFadeOut()
    {
        animator.SetBool("FadeOut", true);
    }


    public void GoToScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ExecuteAction(string action)
    {
        StartCoroutine(DelayedAction(2f, action));
    }

    IEnumerator DelayedAction(float time, string action)
    {

        if (action == "Exit")
        {
            audioSource.PlayOneShot(die);
        }
        else
        {
            audioSource.PlayOneShot(scream);
        }
        yield return new WaitForSeconds(time);
        if (action == "Exit")
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(action);
        }
    }
}
