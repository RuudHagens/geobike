using UnityEngine;
using System.Collections;

public class PreSceneScript : MonoBehaviour
{
    public GameObject Locations;
    public Material lineColor;
    public Dijkstra dijkstra;

    private GameObject selectorSprite;
    private GameObject startNode;
    private GameObject endNode;

    public Color c1 = Color.black;
    public Color c2 = Color.black;

    private void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineColor;
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.05f, 0.05f);
        lineRenderer.SetVertexCount(0);
        lineRenderer.sortingLayerName = "Player";

        setUpDijkstra();
    }

    private void Update()
    {
        setupBeginAndEnd();

        drawFastestRoute();
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
                Debug.Log("hit!");
                if (hitCollider.CompareTag("Node"))
                {
                    Debug.Log("hit!");
                    if (startNode == null)
                    {
                        startNode = getNode(hitCollider);
                    }
                    else if (endNode == null)
                    {
                        endNode = getNode(hitCollider);
                        if (startNode == endNode)
                        {
                            endNode = null;
                        }
                    }
                    else
                    {
                        Debug.Log("Je hebt al een start- en eindpunt geselecteerd.");
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

    private GameObject getNode(Collider2D hitCollider)
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

        return hitCollider.gameObject;
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
        graphAslist.Add(new GraphNode("haa", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("alk", 42.5f));
        graphAslist.Add(new GraphNode("ams", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("alk", 74.6f));
        graphAslist.Add(new GraphNode("lel", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("alk", 95.6f));
        graphAslist.Add(new GraphNode("sne", new ArrayList(vertexForList)));
        vertexForList.Clear();

        dijkstra = new Dijkstra();

        dijkstra.SetGraph(graphAslist);

        /*ArrayList path = dijkstra.GetPath("alk", "sne");

        Debug.Log(path.Count);
        for (int i = 0; i < path.Count; i++)
        {
            Debug.Log(path[i]);
        }*/
    }

    private void drawFastestRoute()
    {
        if (startNode != null && endNode != null)
        {
            string startNodeId = startNode.GetComponent<LocationInfo>().id;
            string endNodeId = endNode.GetComponent<LocationInfo>().id;
            ArrayList fastestRoute = new ArrayList();
            fastestRoute.Add("gro");
            fastestRoute.Add("ass");
            fastestRoute.Add("hoo");
            fastestRoute.Add("zwo");
            fastestRoute.Add("ape");
            fastestRoute.Add("arn");
            fastestRoute.Add("nij");
            fastestRoute.Add("ven");
            fastestRoute.Add("roe");
            fastestRoute.Add("maa");
            //ArrayList fastestRoute = dijkstra.GetPath(startNodeId, endNodeId); //call algorithm for startNodeId and endNodeId

            Debug.Log(fastestRoute);

            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.SetVertexCount(fastestRoute.Count);
            int newNodeOnLine = 0;

            for (int i = 0; i < fastestRoute.Count; i++)
            {
                foreach (Transform location in Locations.GetComponentInChildren<Transform>())
                {
                    if(location.gameObject.GetComponent<LocationInfo>().id == (string)fastestRoute[i])
                    {
                        Debug.Log(location.gameObject.GetComponent<LocationInfo>().id);
                        Debug.Log(location.position);
                        lineRenderer.SetPosition(i, location.position);
                        newNodeOnLine++;
                    }
                }
            }

            Debug.Log("done");
        }
    }
}