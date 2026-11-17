using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class UIBackgroundCover : MonoBehaviour
{
    private RectTransform rectTransform;
    private Canvas canvas;

    private float imageAspect;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        Image image = GetComponent<Image>();

        if (image != null && image.sprite != null)
        {
            imageAspect = image.sprite.rect.width /
                          image.sprite.rect.height;
        }
    }

    void Start()
    {
        UpdateBackground();
    }

    void UpdateBackground()
    {
        if (imageAspect <= 0) return;

        RectTransform parent =
            rectTransform.parent as RectTransform;

        if (parent == null) return;

        //Padre es el canvas
        float screenAspect =
            parent.rect.width / parent.rect.height;

        if (screenAspect > imageAspect)
        {
            // Pantalla relativamente más ancha
            rectTransform.sizeDelta = new Vector2(
                parent.rect.width,
                parent.rect.width / imageAspect
            );
        }
        else
        {
            // Pantalla relativamente más alta
            rectTransform.sizeDelta = new Vector2(
                parent.rect.height * imageAspect,
                parent.rect.height
            );
        }

        rectTransform.anchoredPosition = Vector2.zero;
    }
}