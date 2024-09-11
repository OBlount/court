using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gm;
    public DeckManager dm;
    public Hand hand;

    private bool isMyTurn = false;
    private List<GameObject> cardUis;

    void Awake()
    {
        hand = new Hand();
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
            foreach (GameObject card in cardUis)
            {
                card.SetActive(true);
            }
        }
    }

    public string AskCardCount()
    {
        if (dm != null) return dm.GetCardCount();
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
        // Add logic for each card type
        hand.RemoveCard(cardIndex);
        dm.DealCard(hand);
        gm.NextTurn();
    }
}
