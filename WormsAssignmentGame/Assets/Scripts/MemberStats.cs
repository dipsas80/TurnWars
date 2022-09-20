using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberStats : MonoBehaviour
{
    public int health = 100;
    public float movementUsed = 0;
    public GameObject uiElement;
    public Slider Mslider;
    public Slider HPslider;
    private GameObject particleExplosion;
    private bool dead;

    void Update()
    {
        Mslider.value = movementUsed;
        HPslider.value = health;

        if(health <= 0)
        {
            if(dead == false)
            {
                this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
                particleExplosion = Instantiate(Resources.Load("explode", typeof(GameObject)), this.transform.position, Quaternion.Euler (90f, 0f, 0f)) as GameObject;
                Invoke("RemoveEffect", 1.0f);
                dead = true;
            }
            
        }
    }

    private void RemoveEffect()
    {
        Destroy(particleExplosion);
    }
}
