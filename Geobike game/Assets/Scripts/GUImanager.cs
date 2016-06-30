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
    /// The countdown text.
    /// </summary>
    public Text CountdownText;

    /// <summary>
    /// A boolean indicating whether player 1 has selected the right province.
    /// </summary>
    private bool P1SelectedProvince;

    /// <summary>
    /// A boolean indicating whether player 2 has selected the right province.
    /// </summary>
    private bool P2SelectedProvince;

    /// <summary>
    /// Method is being called when this class is instantiated.
    /// </summary>
    void Awake ()
    {
        instance = this;
        this.QuestText.text = this.AssignmenText.text = string.Empty;
        this.P1SelectedProvince = false;
        this.P2SelectedProvince = false;
    }

    /// <summary>
    /// Method to set the assignment text.
    /// </summary>
    public void SetAssignmentText()
    {
        this.QuestText.text = "Ga van " + StaticObjects.startPoint + " naar " + StaticObjects.endPoint;
        this.AssignmenText.text = "Selecteer eerst de province waar <color=#00ff00>" + StaticObjects.startPoint + "</color> ligt om verder te gaan";
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

    /// <summary>
    /// Method to set a boolean for selected province per player.
    /// </summary>
    /// <param name="playerNr"></param>
    public void PlayerHasSelectedProvince(int playerNr)
    {
        // Set the boolean corresponding to the given player nr to true.
        switch (playerNr)
        {
            case 1:
                this.P1SelectedProvince = true;
                break;
            case 2:
                this.P2SelectedProvince = true;
                break;
        }

        // Change the assignment text when both players have selected the right province.
        if (this.P1SelectedProvince && this.P2SelectedProvince)
        {
            this.AssignmenText.text = "Selecteer <color=#00ff00>" + StaticObjects.startPoint + "</color> om te beginnen";
        }
    }
}
