using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PreSceneScript : MonoBehaviour
{
    public GameObject LocationsLeft;
    public GameObject LocationsRight;
    public Material lineColor;
    public Dijkstra dijkstra;

    private GameObject selectorSprite;

    private GameObject startNodeLeft;
    private GameObject endNodeLeft;

    private GameObject startNodeRight;
    private GameObject endNodeRight;

    public Color c1 = Color.black;
    public Color c2 = Color.black;

    private LineRenderer lineRendererLeft;
    private LineRenderer lineRendererRight;

    private bool onceLeft = false;
    private bool onceRight = false;

    private LocationInfo firstLocation;
    private LocationInfo secondLocation;

    private void Start()
    {
        onceLeft = false;
        onceRight = false;

        lineRendererLeft = LocationsLeft.GetComponent<LineRenderer>();
        lineRendererLeft.SetColors(c1, c2);
        lineRendererLeft.SetWidth(0.05f, 0.05f);
        lineRendererLeft.sortingLayerName = "Player";

        lineRendererRight = LocationsRight.GetComponent<LineRenderer>();
        lineRendererRight.SetColors(c1, c2);
        lineRendererRight.SetWidth(0.05f, 0.05f);
        lineRendererRight.sortingLayerName = "Player";
        
        dijkstra = Dijkstra.Instance;

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

        GUImanager.instance.setAssignmentText(firstLocation.fullName, secondLocation.fullName);
    }

    private void Update()
    {
        setupBeginAndEnd();

        if (startNodeLeft != null && endNodeLeft != null && !onceLeft)
        {
            onceLeft = true;
            drawFastestRoute(startNodeLeft, endNodeLeft, lineRendererLeft, LocationsLeft);
        }

        if (startNodeRight != null && endNodeRight != null && !onceRight)
        {
            onceRight = true;
            drawFastestRoute(startNodeRight, endNodeRight, lineRendererRight, LocationsRight);
        }

    }

    private void setupBeginAndEnd()
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
                        StoreNode(hitCollider, ref startNodeLeft, ref endNodeLeft);
                    }
                    else if (hitCollider.GetComponent<LocationInfo>().map == 2)
                    {
                        StoreNode(hitCollider, ref startNodeRight, ref endNodeRight);
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

    private void StoreNode(Collider2D hit, ref GameObject startNode, ref GameObject endNode)
    {
        if (startNode == null)
        {
            if(hit.gameObject.GetComponent<LocationInfo>().fullName == firstLocation.fullName)
            {
                drawSelection(hit);
                startNode = hit.gameObject;
            }
        }
        else if (endNode == null)
        {
            if (hit.gameObject.GetComponent<LocationInfo>().fullName == secondLocation.fullName)
            {
                drawSelection(hit);
                endNode = hit.gameObject;
            }
        }
        else
        {
            Debug.Log("Je hebt al een start- en eindpunt geselecteerd.");
        }
    }

    private void drawSelection(Collider2D hitCollider)
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

    private void setUpDijkstra()
    {
        
    }

    private void drawFastestRoute(GameObject startNode, GameObject endNode, LineRenderer lineRenderer, GameObject locations)
    {
        string startNodeId = startNode.GetComponent<LocationInfo>().id;
        string endNodeId = endNode.GetComponent<LocationInfo>().id;
        List<string> fastestRoute = dijkstra.GetPath(startNodeId, endNodeId); //call algorithm for startNodeId and endNodeId

        lineRenderer.SetVertexCount(fastestRoute.Count);
        int newNodeOnLine = 0;

        for (int i = 0; i < fastestRoute.Count; i++)
        {
            foreach (Transform location in locations.GetComponentInChildren<Transform>())
            {
                if (location.gameObject.GetComponent<LocationInfo>().id == fastestRoute[i])
                {
                    lineRenderer.SetPosition(i, location.position);
                    newNodeOnLine++;
                }
            }
        }
    }
}