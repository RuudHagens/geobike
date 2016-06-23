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
        DontDestroyOnLoad(transform.gameObject);
        instance = this;
        assignmentText.text = "";
    }
	
	// Update is called once per frame
	void Update ()
    {
        //GameSettings.enableCityNames = enableNames.isOn;
    }

    public void setAssignmentText()
    {
        assignmentText.text = "Ga van " + StaticObjects.startPoint + " naar " + StaticObjects.endPoint + ".\n" + "Selecteer " + StaticObjects.startPoint + " om te beginnen.";
    }

    public void setTargetText()
    {
        assignmentText.text = "Ga naar: " + StaticObjects.endPoint + ".";
    }
}
