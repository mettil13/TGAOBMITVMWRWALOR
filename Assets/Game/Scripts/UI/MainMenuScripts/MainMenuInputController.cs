using UnityEngine;
using UnityEngine.UI;

public class MainMenuInputController : MonoBehaviour
{
    [SerializeField] private Button[] MainMenuButtons;


    private Button[] currentButtons;
    private int currentIndex = 0;

    private void Start()
    {
        currentButtons = null;
        currentIndex = 0;
    }

    public void OnUp()
    {
        if (currentButtons == null) return;

        currentIndex = (currentIndex - 1 + currentButtons.Length) % currentButtons.Length;
    }

    public void OnDown()
    {
        if (currentButtons == null) return;

        currentIndex = (currentIndex + 1) % currentButtons.Length;
    }


    public void OnConfirm()
    {
        if (currentButtons == null || currentButtons.Length == 0) return;

        currentButtons[currentIndex].onClick.Invoke();
    }
}
