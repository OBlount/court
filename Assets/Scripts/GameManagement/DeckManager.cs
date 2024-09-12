using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    private List<Card> deck;
    private List<Card> chest;
    private List<Card> discard;

    void Awake()
    {
        deck = new List<Card>();
        chest = new List<Card>();
        discard = new List<Card>();
    }

    public void PrepareDeck()
    {
        deck.Clear();
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
            hand.AddCard(new Card(CardType.None));
        }
    }

    public void AddToChest(Card card)
    {
        List<CardType> validCardTypes = new List<CardType> { CardType.PlusOne, CardType.MinusTwo, CardType.Zero };
        if (validCardTypes.Contains(card.GetCardType()))
        {
            chest.Add(card);
        }
    }

    public int TotUpChestLoot()
    {
        int result = 0;
        foreach (Card card in chest)
        {
            CardType type = card.GetCardType();
            switch (type)
            {
                case CardType.PlusOne:
                    result++;
                    break;
                case CardType.MinusTwo:
                    result--;
                    result--;
                    break;
            }
        }
        return result;
    }

    public void DiscardCard(Card card)
    {
        if (card.GetCardType() != CardType.None)
        {
            discard.Add(card);
        }
    }

    public void EmptyDiscardDeck()
    {
        discard.Clear();
    }

    public void EmptyChest()
    {
        chest.Clear();
    }

    public string GetCardCount()
    {
        return deck.Count.ToString();
    }

    public string GetChestCardCount()
    {
        return chest.Count.ToString();
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
