using UnityEngine;
using System.Collections;

public class HoverLine : MonoBehaviour {

    public GameObject fastestLineRenderer;
    public GameObject player1LineRenderer;
    public GameObject player2LineRenderer;

    public void EnableFastest()
    {
        fastestLineRenderer.GetComponent<LineRenderer>().enabled = true;
        player1LineRenderer.GetComponent<LineRenderer>().enabled = false;
        player2LineRenderer.GetComponent<LineRenderer>().enabled = false;
    }

    public void EnablePlayer1()
    {
        fastestLineRenderer.GetComponent<LineRenderer>().enabled = false;
        player1LineRenderer.GetComponent<LineRenderer>().enabled = true;
        player2LineRenderer.GetComponent<LineRenderer>().enabled = false;
    }

    public void EnablePlayer2()
    {
        fastestLineRenderer.GetComponent<LineRenderer>().enabled = false;
        player1LineRenderer.GetComponent<LineRenderer>().enabled = false;
        player2LineRenderer.GetComponent<LineRenderer>().enabled = true;
    }

    public void ShowAll()
    {
        fastestLineRenderer.GetComponent<LineRenderer>().enabled = true;
        player1LineRenderer.GetComponent<LineRenderer>().enabled = true;
        player2LineRenderer.GetComponent<LineRenderer>().enabled = true;
    }
}
