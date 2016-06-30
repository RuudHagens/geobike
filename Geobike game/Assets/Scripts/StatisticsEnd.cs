using UnityEngine;
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
        listOfFacts.Add("Voetbalvelden", 120f);
        listOfFacts.Add("Domtorens", 112f);
        listOfFacts.Add("Euromasten", 185f);
        listOfFacts.Add("Bananen", 0.20f);
        listOfFacts.Add("AfsluitDijken", 32500f);
        listOfFacts.Add("Spaghettislierten", 0.26f);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void calculateFact(float distance)
    {
        string randomFactName = listOfFacts.Keys.ToArray()[(int)Random.Range(0, listOfFacts.Keys.Count - 1)];
        float randomFactDistance = listOfFacts[randomFactName];
        float amount = Mathf.Round(distance * 1000 / randomFactDistance);
        lblFeit.text = "Je hebt " + amount + " " + randomFactName + " gefietst!";
    }

    public void totalDistance(List<string> visitedLocations)
    {
        float distance = StaticObjects.dijkstraInstance.GetPathLength(visitedLocations);
        lblDistance.text = "Afstand: " + distance + " KM";
        calculateFact(distance);
    }

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

        lblTijd.text = "Tijd to doel: " + minutestaken.ToString("00") + ":" + secondtaken.ToString("00"); 
    }
}
