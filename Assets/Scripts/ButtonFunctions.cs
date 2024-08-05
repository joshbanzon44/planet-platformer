using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonFunctions : MonoBehaviour
{
    //Function for play button
    public void Play()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
