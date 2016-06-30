using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour {

    public Toggle enableNames;
    public AudioClip click;

	// Use this for initialization
	void Start () {
	    if (StaticObjects.enableCityNames)
	    {
	        enableNames.isOn = true;
	    }
	    else
	    {
            enableNames.isOn = false;
        }

	    Camera.main.GetComponent<AudioSource>().PlayOneShot(click, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
        StaticObjects.enableCityNames = enableNames.isOn;
	}
}
