using UnityEngine;
using UnityEngine.UI;

public class GUIManagerMainScene : MonoBehaviour {

    public static GUIManagerMainScene instance;

    public Text assignmentText;

    // Use this for initialization
    void Awake()
    {
        instance = this;
        assignmentText.text = "Ga naar <color=#00ff00>" + StaticObjects.endPoint + "</color>";
    }
}
