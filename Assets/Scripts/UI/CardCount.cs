using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardCount : MonoBehaviour
{
    void Update()
    {
        GetComponent<TMP_Text>().text = transform.parent.parent.parent.parent.GetComponent<Player>().AskCardCount();
    }
}
