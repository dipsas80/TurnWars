using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    public int team;
    public int damage;

    private void OnCollisionEnter(Collision other)
    {
        

        if(other.transform.tag == "Player" && team != other.transform.GetComponent<MemberStats>().team)
        {
            other.transform.GetComponent<MemberStats>().health -= damage;
        }
        Destroy(this.gameObject);
    }
}
