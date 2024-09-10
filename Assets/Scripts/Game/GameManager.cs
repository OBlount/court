using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private DeckManager dm;
    private List<GameObject> players;
    private int turn = 0;

    void Start()
    {
        InitialiseGame();
    }

    private void NextTurn()
    {
        turn++;
    }

    private void InitialiseGame()
    {
        players = new List<GameObject>();
        InstantiateNewPlayer("King"); // Just place the king prefab down for now

        dm.PrepareDeck();
        foreach (GameObject playerPrefab in players)
        {
            Player player = playerPrefab.GetComponent<Player>();
            dm.DealCard(player.hand);
            dm.DealCard(player.hand);
            dm.DealCard(player.hand);
        }

        NextTurn();
    }

    private void InstantiateNewPlayer(string characterName)
    {
        GameObject newPlayer = Instantiate(Resources.Load<GameObject>($"Prefabs/Players/{characterName}"));
        newPlayer.GetComponent<Player>().dm = dm;
        newPlayer.transform.SetParent(GameObject.Find("==Characters==").transform);
        newPlayer.name = characterName;
        players.Add(newPlayer);
    }
}
