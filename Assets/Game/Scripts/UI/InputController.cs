using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button[] pauseButtons;
    [SerializeField] private GameObject looseMenu;
    [SerializeField] private Button[] looseButtons;

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
        UpdateButtonSelection();
    }

    public void OnDown()
    {
        if (currentButtons == null) return;

        currentIndex = (currentIndex + 1) % currentButtons.Length;
        UpdateButtonSelection();
    }

    public void OnBack()
    {
        PagesManager.instance.ClosePages();
        currentButtons = null;
        currentIndex = 0;
    }

    public void OnConfirm()
    {
        if (currentButtons == null || currentButtons.Length == 0) return;

        currentButtons[currentIndex].onClick.Invoke();
    }

    public void OnPause()
    {
        if (PagesManager.instance == null) return;

        if (pauseMenu.activeSelf)
        {
            PagesManager.instance.ClosePages();
            currentButtons = null;
            currentIndex = 0;
        }
        else
        {
            PagesManager.instance.OpenPage("Pause");
            currentButtons = pauseButtons;
            currentIndex = 0;
            UpdateButtonSelection();
        }
    }

    private void UpdateButtonSelection()
    {
        if (currentButtons == null || currentButtons.Length == 0) return;

        for (int i = 0; i < currentButtons.Length; i++)
        {
            var colors = currentButtons[i].colors;
            colors.normalColor = i == currentIndex ? Color.yellow : Color.white;
            currentButtons[i].colors = colors;
        }
    }

    public void OnPageChange(string pageName)
    {
        PagesManager.instance.OpenPage(pageName);

        if (pageName == "Pause")
        {
            currentButtons = pauseButtons;
        }
        else if (pageName == "Loose")
        {
            currentButtons = looseButtons;
        }
        else
        {
            currentButtons = null;
        }

        currentIndex = 0;
        UpdateButtonSelection();
    }
}
