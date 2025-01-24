using UnityEngine.UI;
using UnityEngine;

public class BackgroundImage : MonoBehaviour
{
    [SerializeField]
    private Vector2 speed;
    [SerializeField, Range(0, 1)]
    private float matchWidthOrHeight = 0;

    RawImage image;

    void Awake()
    {
        image = GetComponent<RawImage>();
    }

    void Update()
    {
        Rect rect = image.uvRect;
        rect.position = new Vector2(
            (rect.position.x + speed.x * Time.deltaTime) % 1,
            (rect.position.y + speed.y * Time.deltaTime) % 1
        );
        rect.width = rect.width * (1f - matchWidthOrHeight) + matchWidthOrHeight * rect.height * Screen.width / Screen.height;
        rect.height = rect.height * matchWidthOrHeight + (1f - matchWidthOrHeight) * rect.width * Screen.height / Screen.width;
        image.uvRect = rect;
    }
}