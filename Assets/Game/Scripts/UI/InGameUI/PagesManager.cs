using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PagesManager : MonoBehaviour
{
    public static PagesManager instance;

    [SerializeField] public GameObject[] pages;
    [SerializeField] private InputController inputController;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1.0f;

    }

    public void ClosePages()
    {
        Time.timeScale = 1;

        foreach (var page in pages)
            page.SetActive(false);

    }

    public void OpenPage(string pageName)
    {
        ClosePages();

        Time.timeScale = 0;

        foreach (Pages pageEnum in System.Enum.GetValues(typeof(Pages)))
        {
            if (pageEnum.ToString().ToLower() == pageName.ToLower())
            {
                int pageIndex = (int)pageEnum;
                if (pageIndex >= 0 && pageIndex < pages.Length)
                {
                    pages[pageIndex].SetActive(true);
                }
                else
                {
                    Debug.LogWarning($"Page index {pageIndex} is out of range.");
                }
                inputController.OnPageChange(pageName);
                return;
            }
        }


        Debug.LogWarning($"Page with name \"{pageName}\" not found in enum Pages.");
    }
}

public enum Pages
{
    Pause = 0,
    Lose = 1
}
