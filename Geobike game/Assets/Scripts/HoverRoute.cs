using UnityEngine;
using System.Collections;

public class HoverRoute : MonoBehaviour
{

    public GameObject fastestRoute;
    public GameObject player1Route;
    public GameObject player2Route;

    public GameObject fastestLineRenderer;
    public GameObject player1LineRenderer;
    public GameObject player2LineRenderer;

    private Ray ray;
    private RaycastHit hit;
	
	// Update is called once per frame
	void Update () {
        Debug.Log("wtf brock");
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Lines")))
        {
            Debug.Log("wtf misty");
            if (hit.collider == fastestRoute.GetComponent<Collider2D>())
            {
                Debug.Log("wtf ash 1");
                fastestLineRenderer.GetComponent<LineRenderer>().enabled = true;
                player1LineRenderer.GetComponent<LineRenderer>().enabled = false;
                player1LineRenderer.GetComponent<LineRenderer>().enabled = false;
            }
            else if (hit.collider == fastestRoute.GetComponent<Collider2D>())
            {
                Debug.Log("wtf ash 2");
                fastestLineRenderer.GetComponent<LineRenderer>().enabled = false;
                player1LineRenderer.GetComponent<LineRenderer>().enabled = true;
                player1LineRenderer.GetComponent<LineRenderer>().enabled = false;
            }
            else if(hit.collider == fastestRoute.GetComponent<Collider2D>())
            {
                Debug.Log("wtf ash 3");
                fastestLineRenderer.GetComponent<LineRenderer>().enabled = false;
                player1LineRenderer.GetComponent<LineRenderer>().enabled = false;
                player1LineRenderer.GetComponent<LineRenderer>().enabled = true;
            }
            else
            {
                fastestLineRenderer.GetComponent<LineRenderer>().enabled = true;
                player1LineRenderer.GetComponent<LineRenderer>().enabled = true;
                player1LineRenderer.GetComponent<LineRenderer>().enabled = true;
            }
        }
        else
        {
            fastestLineRenderer.GetComponent<LineRenderer>().enabled = true;
            player1LineRenderer.GetComponent<LineRenderer>().enabled = true;
            player1LineRenderer.GetComponent<LineRenderer>().enabled = true;
        }
    }
}
