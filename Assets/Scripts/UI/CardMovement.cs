using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<GameObject> cards;
    public float spreadAmount = 50f;
    public float jumpHeight = 200f;
    public float smoothSpeed = 5f;

    private Vector3[] originalPositions;
    private Quaternion[] originalRotations;
    private bool isHovering = false;
    private Coroutine currentCoroutine;

    private void Start()
    {
        originalPositions = new Vector3[cards.Count];
        originalRotations = new Quaternion[cards.Count];

        for (int i = 0; i < cards.Count; i++)
        {
            originalPositions[i] = cards[i].transform.localPosition;
            originalRotations[i] = cards[i].transform.localRotation;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isHovering)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(SmoothSpreadCards());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(SmoothResetCards());
    }

    private IEnumerator SmoothSpreadCards()
    {
        isHovering = true;
        int hoveredCardIndex = cards.IndexOf(gameObject);

        while (isHovering)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                Vector3 targetPosition = originalPositions[i];
                Quaternion targetRotation = originalRotations[i];

                if (i < hoveredCardIndex)
                {
                    targetPosition = originalPositions[i] + Vector3.left * spreadAmount * (hoveredCardIndex - i);
                }
                else if (i > hoveredCardIndex)
                {
                    targetPosition = originalPositions[i] + Vector3.right * spreadAmount * (i - hoveredCardIndex);
                }

                if (i == hoveredCardIndex)
                {
                    targetPosition += Vector3.up * jumpHeight;
                    targetRotation = Quaternion.Euler(0, 0, 0);
                }

                cards[i].transform.localPosition = Vector3.Lerp(cards[i].transform.localPosition, targetPosition, Time.deltaTime * smoothSpeed);
                cards[i].transform.localRotation = Quaternion.Lerp(cards[i].transform.localRotation, targetRotation, Time.deltaTime * smoothSpeed);
            }

            yield return null;
        }
    }

    private IEnumerator SmoothResetCards()
    {
        isHovering = false;

        while (!isHovering)
        {
            bool allReset = true;
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].transform.localPosition = Vector3.Lerp(cards[i].transform.localPosition, originalPositions[i], Time.deltaTime * smoothSpeed);
                cards[i].transform.localRotation = Quaternion.Lerp(cards[i].transform.localRotation, originalRotations[i], Time.deltaTime * smoothSpeed);

                if (Vector3.Distance(cards[i].transform.localPosition, originalPositions[i]) > 0.01f || Quaternion.Angle(cards[i].transform.localRotation, originalRotations[i]) > 0.1f)
                {
                    allReset = false;
                }
            }

            if (allReset)
            {
                break;
            }

            yield return null;
        }
    }
}
