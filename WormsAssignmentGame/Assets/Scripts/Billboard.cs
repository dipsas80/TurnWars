using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Quaternion originalRotation;
    private Camera currentCam;
    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Camera[] allCameras = FindObjectsOfType<Camera>();
        for(int i = 0; i < allCameras.Length; i++)
        {
            if(allCameras[i].enabled == true)
            {
                currentCam = allCameras[i];
            }
            
        }
        transform.rotation = currentCam.transform.rotation * originalRotation;
    }
}
