using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] memberTypes;
    public string member1;
    public string member2;
    public string member3;

    public GameObject[] members;
    
    public int id;

    public int activeChar = 0;

    public void SpawnPlayers()
    {
        if(member1 != null && member2 != null && member3 != null)
        {
            members[0] = Instantiate(Resources.Load(member1, typeof(GameObject))) as GameObject;
            members[1] = Instantiate(Resources.Load(member2, typeof(GameObject))) as GameObject;
            members[2] = Instantiate(Resources.Load(member3, typeof(GameObject))) as GameObject;
        }
        
    }
    void Update()
    {
        if(members[activeChar].GetComponent<MemberStats>().movementUsed >= 100 || members[activeChar].GetComponent<MemberStats>().health <= 0)
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

            members[activeChar].GetComponent<MemberStats>().movementUsed = 0;
        }
        



        if(GameObject.Find("TurnManager") != null && GameObject.Find("TurnManager").GetComponent<TurnManager>().activePlayerIndex == id)
        {
            members[activeChar].GetComponentInChildren<PlayerLook>().enabled = true;
            members[activeChar].GetComponentInChildren<Camera>().enabled = true;
            
            for(int i = 0; i < members.Length; i++)
            {
                if(i != activeChar)
                {
                    members[i].GetComponentInChildren<PlayerLook>().enabled = false;
                    members[i].GetComponentInChildren<Camera>().enabled = false;
                }
            }

            
        }
        else if(GameObject.Find("TurnManager") != null && GameObject.Find("TurnManager").GetComponent<TurnManager>().activePlayerIndex != id)
        {
            for(int i = 0; i < members.Length; i++)
            {
                
                members[i].GetComponentInChildren<PlayerLook>().enabled = false;
                members[i].GetComponentInChildren<Camera>().enabled = false;
                
            }
        }
        
        
    }


}
