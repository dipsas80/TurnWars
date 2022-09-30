using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public GameObject[] playerManagers;
    public GameObject[] players;
    public Transform[] spawnPoints;
    public int activePlayerIndex = 0;
    public Camera BreakCam;

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
        //find break cam
        BreakCam = GameObject.Find("BreakCamera").GetComponent<Camera>();
        
    }

    public void NextPlayerTurn()
    {
        
        activePlayerIndex++;
        activePlayerIndex = activePlayerIndex % playerManagers.Length;
        playerManagers[activePlayerIndex].GetComponent<PlayerManager>().mainCanvas.SetActive(true);
        playerManagers[activePlayerIndex].GetComponent<PlayerManager>().enabled = false;
        BreakCam.enabled = true;
        //Breaks inbetween player turns
        if(playerManagers[activePlayerIndex].GetComponent<PlayerManager>().teamDead == false)
        {
            Invoke("TakeBreak", 2f);
        }
        else
        {
            //skip break if team is eliminated
            playerManagers[activePlayerIndex].GetComponent<PlayerManager>().enabled = true;
            BreakCam.enabled = false;
        }
        
        
        
    }
    void TakeBreak()
    {
        playerManagers[activePlayerIndex].GetComponent<PlayerManager>().enabled = true;
        BreakCam.enabled = false;
    }
}
