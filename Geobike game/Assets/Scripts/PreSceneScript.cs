using UnityEngine;
using System.Collections;

public class PreSceneScript : MonoBehaviour
{
    public GameObject Locations;
    public Material lineColor;

    private GameObject selectorSprite;
    private GameObject startNode;
    private GameObject endNode;

    bool routeDrawn;

    public Color c1 = Color.black;
    public Color c2 = Color.black;
    public int lengthOfLineRenderer = 2;

    private void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineColor;
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.05f, 0.05f);
        lineRenderer.SetVertexCount(lengthOfLineRenderer);
        lineRenderer.sortingLayerName = "Player";

        routeDrawn = false;
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
                if (hitCollider.CompareTag("Node"))
                {
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

    private void drawFastestRoute()
    {
        if (startNode != null && endNode != null)
        {
            string startNodeId = startNode.name;
            string endNodeId = endNode.name;
            ArrayList fastestRoute = new ArrayList(); //call algorithm for startNodeId and endNodeId
            fastestRoute.Add("ams");
            fastestRoute.Add("lel");

            LineRenderer lineRenderer = GetComponent<LineRenderer>();
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