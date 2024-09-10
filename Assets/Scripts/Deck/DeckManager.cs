using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    private List<Card> deck;

    void Awake()
    {
        deck = new List<Card>();
    }

    public void PrepareDeck()
    {
        for (int i = 0; i < 28; i++)
        {
            deck.Add(new Card(CardType.Kill));
        }

        for (int i = 0; i < 18; i++)
        {
            deck.Add(new Card(CardType.PlusOne));
        }

        for (int i = 0; i < 22; i++)
        {
            deck.Add(new Card(CardType.MinusTwo));
        }

        for (int i = 0; i < 20; i++)
        {
            deck.Add(new Card(CardType.Zero));
        }

        ShuffleDeck();
    }


    public void DealCard(Hand hand)
    {
        if (deck != null && deck.Count > 0)
        {
            Card cardToDeal = deck[0];
            deck.RemoveAt(0);
            hand.AddCard(cardToDeal);
        }
        else
        {
            Card noneCard = new Card(CardType.None);
            hand.AddCard(noneCard);
        }
    }

    public string GetCardCount()
    {
        return deck.Count.ToString();
    }

    private void ShuffleDeck()
    {
        System.Random rng = new System.Random();
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card value = deck[k];
            deck[k] = deck[n];
            deck[n] = value;
        }
    }
}
