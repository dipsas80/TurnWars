using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomIconOnStart : MonoBehaviour
{
    public Sprite[] faces;
    public Image faceHolder;

    void Awake()
    {
        faceHolder.sprite = faces[Random.Range(0, 3)];
    }
}
