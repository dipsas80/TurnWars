using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public GameObject menu;
    public Slider sensSlider;
    private bool menuActive;


    void Awake()
    {
        //sets up playerPref if first time playing
        if(!PlayerPrefs.HasKey("sensitivity")) PlayerPrefs.SetFloat("sensitivity", 200f);


        //sets sensitivity across game sessions
        sensSlider.value = PlayerPrefs.GetFloat("sensitivity");
    }
    void Update()
    {
        //open pause menu
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menuActive = !menuActive;
        }

        //pause game
        if(menuActive == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else
        {
            Scene scene = SceneManager.GetActiveScene();
            //dont lock mouse in menu
            if(scene.name != "Menu")
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            Time.timeScale = 1;
        }

        //activate or disable pasue menu
        menu.SetActive(menuActive);

        
        
        

    }
    public void OnSensChange()
    {
        //set sensitivity
        PlayerPrefs.SetFloat("sensitivity", sensSlider.value);
    }
}
