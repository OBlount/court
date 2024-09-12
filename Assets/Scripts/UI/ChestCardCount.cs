using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChestCardCount : MonoBehaviour
{
    void Update()
    {
        GetComponent<TMP_Text>().text = transform.parent.parent.parent.parent.GetComponent<Player>().PeekChest();
    }
}
