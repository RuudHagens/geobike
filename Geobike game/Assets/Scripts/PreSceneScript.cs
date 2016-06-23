﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PreSceneScript : MonoBehaviour
{
    public GameObject LocationsLeft;
    public GameObject LocationsRight;
    public Material lineColor;
    public Dijkstra dijkstra;
    public float maxTime;

    private GameObject selectorSprite;

    private GameObject startNodeLeft;

    private GameObject startNodeRight;

    private bool onceLeft;
    private bool onceRight;

    private LocationInfo firstLocation;
    private LocationInfo secondLocation;

    private float elapsedTime;

    private void Start()
    {
        elapsedTime = 0.0f;

        onceLeft = false;
        onceRight = false;

        dijkstra = new Dijkstra();

        DetermineStartAndEnd();

        StaticObjects.startPoint = firstLocation.fullName;
        StaticObjects.endPoint = secondLocation.fullName;
        StaticObjects.dijkstraInstance = dijkstra;

        GUImanager.instance.setAssignmentText();
    }

    private void Update()
    {
        SetupBegin();

        if (startNodeLeft != null && !onceLeft)
        {
            onceLeft = true;
            //drawFastestRoute(startNodeLeft, endNodeLeft, lineRendererLeft, LocationsLeft);
        }

        if (startNodeRight != null && !onceRight)
        {
            onceRight = true;
            //drawFastestRoute(startNodeRight, endNodeRight, lineRendererRight, LocationsRight);
        }

        if (startNodeLeft != null && startNodeRight != null)
        {
            Debug.Log(elapsedTime);
            elapsedTime += Time.deltaTime;

            if(elapsedTime >= maxTime)
            {
                SceneManager.LoadScene("main scene");
            }
        }

    }

    private void DetermineStartAndEnd()
    {
        int numberOfLocations = LocationsLeft.transform.childCount;
        List<LocationInfo> locationNames = new List<LocationInfo>();
        foreach (Transform location in LocationsLeft.GetComponentInChildren<Transform>())
        {
            locationNames.Add(location.gameObject.GetComponent<LocationInfo>());
        }
        firstLocation = locationNames[Random.Range(0, numberOfLocations)];
        secondLocation = null;

        List<string> path = new List<string>();

        while (secondLocation == null)
        {
            secondLocation = locationNames[Random.Range(0, numberOfLocations)];

            path = dijkstra.GetPath(firstLocation.id, secondLocation.id);

            if (path.Count < 4)
            {
                secondLocation = null;
            }
        }
    }

    private void SetupBegin()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            Debug.Log("mouse pos " + mousePosition.x + " y " + mousePosition.y + " ");

            if (hitCollider)
            {
                if (hitCollider.CompareTag("Node"))
                {
                    Debug.Log("hit!");

                    if (hitCollider.GetComponent<LocationInfo>().map == 1)
                    {
                        StoreNode(hitCollider, ref startNodeLeft);
                    }
                    else if (hitCollider.GetComponent<LocationInfo>().map == 2)
                    {
                        StoreNode(hitCollider, ref startNodeRight);
                    }
                }
                else
                {
                    Debug.Log("Geen node gevonden. Hit " + hitCollider.transform.name + " x" + hitCollider.transform.position.x + " y " +
                          hitCollider.transform.position.y);
                }
            }
        }
    }

    private void StoreNode(Collider2D hit, ref GameObject startNode)
    {
        if (startNode == null)
        {
            if(hit.gameObject.GetComponent<LocationInfo>().fullName == firstLocation.fullName)
            {
                DrawSelection(hit);
                startNode = hit.gameObject;
            }
        }
        else
        {
            Debug.Log("Je hebt al een startpunt geselecteerd.");
        }
    }

    private void DrawSelection(Collider2D hitCollider)
    {
        selectorSprite = Instantiate(Resources.Load("Selector"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        if (selectorSprite != null)
        {
            Vector3 position = selectorSprite.transform.position;
            position.x = hitCollider.transform.position.x;
            position.y = hitCollider.transform.position.y;
            selectorSprite.transform.position = position;

            Debug.Log("Node gevonden! Hit " + hitCollider.transform.name + " x" + hitCollider.transform.position.x + " y " +
                  hitCollider.transform.position.y + " GameObject:" + hitCollider);
        }
    }
}