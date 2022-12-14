using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    public int team;
    public int damage;
    public bool isExplosive;

    private void OnCollisionEnter(Collision other)
    {
        Invoke("Decay", 0.5f);
        if(other.transform.tag == "Player" && team != other.transform.GetComponent<MemberStats>().team)
        {
            if(isExplosive == false)
            {
                other.transform.GetComponent<MemberStats>().health -= damage;
                Destroy(this.gameObject);
            }
            else
            {
                Instantiate(Resources.Load("Explosion", typeof(GameObject)), this.transform.position, Quaternion.Euler (90f, 0f, 0f));
                foreach(Collider collider in Physics.OverlapSphere(this.transform.position, 10f))
                {
                    if(collider.GetComponent<MemberStats>() != null)
                    {
                        collider.GetComponent<MemberStats>().health -= damage;
                    }
                }
                Destroy(this.gameObject);
            }
            
            
        }
        else if(isExplosive == true)
        {
            Instantiate(Resources.Load("Explosion", typeof(GameObject)), this.transform.position, Quaternion.Euler (90f, 0f, 0f));
            foreach(Collider collider in Physics.OverlapSphere(this.transform.position, 10f))
            {
                if(collider.GetComponent<MemberStats>() != null)
                {
                    collider.GetComponent<MemberStats>().health -= damage;
                }
            }
            Destroy(this.gameObject);
        }
        
    }
    void Update()
    {
        //move bullet
        transform.Translate(Vector3.forward * Time.deltaTime * 50f);
    }
    void Decay()
    {
        Destroy(this.gameObject);
    }
}
