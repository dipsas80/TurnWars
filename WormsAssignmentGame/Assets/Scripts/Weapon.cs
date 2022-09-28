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
    //public Transform shootPos;

    private void Awake() 
    {
        
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && noMoreShots == false)
        {
            Fire();
        }
        if(Input.GetMouseButton(1) && noMoreShots == false)
        {
            
            cam.GetComponent<Camera>().fieldOfView = zoomLevel;
            scope.SetActive(true);
        }
        else
        {
            
            cam.GetComponent<Camera>().fieldOfView = 60f;
            scope.SetActive(false);
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
            var newProjectile = Instantiate(projectile, cam.transform.position, cam.transform.rotation);
            newProjectile.velocity = transform.TransformDirection(new Vector3(0, -50f, 0));
            newProjectile.gameObject.GetComponent<DestroyOnHit>().team = player.GetComponent<MemberStats>().team;
            newProjectile.gameObject.GetComponent<DestroyOnHit>().damage = damage;
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
