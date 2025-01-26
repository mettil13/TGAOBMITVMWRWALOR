using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsInputController : MonoBehaviour
{
    [SerializeField] private List<Button> MainMenuButtons = new();

    public int currentIndex = 0;

    private void Start()
    {
        if (MainMenuButtons != null && MainMenuButtons.Count > 0)
        {
            UpdateButtonSelection();
        }
    }

    public void OnUp()
    {
        if (MainMenuButtons == null) return;

        currentIndex = (currentIndex - 1 + MainMenuButtons.Count) % MainMenuButtons.Count;
        UpdateButtonSelection();
    }

    public void OnDown()
    {
        if (MainMenuButtons == null) return;

        currentIndex = (currentIndex + 1) % MainMenuButtons.Count;
        UpdateButtonSelection();
    }

    public void OnConfirm()
    {
        if (MainMenuButtons == null || MainMenuButtons.Count == 0) return;

        MainMenuButtons[currentIndex].onClick.Invoke();
    }



    private void UpdateButtonSelection()
    {
        if (MainMenuButtons == null || MainMenuButtons.Count == 0) return;

        for (int i = 0; i < MainMenuButtons.Count; i++)
        {
            var button = MainMenuButtons[i];
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
