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
    public GameObject publicHPslider;
    public Image playerPublicHPcolor;
    public TextMeshProUGUI actions;
    private GameObject particleExplosion;
    private bool dead;
    public GameObject weapon;
    public int team;
    public bool doneShooting;
    public GameObject model;
    public GameObject body;
    public GameObject turnOverSymbol;
    
    void Start()
    {
        if(team == 0)
        {
            playerPublicHPcolor.color = new Color (255, 0, 0);
            
        }
        else if(team == 1)
        {
            playerPublicHPcolor.color = new Color (255, 239, 0);
            
        }
        else if(team == 2)
        {
            playerPublicHPcolor.color = new Color (0, 253, 0);
            
        }
        else
        {
            playerPublicHPcolor.color = new Color (0, 0, 255);
            
        }
    }
    void Update()
    {
        Mslider.value = movementUsed;
        shots = (weapon.GetComponent<Weapon>().maxShots - weapon.GetComponent<Weapon>().shotAmount);
        HPslider.value = health;
        publicHPslider.GetComponent<Slider>().value = health;
        doneShooting = weapon.GetComponent<Weapon>().noMoreShots;
        actions.text = ("actions: " + shots);

        if(health <= 0)
        {
            if(dead == false)
            {
                model.SetActive(false);
                body.SetActive(false);
                weapon.SetActive(false);
                publicHPslider.SetActive(false);
                this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
                particleExplosion = Instantiate(Resources.Load("explode", typeof(GameObject)), this.transform.position, Quaternion.Euler (90f, 0f, 0f)) as GameObject;
                Invoke("RemoveEffect", 1.0f);
                dead = true;
            }
            
        }

        //adds delay between character swaps
        if(movementUsed == 99 && doneShooting == true)
        {
            
            Invoke("TurnUpSymbol", 0.5f);
            Invoke("NextPlayer", 2f);
        }
    }

    private void RemoveEffect()
    {
        Destroy(particleExplosion);
    }
    private void TurnUpSymbol()
    {
        turnOverSymbol.SetActive(true);
    }
    private void NextPlayer()
    {
        turnOverSymbol.SetActive(false);
        movementUsed = 100;
    }
}
