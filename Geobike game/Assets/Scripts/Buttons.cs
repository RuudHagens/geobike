using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

    public GameObject canvashelp;
    public GameObject canvas;
    public AudioClip click;

    public void Start()
    {
        if (StaticObjects.clickSound)
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(click, 1.0f);
            StaticObjects.clickSound = false;
        }
    }

    // Method to load the pre scene
    public void LoadPreScene()
    {
        SceneManager.LoadScene("pre scene");
    }

    // Method to load the main scene
    public void LoadMainScene()
    {
        SceneManager.LoadScene("main scene");
    }

    // Method to load the map scene
    public void LoadMapScene()
    {
        SceneManager.LoadScene("map scene");
    }

    public void Settings()
    {
        SceneManager.LoadScene("settings scene");
    }

    public void BackToStart()
    {
        StaticObjects.clickSound = true;
        SceneManager.LoadScene("start scene");
    }

    // Method to exit the game
    public void ExitGame()
    {
        Application.Quit();
    }

    // method to show the help screen
    public void ShowHelp()
    {
        canvashelp.SetActive(true);
        canvas.SetActive(false);
    }
    
    // method to hide the help screen
    public void HideHelp()
    {
        canvas.SetActive(true);
        canvashelp.SetActive(false);
    }

    // Method to play clicking sound when clicking on a button
    public void PlayClickSound()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(click, 1.0f);
    }
}
