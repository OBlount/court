using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnNumber : MonoBehaviour
{
    void Update()
    {
        GetComponent<TMP_Text>().text = $"Turn: {transform.parent.parent.parent.GetComponent<Player>().AskTurnNumber()}";
    }
}
