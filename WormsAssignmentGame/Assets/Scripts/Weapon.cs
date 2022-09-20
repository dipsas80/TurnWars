using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int weaponType;
    public Transform cam;
    private GameObject hitEffect;
    public GameObject player;
    public int shotAmount;
    public bool noMoreShots;

    public int damage;
    public int maxShots;

    private void Awake() 
    {
        
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && noMoreShots == false)
        {
            Fire();
        }

    }
    public void Fire()
    {
        shotAmount++;
        
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            print(hit.transform.name);
            if(hit.transform.tag == "Player" && hit.transform.GetComponent<MemberStats>().team != player.GetComponent<MemberStats>().team)
            {
                hit.transform.GetComponent<MemberStats>().health -= damage;
            }
            
            hitEffect = Instantiate(Resources.Load("shoot", typeof(GameObject)), hit.point, Quaternion.Euler (90f, 0f, 0f)) as GameObject;
            Invoke("RemoveEffect", 0.2f);        
        }
        if(shotAmount >= maxShots)
        {
            noMoreShots = true;
        }
        else
        {
            noMoreShots = false;
        }
        
    }
    private void RemoveEffect()
    {
        Destroy(hitEffect);
    }
}
