using System.Collections;
using System.Collections.Generic;

public class Hand
{
    private List<Card> hand;

    public Hand()
    {
        hand = new List<Card>();
    }

    public void AddCard(Card card)
    {
        hand.Add(card);
    }

    public Card GetCard(int index)
    {
        if (index < 0 || index > 3) return null;
        return hand[index];
    }
}
