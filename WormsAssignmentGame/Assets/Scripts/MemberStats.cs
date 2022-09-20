using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemberStats : MonoBehaviour
{
    public int health = 100;
    public float movementUsed = 0;
    public int shots = 0;
    public GameObject uiElement;
    public Slider Mslider;
    public Slider HPslider;
    public TextMeshProUGUI actions;
    private GameObject particleExplosion;
    private bool dead;
    public GameObject weapon;
    public int team;
    public bool doneShooting;

    void Update()
    {
        Mslider.value = movementUsed;
        shots = (weapon.GetComponent<Weapon>().maxShots - weapon.GetComponent<Weapon>().shotAmount);
        HPslider.value = health;
        doneShooting = weapon.GetComponent<Weapon>().noMoreShots;
        actions.text = ("actions: " + shots);

        if(health <= 0)
        {
            if(dead == false)
            {
                weapon.SetActive(false);
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
