using UnityEngine;
using System.Collections;

public class EndSceneScript : MonoBehaviour
{

    private Dijkstra dijkstra;

	// Use this for initialization
	void Start () {
	    dijkstra = new Dijkstra();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /*private void drawFastestRoute(GameObject startNode, GameObject endNode, LineRenderer lineRenderer, GameObject locations)
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
    }*/
}
