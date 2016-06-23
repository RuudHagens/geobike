using UnityEngine;
using System.Collections.Generic;

public class EndSceneScript : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public LineRenderer lineRendererPlayer1;
    public LineRenderer lineRendererPlayer2;
    public GameObject locations;
    public GameObject map;

    private Dijkstra dijkstra;
    private GameObject startPoint;
    private GameObject endPoint;

    // Use this for initialization
    void Start () {
	    dijkstra = new Dijkstra();

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

        DrawFastestRoute(startPoint, endPoint, lineRenderer, locations);
        DrawPlayerRoute(StaticObjects.visitedLocationsPlayer1, lineRendererPlayer1, locations);
        DrawPlayerRoute(StaticObjects.visitedLocationsPlayer2, lineRendererPlayer2, locations);
    }

    private void DrawFastestRoute(GameObject startNode, GameObject endNode, LineRenderer lineRenderer, GameObject locations)
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
}
