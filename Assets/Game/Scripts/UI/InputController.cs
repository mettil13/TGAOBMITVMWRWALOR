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

        proceduralGeneration.RiverGeneration.instance.playerInput.quitPressed.AddListener(OnPause);
        proceduralGeneration.RiverGeneration.instance.playerInput.applyPressed.AddListener(OnConfirm);
        proceduralGeneration.RiverGeneration.instance.playerInput.backPressed.AddListener(OnBack);
        proceduralGeneration.RiverGeneration.instance.playerInput.verticalChanged.AddListener(OnVerticalChanged);
        //proceduralGeneration.RiverGeneration.instance.playerInput.horizontalChanged.AddListener();
    }

    float lastVertical = 0;
    public void OnVerticalChanged(float value)
    {
        if (value == 0 || (lastVertical != 0 && Mathf.Sign(lastVertical) == Mathf.Sign(value)))
        {
            lastVertical = value;
            return;
        }

        if (value < 0)
        {
            lastVertical = value;
            OnDown();
        }
        else
        {
            lastVertical = value;
            OnUp();
        }
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
        if (PagesManager.instance.pages[1].activeSelf) return;

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
        if (PagesManager.instance == null || PagesManager.instance.pages[1].activeSelf) return;

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
        if (pageName == "Pause")
        {
            currentButtons = pauseButtons;
        }
        else if (pageName == "Lose")
        {
            currentButtons = looseButtons;
        }
        else
        {
            currentButtons = null;
        }

        UpdateButtonSelection();
        currentIndex = 0;
    }
}
