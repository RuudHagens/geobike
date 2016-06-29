using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUImanager : MonoBehaviour
{
    /// <summary>
    /// The static instance of this class, so it can be used anywhere.
    /// </summary>
    public static GUImanager instance;

    /// <summary>
    /// The quest text object. Should only contain the quest.
    /// </summary>
    public Text QuestText;

    /// <summary>
    /// The assignment text object. Should only contain the assignment.
    /// </summary>
    public Text AssignmenText;

    /// <summary>
    /// The hint text object for player 1. Can contain any hint related text.
    /// </summary>
    public Text HintTextP1;

    /// <summary>
    /// The hint text object for player 2. Can contain any hint related text.
    /// </summary>
    public Text HintTextP2;

    /// <summary>
    /// Method is being called when this class is instantiated.
    /// </summary>
    void Awake ()
    {
        instance = this;
        this.QuestText.text = this.AssignmenText.text = string.Empty;
    }

    /// <summary>
    /// Method to set the assignment text.
    /// </summary>
    public void SetAssignmentText()
    {
        this.QuestText.text = "Ga van " + StaticObjects.startPoint + " naar " + StaticObjects.endPoint;
        this.AssignmenText.text = "Selecteer <color=#00ff00>" + StaticObjects.startPoint + "</color> om te beginnen";
    }

    /// <summary>
    /// Method to set the hint text object of one of the player.
    /// </summary>
    /// <param name="text">The hint text.</param>
    /// <param name="playerNr">The player number of the player.</param>
    public void SetHintText(string text, int playerNr)
    {
        switch (playerNr)
        {
            case 1:
                this.HintTextP1.text = text;
                break;
            case 2:
                this.HintTextP2.text = text;
                break;
        }
    }
}
