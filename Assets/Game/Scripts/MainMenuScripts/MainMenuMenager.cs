using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private AudioClip welcomeToClip;
    [SerializeField] private ParticleSystem particleSystems;

    private void Start()
    {
        StartCoroutine(WelcomeToGame());
    }

    private void Update()
    {
        int randomValue = Random.Range(0, 256);
        AnimateParticleColor(5,255,45);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Exit() => Application.Quit();

    IEnumerator WelcomeToGame()
    {
        yield return new WaitForSeconds(.7f);

        AudioManager.instance.PlaySound(welcomeToClip);
    }

    public void AnimateParticleColor(float r, float g, float b)
    {
        var particleColor = particleSystems.colorOverLifetime.color;


    }
}
