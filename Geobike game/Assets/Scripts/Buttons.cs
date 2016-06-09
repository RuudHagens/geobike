using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

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

    // Method to exit the game
    public void ExitGame()
    {
        Application.Quit();
    }
}
