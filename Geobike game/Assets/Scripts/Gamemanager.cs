using UnityEngine;
using System.Collections;

public class Gamemanager : MonoBehaviour
{

    private float Timer = 0.0f;

    public Gamemanager()
    {

    }

    public void StartTimer()
    {
        Timer = 0.0f;
    }

    public void StopTimer()
    {
        string finaltime = Timer.ToString();

        Debug.Log(finaltime);
    }

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void FixedUpdate()
    {
        StartTimer();

        Timer += Time.deltaTime;
    }
}
