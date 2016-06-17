using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUImanager : MonoBehaviour
{
    public static GUImanager instance;

    public Text assignmentText;

    // Use this for initialization
    void Start ()
    {
        instance = this;
        assignmentText.text = "";
    }
	
	// Update is called once per frame
	void Update ()
    {
        //GameSettings.enableCityNames = enableNames.isOn;
    }

    public void setAssignmentText(string firstLocation, string secondLocation)
    {
        assignmentText.text = "Ga van " + firstLocation + " naar " + secondLocation + ".";
    }
}
