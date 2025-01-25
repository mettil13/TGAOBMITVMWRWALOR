using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public ScrollRect scrollRect;
    private RectTransform contentRect;
    public TextMeshProUGUI textMeshPro;

    public float scrollSpeed = 20f;

    private float targetPosition = 0f;
    private bool autoScroll = true;

    void Start()
    {
        if (scrollRect != null)
        {
            contentRect = scrollRect.content;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");

        if (autoScroll && contentRect != null)
        {
            textMeshPro.transform.position += new Vector3(0, .4f, 0);
        }
    }

    public void ToggleAutoScroll(bool enabled)
    {
        autoScroll = enabled;
    }
}
