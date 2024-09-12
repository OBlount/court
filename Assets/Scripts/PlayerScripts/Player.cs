using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gm;
    public DeckManager dm;
    public Hand hand;

    private bool isMyTurn = false;
    private PlayerMovement playerMovement;
    private List<GameObject> cardUis;

    void Awake()
    {
        hand = new Hand();
        playerMovement = GetComponent<PlayerMovement>();
        cardUis = new List<GameObject>();
        foreach (Transform child in transform.Find("==UI==").Find("HUD"))
        {
            if (child.CompareTag("Card"))
            {
                cardUis.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (isMyTurn)
        {
            playerMovement.SetLookingMode(false);
            foreach (GameObject card in cardUis)
            {
                card.SetActive(true);
            }
        }
        else
        {
            playerMovement.SetLookingMode(true);
            foreach (GameObject card in cardUis)
            {
                card.SetActive(false);
            }
        }
    }

    public string AskCardCount()
    {
        if (dm != null) return dm.GetCardCount();
        return "???";
    }

    public string PeekChest()
    {
        if (dm != null) return dm.GetChestCardCount();
        return "???";
    }

    public string AskTurnNumber()
    {
        if (gm != null) return gm.GetTurnNumber();
        return "???";
    }

    public void SetTurnState(bool state)
    {
        isMyTurn = state;
    }

    public void PlayCard(int cardIndex)
    {
        Card card = hand.GetCard(cardIndex);
        switch (card.GetCardType())
        {
            case CardType.Kill:
                // TODO
                break;
            case CardType.PlusOne:
            case CardType.MinusTwo:
            case CardType.Zero:
                dm.AddToChest(card);
                break;
        }

        hand.RemoveCard(cardIndex);
        dm.DealCard(hand);
        gm.NextTurn();
    }
}
