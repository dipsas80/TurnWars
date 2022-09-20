using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int weaponType;
    public Transform cam;
    private GameObject particleExplosion;
    public GameObject player;
    private int shotAmount;
    public bool noMoreShots;
    public int maxShots;

    private void Awake() 
    {
        shotAmount = 0;
        noMoreShots = false;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }

    }
    public void Fire()
    {
        shotAmount++;
        if(shotAmount >= maxShots)
        {
            noMoreShots = true;
        }
        RaycastHit hit;
        if (noMoreShots == false && Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            print(hit.transform.name);
            if(hit.transform.tag == "Player" && hit.transform.GetComponent<MemberStats>().team != player.GetComponent<MemberStats>().team)
            {
                hit.transform.GetComponent<MemberStats>().health -= 10;
            }
            
            particleExplosion = Instantiate(Resources.Load("shoot", typeof(GameObject)), hit.point, Quaternion.Euler (90f, 0f, 0f)) as GameObject;
            Invoke("RemoveEffect", 0.2f);        
        }
    }
    private void RemoveEffect()
    {
        Destroy(particleExplosion);
    }
}
