using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private float dampingSpeed = 0.1f;
    private RectTransform rect;
    private Vector3 originalPosition;
    private Vector3 velocity = Vector3.zero;
    private Coroutine currentMovement;
    private Coroutine currentFade;
    private GameObject dropZone;

    void Awake()
    {
        rect = transform as RectTransform;
        originalPosition = rect.position;
        dropZone = transform.parent.Find("DropZone").gameObject;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentFade != null)
        {
            StopCoroutine(currentFade);
        }
        currentFade = StartCoroutine(Fade(1f));
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, eventData.position, eventData.pressEventCamera, out var globalMousePosition))
        {
            if (currentMovement != null)
            {
                StopCoroutine(currentMovement);
            }
            currentMovement = StartCoroutine(MoveCard(globalMousePosition));
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentMovement != null)
        {
            StopCoroutine(currentMovement);
        }
        currentMovement = StartCoroutine(MoveCard(originalPosition));

        if (currentFade != null)
        {
            StopCoroutine(currentFade);
        }
        currentFade = StartCoroutine(Fade(0f));
    }

    private IEnumerator MoveCard(Vector3 targetPosition)
    {
        while (Vector3.Distance(rect.position, targetPosition) > 0.001f)
        {
            rect.position = Vector3.SmoothDamp(rect.position, targetPosition, ref velocity, dampingSpeed);
            yield return null;
        }
        rect.position = targetPosition;
    }

    private IEnumerator Fade(float targetAlpha)
    {
        CanvasGroup canvasGroup = dropZone.GetComponent<CanvasGroup>();
        float startAlpha = canvasGroup.alpha;
        float fadeDuration = 0.2f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}
