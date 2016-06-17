﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{

    private float timer = 0.0f;
    public Text timep1;
    public Text timep2;

    string minutes;
    string seconds;

    public Gamemanager()
    {

    }

    public void StartTimer()
    {
        timer = 300.0f;
        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = Mathf.Floor(timer % 60).ToString("00");
    }

    public void StopTimer()
    {
        string finaltime = timer.ToString();

        Debug.Log(finaltime);
    }

	// Use this for initialization
	void Start ()
    {
        StartTimer();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timep1.text = minutes + ":" + seconds;
            timep2.text = minutes + ":" + seconds;
        }
    }
}
