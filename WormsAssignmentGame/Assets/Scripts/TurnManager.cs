using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public GameObject[] playerManagers;
    public int activePlayerIndex = 0;

    void Awake(){
        playerManagers = GameObject.FindGameObjectsWithTag("pm");
        for(int i = 0; i < playerManagers.Length; i++)
        {
            playerManagers[i].GetComponent<PlayerManager>().SpawnPlayers();
        }
    }

    public void NextPlayerTurn()
    {
        activePlayerIndex++;
        activePlayerIndex = activePlayerIndex % playerManagers.Length;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            NextPlayerTurn();
        }
    }
}
