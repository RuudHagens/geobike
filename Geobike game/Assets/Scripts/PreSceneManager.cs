using UnityEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PreSceneManager : MonoBehaviour
{
    /// <summary>
    /// The player1 game object.
    /// </summary>
    public GameObject Player1;

    /// <summary>
    /// The player2 game object.
    /// </summary>
    public GameObject Player2;

    /// <summary>
    /// The provinces game object, holding the province game objects.
    /// </summary>
    public GameObject Provinces;
    
    /// <summary>
    /// The locations game object, holding the location game objects.
    /// </summary>
    public GameObject Locations;

    /// <summary>
    /// The max time as a float.
    /// </summary>
    public float MaxTime;

    /// <summary>
    /// Dijkstra object.
    /// </summary>
    private Dijkstra _dijkstra;

    /// <summary>
    /// The first location (start node).
    /// </summary>
    private LocationInfo _firstLocation;

    /// <summary>
    /// The second location (end node).
    /// </summary>
    private LocationInfo _secondLocation;

    /// <summary>
    /// The province info of the province the first location is located in.
    /// </summary>
    private ProvinceInfo _firstLocationProvince;

    /// <summary>
    /// The total elapsed time.
    /// </summary>
    private float _elapsedTime;
    
    /// <summary>
    /// Method which is called when initializing the class.
    /// </summary>
	void Start ()
    {
        // Assign the elapsed time to maxTime.
        this._elapsedTime = this.MaxTime;

        // Initialize a new dijkstra object.
        this._dijkstra = new Dijkstra();

        // Create random start and end nodes.
        this.DetermineStartAndEnd();

        // Assign these start and end nodes to the static variables in the StaticObjects class,
        // so they can be used in other scripts.
        StaticObjects.startPoint = this._firstLocation.fullName;
        StaticObjects.endPoint = this._secondLocation.fullName;
        StaticObjects.startProvince = this._firstLocationProvince.fullName;
        StaticObjects.dijkstraInstance = this._dijkstra;

        // Set the assignment text (handled by the GUImanager script).
        GUImanager.instance.SetAssignmentText();

        // Reset the HasSelectedLocation boolean.
        Player1.GetComponent<PreSceneScript>().HasSelectedLocation = false;
        Player2.GetComponent<PreSceneScript>().HasSelectedLocation = false;

        // Remove the text of the countdown text object.
        GUImanager.instance.CountdownText.text = string.Empty;
    }
	
    /// <summary>
    /// Method which is being called once per frame
    /// </summary>
	void Update ()
    { 
        // Check if both players have selected a location.
        if (this.Player1.GetComponent<PreSceneScript>().HasSelectedLocation && this.Player2.GetComponent<PreSceneScript>().HasSelectedLocation)
        {
            // Set the text to the ceiled elapsed time.
            GUImanager.instance.CountdownText.text = Convert.ToString(Mathf.Ceil(this._elapsedTime), CultureInfo.CurrentCulture);

            // Decrease the elapsed time.
            this._elapsedTime -= Time.deltaTime;

            // Check if the elapsed time has reached 0.
            if (_elapsedTime <= 0)
            {
                // Set the elapsed time to 0 to prevent displaying a negative number.
                this._elapsedTime = 0;

                // Load the main scene to start the game.
                SceneManager.LoadScene("main scene");
            }
        }
    }

    /// <summary>
    /// Method to get a random path by setting a start and end node.
    /// </summary>
    private void DetermineStartAndEnd()
    {
        // Get the amount of locations.
        int numberOfLocations = Locations.transform.childCount;

        // Create a new list of location names.
        List<LocationInfo> locationNames = new List<LocationInfo>();

        // Fill the list with the location info of every location.
        foreach (Transform location in this.Locations.GetComponentInChildren<Transform>())
        {
            locationNames.Add(location.gameObject.GetComponent<LocationInfo>());
        }
        
        // Set the first location to a random location.
        this._firstLocation = locationNames[Random.Range(0, numberOfLocations)];

        // Set the province info of the province the first location is located in.
        foreach (Transform province in this.Provinces.GetComponentInChildren<Transform>())
        {
            // Save the provinceInfo of the province.
            ProvinceInfo foundProvinceInfo = province.gameObject.GetComponent<ProvinceInfo>();

            // Check if the provinceInfo id matched the firstlocation province id.
            if (foundProvinceInfo.id == this._firstLocation.provinceId)
            {
                // If it matches, assign the firstLocationProvince to the found provinceInfo
                this._firstLocationProvince = foundProvinceInfo;

                // Break out of the loop as the province info has been found.
                break;
            }
        }

        // Set the second location to null.
        this._secondLocation = null;

        // Create a list of strings to hold the path.
        List<string> path = new List<string>();

        // Keep looping if the second location is not null.
        while (this._secondLocation == null)
        {
            // Set the second location to a random integer.
            this._secondLocation = locationNames[Random.Range(0, numberOfLocations)];

            // Get the path of the first location to the second location.
            path = this._dijkstra.GetPath(this._firstLocation.id, this._secondLocation.id);

            // Check if the path is long enough.
            if (path.Count < 4)
            {
                // The path is not long enough, so set the second location to null.
                this._secondLocation = null;

                // Remove the second location as this location is not valid.
                locationNames.Remove(this._secondLocation);

                // Reassign the number of locations.
                numberOfLocations = this.Locations.transform.childCount;
            }
        }
    }
}
