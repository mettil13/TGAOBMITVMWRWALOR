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

        if (textMeshPro.transform.position.y >= 5600) return;
        
        if (autoScroll && contentRect != null)
        {
            textMeshPro.transform.position += new Vector3(0, .8f, 0) * Time.deltaTime * 200;
        }
    }

    public void ToggleAutoScroll(bool enabled)
    {
        autoScroll = enabled;
    }
}
