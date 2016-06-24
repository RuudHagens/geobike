using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PreSceneManager : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public GameObject locations;
    public float maxTime;

    private Dijkstra dijkstra;
    private LocationInfo firstLocation;
    private LocationInfo secondLocation;

    private float elapsedTime;

	// Use this for initialization
	void Start ()
    {
        elapsedTime = 0.0f;

        dijkstra = new Dijkstra();

        DetermineStartAndEnd();

        StaticObjects.startPoint = firstLocation.fullName;
        StaticObjects.endPoint = secondLocation.fullName;
        StaticObjects.dijkstraInstance = dijkstra;

        GUImanager.instance.setAssignmentText();

        player1.GetComponent<PreSceneScript>().done = false;
        player2.GetComponent<PreSceneScript>().done = false;
    }
	
	// Update is called once per frame
	void Update ()
    { 
        if (player1.GetComponent<PreSceneScript>().done && player2.GetComponent<PreSceneScript>().done)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= maxTime)
            {
                SceneManager.LoadScene("main scene");
            }
        }
    }

    private void DetermineStartAndEnd()
    {
        int numberOfLocations = locations.transform.childCount;
        List<LocationInfo> locationNames = new List<LocationInfo>();
        foreach (Transform location in locations.GetComponentInChildren<Transform>())
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
}
