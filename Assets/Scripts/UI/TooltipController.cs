using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class TooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltipTextObject;
    public string tooltipText;
    public float fadeDuration;

    private TMP_Text tooltipTMPText;

    void Start()
    {
        tooltipTMPText = tooltipTextObject.GetComponent<TMP_Text>();
        SetAlpha(0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipTMPText.text = tooltipText;
        Vector3 mousePos = Input.mousePosition;
        tooltipTextObject.transform.position = mousePos + new Vector3(0f,-30f, 0f);
        tooltipTextObject.SetActive(true);
        StartCoroutine(Fade(0f, 1f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            SetAlpha(Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration));
            yield return null;
        }
        SetAlpha(endAlpha);
    }

    private void SetAlpha(float alpha)
    {
        Color color = tooltipTMPText.color;
        color.a = alpha;
        tooltipTMPText.color = color;
    }
}
