using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUImanager : MonoBehaviour
{
    public static GUImanager instance;

    public Text QuestText;

    public Text AssignmenText;

    // Use this for initialization
    void Awake ()
    {
        instance = this;
        this.QuestText.text = this.AssignmenText.text = string.Empty;
    }

    public void setAssignmentText()
    {
        this.QuestText.text = "Ga van " + StaticObjects.startPoint + " naar " + StaticObjects.endPoint;
        this.AssignmenText.text = "Selecteer <color=#00ff00>" + StaticObjects.startPoint + "</color> om te beginnen";
    }
}
