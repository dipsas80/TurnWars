using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public GameObject[] playerManagers;
    public GameObject[] players;
    public Transform[] spawnPoints;
    public int activePlayerIndex = 0;

    void Awake(){
        playerManagers = GameObject.FindGameObjectsWithTag("pm");
        for(int i = 0; i < playerManagers.Length; i++)
        {
            playerManagers[i].GetComponent<PlayerManager>().SpawnPlayers();
        }


        //spawn in
        players = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = spawnPoints[i].position;
        }
        
    }

    public void NextPlayerTurn()
    {
        activePlayerIndex++;
        activePlayerIndex = activePlayerIndex % playerManagers.Length;
    }
}
