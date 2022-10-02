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

    public bool isHitscan;
    public Rigidbody projectile;
    public GameObject scope;
    public float zoomLevel;
    public float range;
    private bool canScope;
    public Transform shootPos;
    public Transform shootPos2;

    private void Awake() 
    {
        
    }

    private void Update()
    {
        //check if paused first
        if(Time.timeScale == 1)
        {
            if(Input.GetMouseButtonDown(0) && noMoreShots == false)
            {
                Fire();
            }
            if(Input.GetMouseButton(1) && noMoreShots == false)
            {    
                cam.GetComponent<Camera>().fieldOfView = zoomLevel;
                scope.SetActive(true);
                if(weaponType == 0)
                {
                    this.GetComponent<MeshRenderer>().enabled = false;
                }
                
            }
            else
            {
                cam.GetComponent<Camera>().fieldOfView = 60f;
                scope.SetActive(false);
                if(weaponType == 0)
                {
                    this.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
        
    }
    public void Fire()
    {
        shotAmount++;
        if(this.GetComponent<Animator>() != null)
        {
            //this.GetComponent<Animator>().ResetTrigger("fire");
            this.GetComponent<Animator>().SetTrigger("fire");
            
        }
        RaycastHit hit;
        if(isHitscan == true)
        {
            
            if(Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, range))
            {
                

                if(hit.transform.tag == "Player" && hit.transform.GetComponent<MemberStats>().team != player.GetComponent<MemberStats>().team)
                {
                    hit.transform.GetComponent<MemberStats>().health -= damage;
                }
                hitEffect = Instantiate(Resources.Load("shoot", typeof(GameObject)), hit.point, Quaternion.Euler (90f, 0f, 0f)) as GameObject;
                Invoke("RemoveEffect", 0.2f);        
            }
        }
        else if(isHitscan == false && projectile != null)
        {
            var newProjectile = Instantiate(projectile, shootPos.position, cam.transform.rotation);
            //newProjectile.GetComponent<Rigidbody>().AddForce(cam.transform.forward * 100f, ForceMode.Impulse);
            newProjectile.gameObject.GetComponent<DestroyOnHit>().team = player.GetComponent<MemberStats>().team;
            newProjectile.gameObject.GetComponent<DestroyOnHit>().damage = damage;
            if(weaponType == 2)
            {
                var newProjectile2 = Instantiate(projectile, shootPos2.position, cam.transform.rotation);
                //newProjectile2.GetComponent<Rigidbody>().AddForce(cam.transform.forward * 100f, ForceMode.Impulse);
                newProjectile2.gameObject.GetComponent<DestroyOnHit>().team = player.GetComponent<MemberStats>().team;
                newProjectile2.gameObject.GetComponent<DestroyOnHit>().damage = damage;
            }
        }

        //launch backwards on sniper & bomber
        if(weaponType == 0 || weaponType == 1)
        {
            player.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * -12, ForceMode.Impulse);
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
