using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public bool teamDead = false;


    public GameObject[] memberTypes;
    public string member1;
    public string member2;
    public string member3;

    public GameObject[] members;
    
    public int id;

    public int activeChar = 0;

    public GameObject mainCanvas;
    public TextMeshProUGUI playerIDUI;

    public void SpawnPlayers()
    {
        //change canvas UI
        
        playerIDUI.text = ("Player " + (id + 1));
        if(id == 0)
        {
            playerIDUI.color = new Color (255, 0, 0);
            
        }
        else if(id == 1)
        {
            playerIDUI.color = new Color (255, 239, 0);
            
        }
        else if(id == 2)
        {
            playerIDUI.color = new Color (0, 253, 0);
            
        }
        else
        {
            playerIDUI.color = new Color (0, 0, 255);
            
        }
        //spawn players

        if(member1 != null && member2 != null && member3 != null)
        {
            members[0] = Instantiate(Resources.Load(member1, typeof(GameObject))) as GameObject;
            members[1] = Instantiate(Resources.Load(member2, typeof(GameObject))) as GameObject;
            members[2] = Instantiate(Resources.Load(member3, typeof(GameObject))) as GameObject;

            members[0].GetComponent<MemberStats>().team = id;
            members[1].GetComponent<MemberStats>().team = id;
            members[2].GetComponent<MemberStats>().team = id;
        }
        
    }
    void Update()
    {
        //Ui on/off
        if(GameObject.Find("TurnManager") != null && GameObject.Find("TurnManager").GetComponent<TurnManager>().activePlayerIndex == id)
        {
            mainCanvas.SetActive(true);
        }
        else
        {
            mainCanvas.SetActive(false);
        }


        if(GameObject.Find("TurnManager") != null && GameObject.Find("TurnManager").GetComponent<TurnManager>().activePlayerIndex == id)
        {
            if(members[activeChar].GetComponent<MemberStats>().movementUsed >= 100 && members[activeChar].GetComponent<MemberStats>().doneShooting == true || members[activeChar].GetComponent<MemberStats>().health <= 0)
            {
                
                if(activeChar != 2)
                {
                    activeChar++;
                }
                else
                {
                    activeChar = 0;
                    GameObject.Find("TurnManager").GetComponent<TurnManager>().NextPlayerTurn();
                }
                members[activeChar].GetComponent<MemberStats>().weapon.GetComponent<Weapon>().shotAmount = 0;
                members[activeChar].GetComponent<MemberStats>().weapon.GetComponent<Weapon>().noMoreShots = false;
                members[activeChar].GetComponent<MemberStats>().movementUsed = 0;
            }
        }
        
        



        if(GameObject.Find("TurnManager") != null && GameObject.Find("TurnManager").GetComponent<TurnManager>().activePlayerIndex == id)
        {
            
            members[activeChar].GetComponent<MemberStats>().weapon.SetActive(true);
            members[activeChar].GetComponent<MemberStats>().uiElement.SetActive(true);
            members[activeChar].GetComponentInChildren<PlayerLook>().enabled = true;
            members[activeChar].GetComponentInChildren<Camera>().enabled = true;
            members[activeChar].GetComponentInChildren<AudioListener>().enabled = true;
            if(members[activeChar].GetComponent<MemberStats>().health > 0)
            {
                members[activeChar].GetComponent<MemberStats>().publicHPslider.SetActive(false);
                members[activeChar].GetComponent<MeshRenderer>().enabled = false;
            }
            
            
            for(int i = 0; i < members.Length; i++)
            {
                if(i != activeChar)
                {
                    
                    members[i].GetComponent<MemberStats>().weapon.SetActive(false);
                    members[i].GetComponent<MemberStats>().uiElement.SetActive(false);
                    members[i].GetComponentInChildren<PlayerLook>().enabled = false;
                    members[i].GetComponentInChildren<Camera>().enabled = false;
                    members[i].GetComponentInChildren<AudioListener>().enabled = false;
                    if(members[i].GetComponent<MemberStats>().health > 0)
                    {
                        members[i].GetComponent<MemberStats>().publicHPslider.SetActive(true);
                        members[i].GetComponent<MeshRenderer>().enabled = true;
                    }
                }
            }

            
        }
        else if(GameObject.Find("TurnManager") != null && GameObject.Find("TurnManager").GetComponent<TurnManager>().activePlayerIndex != id)
        {
            for(int i = 0; i < members.Length; i++)
            {
                
                members[i].GetComponent<MemberStats>().weapon.SetActive(false);
                members[i].GetComponent<MemberStats>().uiElement.SetActive(false);
                members[i].GetComponentInChildren<AudioListener>().enabled = false;
                members[i].GetComponentInChildren<PlayerLook>().enabled = false;
                members[i].GetComponentInChildren<Camera>().enabled = false;
                if(members[i].GetComponent<MemberStats>().health > 0)
                {
                    members[i].GetComponent<MemberStats>().publicHPslider.SetActive(true);
                    members[i].GetComponent<MeshRenderer>().enabled = true;
                }
                

                
            }
        }

        //check if player is eliminated
        if(members[0].GetComponent<MemberStats>().health <= 0 && members[1].GetComponent<MemberStats>().health <= 0 && members[2].GetComponent<MemberStats>().health <= 0)
        {
            teamDead = true;
        }
        
    }


}
