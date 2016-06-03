using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

    // Method to load the main scene
    public void LoadMainScene()
    {
        SceneManager.LoadScene("pre scene");
    }

    // Method to exit the game
    public void ExitGame()
    {
        Application.Quit();
    }
}
