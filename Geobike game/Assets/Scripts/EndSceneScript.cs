﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSceneScript : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public LineRenderer lineRendererPlayer1;
    public LineRenderer lineRendererPlayer2;
    public GameObject locations;
    public GameObject map;
    public Text winningPlayer;
    public GameObject pnStats1;
    public GameObject pnStats2;
    public AudioClip victory;

    private Dijkstra dijkstra;
    private StatisticsEnd statisticsEnd1;
    private StatisticsEnd statisticsEnd2;
    private GameObject startPoint;
    private GameObject endPoint;

    // Use this for initialization
    void Start () {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(victory, 0.7f);
        dijkstra = StaticObjects.dijkstraInstance;
        statisticsEnd1 = pnStats1.GetComponent<StatisticsEnd>();
        statisticsEnd2 = pnStats2.GetComponent<StatisticsEnd>();

        //get the start and endpoint of the game
        foreach (Transform location in locations.GetComponentInChildren<Transform>())
        {
            if (location.gameObject.GetComponent<LocationInfo>().fullName == StaticObjects.startPoint)
            {
                startPoint = location.gameObject;
            }

            if (location.gameObject.GetComponent<LocationInfo>().fullName == StaticObjects.endPoint)
            {
                endPoint = location.gameObject;
            }
        }

        DrawShortestRoute(startPoint, endPoint, lineRenderer, locations);
        DrawPlayerRoute(StaticObjects.visitedLocationsPlayer1, lineRendererPlayer1, locations);
        DrawPlayerRoute(StaticObjects.visitedLocationsPlayer2, lineRendererPlayer2, locations);

        statisticsEnd1.totalDistance(StaticObjects.visitedLocationsPlayer1);
        statisticsEnd2.totalDistance(StaticObjects.visitedLocationsPlayer2);
        statisticsEnd1.totalTime();
        statisticsEnd2.totalTime();

        winningPlayer.text = "Speler " + StaticObjects.winningPlayer + " heeft gewonnen!";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            StaticObjects.visitedLocationsPlayer1.Clear();
            StaticObjects.visitedLocationsPlayer2.Clear();
            StaticObjects.startPoint = null;
            StaticObjects.endPoint = null;
            StaticObjects.dijkstraInstance = null;
            SceneManager.LoadScene("start scene");
        }
    }
    /// <summary>
    /// Draws the shortest route that the players could have taken.
    /// </summary>
    /// <param name="startNode">the start position of the players</param>
    /// <param name="endNode">the end position of the players</param>
    /// <param name="lineRenderer">the render that draws the line</param>
    /// <param name="locations">the locations inbetween the route</param>
    private void DrawShortestRoute(GameObject startNode, GameObject endNode, LineRenderer lineRenderer, GameObject locations)
    {
        string startNodeId = startNode.GetComponent<LocationInfo>().id;
        string endNodeId = endNode.GetComponent<LocationInfo>().id;
        List<string> shortestRoute = dijkstra.GetPath(startNodeId, endNodeId); //call algorithm for startNodeId and endNodeId

        lineRenderer.SetVertexCount(shortestRoute.Count);
        int newNodeOnLine = 0;

        for (int i = 0; i < shortestRoute.Count; i++)
        {
            foreach (Transform location in locations.GetComponentInChildren<Transform>())
            {
                if (location.gameObject.GetComponent<LocationInfo>().id == shortestRoute[i])
                {
                    lineRenderer.SetPosition(i, location.position);
                    newNodeOnLine++;
                }
            }
        }
    }

    /// <summary>
    /// Draws the line of the route the player has cycled
    /// </summary>
    /// <param name="visitedLocations"></param>
    /// <param name="lineRenderer"></param>
    /// <param name="locations"></param>
    private void DrawPlayerRoute(List<string> visitedLocations, LineRenderer lineRenderer, GameObject locations)
    {
        lineRenderer.SetVertexCount(visitedLocations.Count);
        int newNodeOnLine = 0;

        for (int i = 0; i < visitedLocations.Count; i++)
        {
            foreach (Transform location in locations.GetComponentInChildren<Transform>())
            {
                if (location.gameObject.GetComponent<LocationInfo>().id == visitedLocations[i])
                {
                    lineRenderer.SetPosition(i, location.position);
                    newNodeOnLine++;
                }
            }
        }
    }

    /// <summary>
    /// Method to reset everything and start a new game
    /// </summary>
    public void NewGame()
    {
        StaticObjects.visitedLocationsPlayer1.Clear();
        StaticObjects.visitedLocationsPlayer2.Clear();
        StaticObjects.startPoint = null;
        StaticObjects.endPoint = null;
        StaticObjects.dijkstraInstance = null;
        SceneManager.LoadScene("start scene");
        StaticObjects.clickSound = true;
    }
}
