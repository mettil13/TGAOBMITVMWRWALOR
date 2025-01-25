using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Color onHoverColor;

    public void ChangeColor()
    {
        image.color = onHoverColor;
    }

    public void BackToDefaultColor()
    {
        image.color = Color.white;
    }
}
