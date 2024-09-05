using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public DeckManager dm;

    private List<Card> hand;

    void Start()
    {
        hand = new List<Card>();
        dm.DealCard(hand);
        dm.DealCard(hand);
        dm.DealCard(hand);
    }

    public Card GetCard(int index)
    {
        if (index < 0 || index > 3) return null;
        return hand[index];
    }
}
