using UnityEngine;
using UnityEngine.UI;

public class RenderCard : MonoBehaviour
{
    public int cardIndex;

    private Hand hand;
    private CardType previousCardType;

    void Awake()
    {
        hand = transform.parent.parent.parent.GetComponent<Player>().hand;
    }

    void Update()
    {
        Card card = hand.GetCard(cardIndex);
        if (previousCardType != card.GetCardType())
        {
            previousCardType = card.GetCardType();
            GetComponent<Image>().sprite = card.GetSprite();

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
        }
    }
}
