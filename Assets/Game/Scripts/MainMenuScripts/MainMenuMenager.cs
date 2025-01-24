using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private AudioClip welcomeToClip;

    private void Start()
    {
        StartCoroutine(WelcomeToGame());
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Exit() => Application.Quit();

    IEnumerator WelcomeToGame()
    {
        yield return new WaitForSeconds(3);

        AudioManager.instance.PlaySound(welcomeToClip);
    }
}
