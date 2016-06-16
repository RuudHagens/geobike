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

        setUpDijkstra();

        int numberOfLocations = LocationsLeft.transform.childCount;
        List<LocationInfo> locationNames = new List<LocationInfo>();
        foreach (Transform location in LocationsLeft.GetComponentInChildren<Transform>())
        {
            locationNames.Add(location.gameObject.GetComponent<LocationInfo>());
        }
        firstLocation = locationNames[Random.Range(0, numberOfLocations)];
        secondLocation = null;

        ArrayList path = null;

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
        //DE lijst
        ArrayList graphAslist = new ArrayList();

        //DE lijst binnen de lijst
        ArrayList vertexForList = new ArrayList();

        vertexForList.Add(new Vertex("haa", 35.7f));
        vertexForList.Add(new Vertex("ams", 42.5f));
        vertexForList.Add(new Vertex("lel", 74.6f));
        vertexForList.Add(new Vertex("sne", 95.6f));
        graphAslist.Add(new GraphNode("alk", new ArrayList(vertexForList)));
        vertexForList.Clear();


        vertexForList.Add(new Vertex("alk", 35.7f));
        vertexForList.Add(new Vertex("ams", 19.5f));
        vertexForList.Add(new Vertex("lei", 33.7f));
        graphAslist.Add(new GraphNode("haa", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("alk", 42.5f));
        vertexForList.Add(new Vertex("haa", 19.5f));
        vertexForList.Add(new Vertex("alm", 33.5f));
        vertexForList.Add(new Vertex("utr", 43.5f));
        vertexForList.Add(new Vertex("lei", 46.9f));
        graphAslist.Add(new GraphNode("ams", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("haa", 33.7f));
        vertexForList.Add(new Vertex("ams", 46.9f));
        vertexForList.Add(new Vertex("utr", 58.8f));
        vertexForList.Add(new Vertex("dha", 24.1f));
        graphAslist.Add(new GraphNode("lei", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("lei", 24.1f));
        vertexForList.Add(new Vertex("utr", 68.5f));
        vertexForList.Add(new Vertex("del", 11.6f));
        graphAslist.Add(new GraphNode("dha", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("dha", 11.6f));
        vertexForList.Add(new Vertex("rot", 15.6f));
        graphAslist.Add(new GraphNode("del", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("del", 15.6f));
        vertexForList.Add(new Vertex("utr", 61.8f));
        vertexForList.Add(new Vertex("dor", 25.5f));
        vertexForList.Add(new Vertex("zie", 66.2f));
        graphAslist.Add(new GraphNode("rot", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ams", 43.5f));
        vertexForList.Add(new Vertex("lei", 58.8f));
        vertexForList.Add(new Vertex("dha", 68.5f));
        vertexForList.Add(new Vertex("rot", 61.8f));
        vertexForList.Add(new Vertex("dor", 65.8f));
        vertexForList.Add(new Vertex("bos", 56.9f));
        vertexForList.Add(new Vertex("nij", 84.6f));
        vertexForList.Add(new Vertex("ame", 23.5f));
        vertexForList.Add(new Vertex("alm", 41.3f));
        graphAslist.Add(new GraphNode("utr", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("rot", 25.5f));
        vertexForList.Add(new Vertex("utr", 65.8f));
        vertexForList.Add(new Vertex("bos", 62.8f));
        vertexForList.Add(new Vertex("bre", 35.6f));
        vertexForList.Add(new Vertex("roo", 44.7f));
        graphAslist.Add(new GraphNode("dor", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("rot", 66.2f));
        vertexForList.Add(new Vertex("roo", 59.4f));
        vertexForList.Add(new Vertex("mid", 42.3f));
        vertexForList.Add(new Vertex("goe", 22.9f));
        graphAslist.Add(new GraphNode("zie", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("zie", 42.3f));
        vertexForList.Add(new Vertex("goe", 25.8f));
        vertexForList.Add(new Vertex("ter", 32.8f));
        graphAslist.Add(new GraphNode("mid", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("mid", 32.8f));
        vertexForList.Add(new Vertex("goe", 35.4f));
        graphAslist.Add(new GraphNode("ter", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ter", 35.4f));
        vertexForList.Add(new Vertex("mid", 25.8f));
        vertexForList.Add(new Vertex("zie", 22.9f));
        graphAslist.Add(new GraphNode("goe", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("zie", 59.4f));
        vertexForList.Add(new Vertex("dor", 44.7f));
        vertexForList.Add(new Vertex("bre", 24.6f));
        graphAslist.Add(new GraphNode("roo", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("roo", 24.6f));
        vertexForList.Add(new Vertex("dor", 35.6f));
        vertexForList.Add(new Vertex("til", 28.0f));
        graphAslist.Add(new GraphNode("bre", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("bre", 28.0f));
        vertexForList.Add(new Vertex("bos", 24.1f));
        vertexForList.Add(new Vertex("ein", 34.7f));
        graphAslist.Add(new GraphNode("til", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("til", 34.7f));
        vertexForList.Add(new Vertex("bos", 32.8f));
        vertexForList.Add(new Vertex("nij", 61.3f));
        vertexForList.Add(new Vertex("ven", 58.6f));
        vertexForList.Add(new Vertex("roe", 51.2f));
        graphAslist.Add(new GraphNode("ein", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ein", 58.6f));
        vertexForList.Add(new Vertex("nij", 62.4f));
        vertexForList.Add(new Vertex("roe", 29.3f));
        graphAslist.Add(new GraphNode("ven", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ven", 29.3f));
        vertexForList.Add(new Vertex("ein", 31.2f));
        vertexForList.Add(new Vertex("maa", 48.3f));
        vertexForList.Add(new Vertex("hrl", 40.9f));
        graphAslist.Add(new GraphNode("roe", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("roe", 48.3f));
        vertexForList.Add(new Vertex("hrl", 24.9f));
        graphAslist.Add(new GraphNode("maa", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("maa", 24.9f));
        vertexForList.Add(new Vertex("roe", 40.9f));
        graphAslist.Add(new GraphNode("hrl", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("til", 24.1f));
        vertexForList.Add(new Vertex("ein", 32.8f));
        vertexForList.Add(new Vertex("nij", 48.3f));
        vertexForList.Add(new Vertex("utr", 56.9f));
        vertexForList.Add(new Vertex("dor", 62.8f));
        graphAslist.Add(new GraphNode("bos", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ven", 62.4f));
        vertexForList.Add(new Vertex("ein", 61.3f));
        vertexForList.Add(new Vertex("bos", 48.3f));
        vertexForList.Add(new Vertex("utr", 84.6f));
        vertexForList.Add(new Vertex("arn", 23.1f));
        graphAslist.Add(new GraphNode("nij", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("nij", 23.1f));
        vertexForList.Add(new Vertex("ame", 51.8f));
        vertexForList.Add(new Vertex("ens", 95.7f));
        vertexForList.Add(new Vertex("ape", 31.7f));
        graphAslist.Add(new GraphNode("arn", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("arn", 51.8f));
        vertexForList.Add(new Vertex("utr", 23.4f));
        vertexForList.Add(new Vertex("alm", 41.5f));
        graphAslist.Add(new GraphNode("ame", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ame", 41.5f));
        vertexForList.Add(new Vertex("utr", 41.3f));
        vertexForList.Add(new Vertex("ams", 33.5f));
        vertexForList.Add(new Vertex("lel", 30.5f));
        vertexForList.Add(new Vertex("ape", 74.1f));
        graphAslist.Add(new GraphNode("alm", new ArrayList(vertexForList)));
        vertexForList.Clear();





        vertexForList.Add(new Vertex("alm", 30.5f));
        vertexForList.Add(new Vertex("alk", 74.6f));
        vertexForList.Add(new Vertex("zwo", 49.6f));
        graphAslist.Add(new GraphNode("lel", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("arn", 31.7f));
        vertexForList.Add(new Vertex("alm", 74.1f));
        vertexForList.Add(new Vertex("zwo", 39.0f));
        vertexForList.Add(new Vertex("alo", 60.3f));
        graphAslist.Add(new GraphNode("ape", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("arn", 95.7f));
        vertexForList.Add(new Vertex("alo", 29.4f));
        vertexForList.Add(new Vertex("hoo", 74.3f));
        graphAslist.Add(new GraphNode("ens", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ens", 29.4f));
        vertexForList.Add(new Vertex("ape", 60.3f));
        vertexForList.Add(new Vertex("zwo", 52.3f));
        vertexForList.Add(new Vertex("hoo", 51.6f));
        graphAslist.Add(new GraphNode("alo", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("lel", 49.6f));
        vertexForList.Add(new Vertex("ape", 39.0f));
        vertexForList.Add(new Vertex("alo", 52.3f));
        vertexForList.Add(new Vertex("hoo", 53.5f));
        vertexForList.Add(new Vertex("hee", 62.9f));
        graphAslist.Add(new GraphNode("zwo", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ens", 74.3f));
        vertexForList.Add(new Vertex("alo", 51.6f));
        vertexForList.Add(new Vertex("zwo", 43.5f));
        vertexForList.Add(new Vertex("hee", 63.5f));
        vertexForList.Add(new Vertex("ass", 34.3f));
        vertexForList.Add(new Vertex("emm", 33.0f));
        graphAslist.Add(new GraphNode("hoo", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("hoo", 33.0f));
        vertexForList.Add(new Vertex("ass", 38.9f));
        graphAslist.Add(new GraphNode("emm", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("emm", 38.9f));
        vertexForList.Add(new Vertex("hoo", 34.3f));
        vertexForList.Add(new Vertex("gro", 32.2f));
        graphAslist.Add(new GraphNode("ass", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ass", 32.2f));
        vertexForList.Add(new Vertex("hee", 60.0f));
        vertexForList.Add(new Vertex("lee", 60.0f));
        graphAslist.Add(new GraphNode("gro", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("zwo", 62.9f));
        vertexForList.Add(new Vertex("hoo", 63.5f));
        vertexForList.Add(new Vertex("gro", 60.0f));
        vertexForList.Add(new Vertex("lee", 31.2f));
        vertexForList.Add(new Vertex("sne", 25.8f));
        graphAslist.Add(new GraphNode("hee", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("hee", 25.8f));
        vertexForList.Add(new Vertex("lee", 22.0f));
        vertexForList.Add(new Vertex("alk", 95.6f));
        graphAslist.Add(new GraphNode("sne", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("sne", 22.0f));
        vertexForList.Add(new Vertex("hee", 31.2f));
        vertexForList.Add(new Vertex("gro", 60.0f));
        graphAslist.Add(new GraphNode("lee", new ArrayList(vertexForList)));
        vertexForList.Clear();

        dijkstra = new Dijkstra();

        dijkstra.SetGraph(graphAslist);
    }

    private void drawFastestRoute(GameObject startNode, GameObject endNode, LineRenderer lineRenderer, GameObject locations)
    {
        string startNodeId = startNode.GetComponent<LocationInfo>().id;
        string endNodeId = endNode.GetComponent<LocationInfo>().id;
        ArrayList fastestRoute = dijkstra.GetPath(startNodeId, endNodeId); //call algorithm for startNodeId and endNodeId

        lineRenderer.SetVertexCount(fastestRoute.Count);
        int newNodeOnLine = 0;

        for (int i = 0; i < fastestRoute.Count; i++)
        {
            foreach (Transform location in locations.GetComponentInChildren<Transform>())
            {
                if (location.gameObject.GetComponent<LocationInfo>().id == (string)fastestRoute[i])
                {
                    lineRenderer.SetPosition(i, location.position);
                    newNodeOnLine++;
                }
            }
        }
    }
}