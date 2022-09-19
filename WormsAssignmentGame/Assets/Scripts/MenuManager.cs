using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject playerCountSelector;
    public GameObject playerCustomization;
    public GameObject[] teamSelector;
    public GameObject levelSelector;
    public GameObject playerManager;

    private int playerAmount;
    private GameObject activePlayer;
    private int playerCycle;
    private string memberType1;
    private string memberType2;
    private string memberType3;
    
    //start menu
    private void Start()
    {
        playerCountSelector.SetActive(false);
        playerCustomization.SetActive(false);
        levelSelector.SetActive(false);
    }

    public void Play()
    {
        playerCountSelector.SetActive(true);
    }
    //player count and customization
    public void Players(int playerCount)
    {

        playerAmount = playerCount;
        for(int i = 0; i < 4; i++)
        {
            teamSelector[i].SetActive(false);
        }
        playerCountSelector.SetActive(false);
        playerCustomization.SetActive(true);

        //start of playerCycle
        activePlayer = Instantiate(playerManager, Vector3.zero, teamSelector[playerCycle].transform.rotation);
        activePlayer.GetComponent<PlayerManager>().id = 0;
        DontDestroyOnLoad(activePlayer);
        teamSelector[playerCycle].SetActive(true);
    }
    public void Choice1(string type)
    {
        activePlayer.GetComponent<PlayerManager>().member1 = type;
    }
    public void Choice2(string type)
    {
        activePlayer.GetComponent<PlayerManager>().member2 = type;
    }
    public void Choice3(string type)
    {
        activePlayer.GetComponent<PlayerManager>().member3 = type;
    }
    public void Done()
    {
        if(playerCycle != playerAmount - 1)
        {
            playerCycle++;
            for(int i = 0; i < 4; i++)
            {
                teamSelector[i].SetActive(false);
            }
            teamSelector[playerCycle].SetActive(true);
            activePlayer = Instantiate(playerManager, Vector3.zero, teamSelector[playerCycle].transform.rotation);
            activePlayer.GetComponent<PlayerManager>().id = playerCycle;
            DontDestroyOnLoad(activePlayer);
        }
        else
        {
            LevelSelect();
        }
        
    }


    //level select
    public void LevelSelect()
    {
        playerCustomization.SetActive(false);
        levelSelector.SetActive(true);
    }
    
    public void Level(string levelName)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }
}
