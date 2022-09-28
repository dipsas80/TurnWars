using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decay : MonoBehaviour
{
    void Awake()
    {
        Invoke("Remove", 1f);
    }
    void Remove()
    {
        Destroy(this.gameObject);
    }
}
