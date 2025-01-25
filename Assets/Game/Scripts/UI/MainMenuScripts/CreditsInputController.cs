using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuInputController : MonoBehaviour
{ 
    public void OnPause()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
