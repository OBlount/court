using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public DeckManager dm;
    public Hand hand;

    private bool isMyTurn = false;

    void Awake()
    {
        hand = new Hand();
    }

    public string AskCardCount()
    {
        if (dm != null) return dm.GetCardCount();
        return "???";
    }

    public void SetTurnState(bool state)
    {
        isMyTurn = state;
    }
}
