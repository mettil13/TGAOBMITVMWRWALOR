using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private Image image;
    [SerializeField] private Color onHoverColor;

    public void ChangeColor()
    {
        AllButtonsDefaultColor();
        image.color = onHoverColor;
    }

    public void BackToDefaultColor()
    {
        image.color = Color.white;
    }

    private void AllButtonsDefaultColor()
    {
        foreach (var button in buttons)
        {
            button.image.color = Color.white;
        }
    }
}
