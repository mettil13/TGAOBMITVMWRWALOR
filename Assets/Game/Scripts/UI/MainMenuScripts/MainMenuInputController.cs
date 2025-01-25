using UnityEngine;
using UnityEngine.UI;

public class CreditsInputController : MonoBehaviour
{
    [SerializeField] private Button[] MainMenuButtons;

    private Button[] currentButtons;
    private int currentIndex = 0;

    private void Start()
    {
        // Inizializza currentButtons con MainMenuButtons
        currentButtons = MainMenuButtons;

        if (currentButtons != null && currentButtons.Length > 0)
        {
            UpdateButtonSelection();
        }
    }

    public void OnUp()
    {
        Debug.Log("su");
        if (currentButtons == null || currentButtons.Length == 0) return;

        currentIndex = (currentIndex - 1 + currentButtons.Length) % currentButtons.Length;
        UpdateButtonSelection();
    }

    public void OnDown()
    {
        if (currentButtons == null || currentButtons.Length == 0) return;

        currentIndex = (currentIndex + 1) % currentButtons.Length;
        UpdateButtonSelection();
    }

    public void OnConfirm()
    {
        if (currentButtons == null || currentButtons.Length == 0) return;

        currentButtons[currentIndex].onClick.Invoke();
    }



    private void UpdateButtonSelection()
    {
        if (currentButtons == null || currentButtons.Length == 0) return;

        for (int i = 0; i < currentButtons.Length; i++)
        {
            var button = currentButtons[i];
            var colors = button.colors;

            if (i == currentIndex)
            {
                colors.normalColor = Color.yellow;
                colors.highlightedColor = Color.yellow;
                colors.selectedColor = Color.yellow;
            }
            else
            {
                colors.normalColor = Color.white;
                colors.highlightedColor = Color.gray;
                colors.selectedColor = Color.white;
            }

            button.colors = colors;

            button.GetComponent<Image>().color = colors.normalColor;
        }
    }
}
