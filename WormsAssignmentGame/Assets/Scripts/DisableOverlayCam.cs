using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOverlayCam : MonoBehaviour
{
    public Camera playerCam;
    public bool disableFOV;
    // Update is called once per frame
    void Update()
    {
        if(playerCam.enabled == true)
        {

            this.GetComponent<Camera>().enabled = true;
            if(disableFOV == false)
            {
                this.GetComponent<Camera>().fieldOfView = playerCam.fieldOfView;
            }
            
        }
        else
        {
            this.GetComponent<Camera>().enabled = false;
        }
    }
}
