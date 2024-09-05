using UnityEngine;
using UnityEngine.UI;

public class RenderCard : MonoBehaviour
{
    public int cardIndex;
    private bool isRendered = false;

    void Update()
    {
        if (!isRendered)
        {
            Hand hand = transform.parent.parent.parent.GetComponent<Hand>();
            GetComponent<Image>().sprite = hand.GetCard(cardIndex).GetSprite();

            // Change alpha
            if (hand.GetCard(cardIndex).GetCardType() == CardType.None)
            {
                Color colour = GetComponent<Image>().color;
                colour.a = 0.1f;
                GetComponent<Image>().color = colour;
            }
            else
            {
                Color colour = GetComponent<Image>().color;
                colour.a = 1.0f;
                GetComponent<Image>().color = colour;
            }

            isRendered = true;
        }
    }

    public void Render()
    {
        isRendered = false;
    }
}
