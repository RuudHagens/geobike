﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class StatisticsEnd : MonoBehaviour {

    public Text lblDistance;
    public Text lblTijd;
    public Text lblFeit;

    private Dictionary<string, float> listOfFacts;

    // Use this for initialization
    void Awake () {
        listOfFacts = new Dictionary<string, float>();
        listOfFacts.Add("voetbalvelden", 120f);
        listOfFacts.Add("Domtorens", 112f);
        listOfFacts.Add("Euromasten", 185f);
        listOfFacts.Add("bananen", 0.20f);
        listOfFacts.Add("Afsluitdijken", 32500f);
        listOfFacts.Add("spaghettislierten", 0.26f);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    /// <summary>
    /// Method to calculate a random fact for the distance that is cycled
    /// </summary>
    /// <param name="distance">the distance the player has cycled</param>
    public void calculateFact(float distance)
    {
        string randomFactName = listOfFacts.Keys.ToArray()[(int)Random.Range(0, listOfFacts.Keys.Count)];
        float randomFactDistance = listOfFacts[randomFactName];
        float amount = Mathf.Round(distance * 1000 / randomFactDistance);
        lblFeit.text = "Je hebt " + amount + " " + randomFactName + " gefietst!";
    }

    /// <summary>
    /// Method to calculate the distance the player has cycled
    /// </summary>
    /// <param name="visitedLocations"></param>
    public void totalDistance(List<string> visitedLocations)
    {
        float distance = StaticObjects.dijkstraInstance.GetPathLength(visitedLocations);
        lblDistance.text = "Afstand: " + distance + " km";
        calculateFact(distance);
    }

    /// <summary>
    /// Method to show how long the game took
    /// </summary>
    public void totalTime()
    {
        float minutestaken = 0;
        float secondtaken = 0;

        if(this.name.Contains("1"))
        {
            minutestaken = 4f - StaticObjects.minutesleftPlayer1;
            secondtaken = 60f - StaticObjects.secondsleftPlayer1;
        }
        else if (this.name.Contains("2"))
        {
            minutestaken = 4f - StaticObjects.minutesleftPlayer2;
            secondtaken = 60f - StaticObjects.secondsleftPlayer2;
        }

        lblTijd.text = "Tijd tot doel: " + minutestaken.ToString("00") + ":" + secondtaken.ToString("00");
    }
}
