using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    public GameObject[] playerManagers;
    public GameObject[] players;
    public Transform[] spawnPoints;
    public int activePlayerIndex = 0;
    public Camera BreakCam;
    public GameObject winScreen;
    public TextMeshProUGUI winText;
    private bool gameOver;

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

    void Update()
    {
        //check for winner
        int deadPlayers = 0;
        for(int i = 0; i < playerManagers.Length; i++)
        {
            if(playerManagers[i].GetComponent<PlayerManager>().teamDead == true)
            {
                deadPlayers++;
            }
        }
        if(deadPlayers == (playerManagers.Length - 1))
        {
            
            for(int i = 0; i < playerManagers.Length; i++)
            {
                if(playerManagers[i].GetComponent<PlayerManager>().teamDead == false)
                {
                    winScreen.SetActive(true);
                    if(i == 0)
                    {
                        winText.color = new Color (255, 0, 0);
                        
                    }
                    else if(i == 1)
                    {
                        winText.color = new Color (255, 239, 0);
                        
                    }
                    else if(i == 2)
                    {
                        winText.color = new Color (0, 253, 0);
                        
                    }
                    else
                    {
                        winText.color = new Color (0, 0, 255);
                        
                    }
                    winText.text = ("Player " + (i + 1) + " Victory");
                    gameOver = true;
                    //destroy player managers
                    
                    Invoke("BackToMenu", 4f);
                    
                }
                
            }
        }
        else
        {
            winScreen.SetActive(false);
        }

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
        if(gameOver == false)
        {
            playerManagers[activePlayerIndex].GetComponent<PlayerManager>().enabled = true;
            BreakCam.enabled = false;
        }
        
    }
    void BackToMenu()
    {
        for(int r = 0; r < playerManagers.Length; r++)
        {
            Destroy(playerManagers[r]);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
