using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class TooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltipTextObject;
    public string tooltipText;
    public Vector2 offset;
    public Camera uiCamera;
    public float fadeDuration;

    private TMP_Text tooltipTMPText;
    private RectTransform tooltipRectTransform;
    private Canvas canvas;

    void Start()
    {
        tooltipTMPText = tooltipTextObject.GetComponent<TMP_Text>();
        tooltipRectTransform = tooltipTextObject.GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        SetAlpha(0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipTMPText.text = tooltipText;
        tooltipTextObject.SetActive(true);
        UpdateTooltipPosition();
        StartCoroutine(Fade(0f, 1f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(Fade(1f, 0f));
    }

    void Update()
    {
        if (tooltipTextObject.activeSelf)
        {
            UpdateTooltipPosition();
        }
    }

    private void UpdateTooltipPosition()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            uiCamera,
            out localPoint
        );
        tooltipRectTransform.localPosition = localPoint + offset;
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0;
        Color color = tooltipTMPText.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            tooltipTMPText.color = color;
            yield return null;
        }
        color.a = endAlpha;
        tooltipTMPText.color = color;
    }

    private void SetAlpha(float alpha)
    {
        Color color = tooltipTMPText.color;
        color.a = alpha;
        tooltipTMPText.color = color;
    }
}
