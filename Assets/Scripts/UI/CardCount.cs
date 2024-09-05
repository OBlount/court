using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardCount : MonoBehaviour
{
    public GameObject dm;
    void Update()
    {
        GetComponent<TMP_Text>().text = dm.GetComponent<DeckManager>().GetCardCount();
    }
}
