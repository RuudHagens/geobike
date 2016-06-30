using System;
using UnityEngine;
using System.Collections.Generic;

public class PreSceneScript : MonoBehaviour
{
    /// <summary>
    /// The number of the player matching this script.
    /// </summary>
    public int PlayerNumber;

    /// <summary>
    /// The locations game object, holding the various location objects.
    /// </summary>
    public GameObject Locations;

    /// <summary>
    /// The provinces game object, holding all province objects.
    /// </summary>
    public GameObject Provinces;

    /// <summary>
    /// The keycode used to cycle.
    /// </summary>
    public KeyCode Cycling;

    /// <summary>
    /// The keycode for the button on the back left side of the steeringwheel.
    /// </summary>
    public KeyCode ConfirmButton;

    /// <summary>
    /// The keycode for the button on the front right side of the steeringwheel.
    /// </summary>
    public KeyCode RightSteeringWheel;

    /// <summary>
    /// The keycode for the button on the front left side of the steeringwheel.
    /// </summary>
    public KeyCode LeftSteeringWheel;

    /// <summary>
    /// The keyboard keycode used to cycle through the locations.
    /// </summary>
    public KeyCode CyclingFromKeyboard;

    /// <summary>
    /// The keycode for the button on the back left side of the steeringwheel.
    /// </summary>
    public KeyCode ConfirmButtonKeyboard;

    /// <summary>
    /// The keycode for the button on the front right side of the steeringwheel simulated from the keyboard.
    /// </summary>
    public KeyCode RightSteeringWheelFromKeyboard;

    /// <summary>
    /// The keycode for the button on the front left side of the steeringwheel simulated from the keyboard.
    /// </summary>
    public KeyCode LeftSteeringWheelFromKeyboard;

    /// <summary>
    /// A boolean indicating whether the right province has been pressed.
    /// </summary>
    [HideInInspector]
    public bool HasSelectedProvince;

    /// <summary>
    /// A boolean indicating whether the right location has been pressed and the player is ready.
    /// </summary>
    [HideInInspector]
    public bool HasSelectedLocation;

    public AudioClip select;
    public AudioClip error;
    public AudioClip success;

    /// <summary>
    /// The startNode object.
    /// </summary>
    private GameObject _startNode;

    /// <summary>
    /// Used to store the index of the selected province.
    /// </summary>
    private int _loopProvinces;

    /// <summary>
    /// Used to store the index of the selected location.
    /// </summary>
    private int _loopLocations;

    /// <summary>
    /// The red nodeselector object.
    /// </summary>
    private GameObject _nodeSelectorBlue;

    /// <summary>
    /// The green nodeselector object.
    /// </summary>
    private GameObject _nodeSelectorGreen;

    /// <summary>
    /// The red nodeselector object.
    /// </summary>
    private GameObject _nodeSelectorRed;

    /// <summary>
    /// The selected province game object.
    /// </summary>
    private GameObject _selectedProvince;

    /// <summary>
    /// The selected location game object.
    /// </summary>
    private GameObject _selectedLocation;

    /// <summary>
    /// A list of player node objects.
    /// </summary>
    private List<GameObject> _playerNodes;

    /// <summary>
    /// A list of province game objects.
    /// </summary>
    private List<GameObject> _provinces;

    /// <summary>
    /// The entry point of this script.
    /// </summary>
    private void Start()
    {
        // Instantiate variables.
        this.HasSelectedLocation = false;
        this._startNode = null;
        this._loopLocations = 1;
        this._loopProvinces = 1;
        this._playerNodes = new List<GameObject>();
        this._provinces = new List<GameObject>();

        // Add every location in _playerNodes.
        foreach(Transform location in this.Locations.GetComponentInChildren<Transform>())
        {
            // Add the location game object to the list.
            this._playerNodes.Add(location.gameObject);
        }

        // Add every province in _provinces
        foreach (Transform province in this.Provinces.GetComponentInChildren<Transform>())
        {
            // Add the province game object to the list.
            this._provinces.Add(province.gameObject);

            // Set the first province in the list as the selected province.
            if (this._selectedProvince == null) this._selectedProvince = province.gameObject;
        }

        // Instantiate a blue selector.
        this._nodeSelectorBlue = Instantiate(Resources.Load("NodeSelector"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

        // Check if the blue node selector is not null before calling its position.
        if (this._nodeSelectorBlue != null)
        {
            // Set the position of the blue selector to the first location.
            this._nodeSelectorBlue.transform.position = this._playerNodes[0].transform.position;

            // Disable the blue selector game object as the locations shouldn't yet be selected.
            this._nodeSelectorBlue.SetActive(false);
        }

        // Highlight the selected province.
        this._selectedProvince.GetComponent<SpriteRenderer>().material.color = Color.blue;

        // Disable the locations gameobject (holding all location game objects).
        this.Locations.SetActive(false);
    }

    /// <summary>
    /// The update method is called as often as possible.
    /// </summary>
    private void Update()
    {
        // Call method for checking input.
        this.DetectPressedKeyOrButton();

        // Set the boolean 'HasSelectedLocation' to true only if
        // the startNode is not null (meaning a location has been selected).
        if (this._startNode != null)
        {
            this.HasSelectedLocation = true;
        }

        // Call the method to check for inputs to move the selector,
        // only if the player has not selected a location yet.
        if (!this.HasSelectedLocation)
        {
            this.MoveSelector();
        }
    }
    
    /// <summary>
    /// Method used to print a pressed key (used for debugging).
    /// </summary>
    public void DetectPressedKeyOrButton()
    {
        // Loop through all possible keycodes.
        foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            // Check if the keycode is currently pressed.
            if (Input.GetKeyDown(kcode))
            {
                // Log the pressed keycode.
                Debug.Log("KeyCode down: " + kcode);
            }
        }
    }

    /// <summary>
    /// Method to move the selector to the next or previous location node.
    /// </summary>
    public void MoveSelector()
    {
        // Check for right steering wheel input.
        if (Input.GetKeyDown(this.RightSteeringWheelFromKeyboard) || Input.GetKeyDown(this.RightSteeringWheel))
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(select, 1.0f);

            // Check if the player has selected a province.
            if (this.HasSelectedProvince)
            {
                // Cycle to the next location within the province.
                this._loopLocations++;
                if (this._loopLocations >= this._playerNodes.Count)
                {
                    this._loopLocations = 0;
                }

                this._selectedLocation = this._playerNodes[this._loopLocations].gameObject;

                this._nodeSelectorBlue.transform.position = this._playerNodes[this._loopLocations].transform.position;
            }
            else
            {
                // Cycle to the next province.
                this._loopProvinces++;
                if (this._loopProvinces >= this._provinces.Count)
                {
                    this._loopProvinces = 0;
                }

                this._selectedProvince = this._provinces[this._loopProvinces].gameObject;

                // Deselect all provinces.
                foreach (GameObject province in this._provinces)
                {
                    province.GetComponent<SpriteRenderer>().material.color = Color.white;
                }

                // Highlight the selected province.
                this._selectedProvince.GetComponent<SpriteRenderer>().material.color = Color.blue;
            }
        }

        // Check for left steering wheel input.
        if (Input.GetKeyDown(this.LeftSteeringWheelFromKeyboard) || Input.GetKeyDown(this.LeftSteeringWheel))
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(select, 1.0f);

            // Check if the player has selected a province
            if (this.HasSelectedProvince)
            {
                // Cycle to the previous location within the province
                this._loopLocations--;
                if (this._loopLocations < 0)
                {
                    this._loopLocations = this._playerNodes.Count - 1;
                }

                this._selectedLocation = this._playerNodes[this._loopLocations].gameObject;

                // Highlight the previous location.
                this._nodeSelectorBlue.transform.position = this._playerNodes[this._loopLocations].transform.position;

                Destroy(this._nodeSelectorRed);
                this._nodeSelectorRed = null;
            }
            else
            {
                // cycle to the previous province
                this._loopProvinces--;
                if (this._loopProvinces < 0)
                {
                    this._loopProvinces = this._provinces.Count - 1;
                }

                this._selectedProvince = this._provinces[this._loopProvinces].gameObject;

                // Deselect all provinces.
                foreach (GameObject province in this._provinces)
                {
                    province.GetComponent<SpriteRenderer>().material.color = Color.white;
                }

                // Highlight the selected province.
                this._selectedProvince.GetComponent<SpriteRenderer>().material.color = Color.blue;
            }
        }

        // Check for confirm input.
        if (Input.GetKeyDown(this.ConfirmButtonKeyboard) || Input.GetKeyDown(this.ConfirmButton))
        {
            Destroy(this._nodeSelectorRed);
            this._nodeSelectorRed = null;

            // Check if the player has selected a province
            if (this.HasSelectedProvince)
            {
                // Check if the location is the right location
                if (this._selectedLocation.GetComponent<LocationInfo>().fullName == StaticObjects.startPoint)
                {
                    // Highlight location green, disable further input
                    this.DrawSelection(this._nodeSelectorBlue, true);
                    this.HasSelectedLocation = true;
                    Destroy(this._nodeSelectorBlue);
                    this._nodeSelectorBlue = null;
                    Camera.main.GetComponent<AudioSource>().PlayOneShot(success, 0.7f);
                }
                else
                {
                    // Highlight location red, display location name
                    this.DrawSelection(this._nodeSelectorBlue, false);
                    Camera.main.GetComponent<AudioSource>().PlayOneShot(error, 0.7f);
                }
            }
            else
            {
                // Check if the province is the right province
                if (this._selectedProvince.GetComponent<ProvinceInfo>().fullName == StaticObjects.startProvince)
                {
                    Camera.main.GetComponent<AudioSource>().PlayOneShot(success, 0.7f);

                    // Set HasSelectedProvince to true as the right province has been selected.
                    this.HasSelectedProvince = true;

                    // Disable the provinces game object (holding all province game objects).
                    this.Provinces.SetActive(false);
                    
                    // Enable the locations game object (holding all location game objects).
                    this.Locations.SetActive(true);

                    // Enable the blue selector game object.
                    this._nodeSelectorBlue.SetActive(true);

                    // Create a new list of allowed locations.
                    List<GameObject> allowedLocations = new List<GameObject>();

                    // Temporarely save the provinceInfo object of the selected province to prevent unnecessary lookup.
                    ProvinceInfo selectedProvinceInfo = _selectedProvince.GetComponent<ProvinceInfo>();

                    // Throw an exception if the selected province info has not been found
                    if (selectedProvinceInfo == null)
                    {
                        throw new Exception("Could not find the ProvinceInfo object in the selected province.");
                    }

                    // Remove all locations which are not located in the province.
                    foreach (GameObject location in this._playerNodes)
                    {
                        // Check if the province ids match.
                        if (location.GetComponent<LocationInfo>().provinceId == selectedProvinceInfo.id)
                        {
                            // If the province ids match, add this location to the list of allowed locations
                            allowedLocations.Add(location);
                        }
                        else
                        {
                            // Disable the location as it is not allowed
                            location.SetActive(false);
                        }
                    }

                    // Throw an exception if there aren't any allowed locations.
                    if (allowedLocations.Count == 0)
                    {
                        throw new Exception("Could not find any locations matching the province.");
                    }

                    // Assign the allowed locations list to the global locations list.
                    this._playerNodes = allowedLocations;
                    
                    // Set the selected location to the first location of the allowed locations.
                    this._selectedLocation = allowedLocations[0];

                    // Set the position of the blue selector node to the seleted location.
                    this._nodeSelectorBlue.transform.position = _selectedLocation.transform.position;
                }
                else
                {
                    Camera.main.GetComponent<AudioSource>().PlayOneShot(error, 0.7f);

                    // Reset all province colors
                    foreach (GameObject province in this._provinces)
                    {
                        // Set the color of the province to white.
                        province.GetComponent<SpriteRenderer>().material.color = Color.white;
                    }

                    // Set province color to red
                    this._selectedProvince.GetComponent<SpriteRenderer>().material.color = Color.red;

                    // Set hint message
                    string hintText = "Dit is een hint";
                    GUImanager.instance.SetHintText(hintText, this.PlayerNumber);
                }
            }
        }
    }

    /// <summary>
    /// Method used to draw a hitcollider.
    /// </summary>
    /// <param name="hitCollider">The location game object.</param>
    /// <param name="rightLocation">A boolean indicating if the pressed location is the right location.</param>
    private void DrawSelection(GameObject hitCollider, bool rightLocation)
    {
        // Variable to store the selector gameobject (red or green).
        GameObject selector;

        // Initialize the selector type based on the rightLocation boolean.
        if (rightLocation)
        {
            // Initialize a green circle gameobject if the selected location is the right location.
            this._nodeSelectorGreen = Instantiate(Resources.Load("Selector_green"), Vector3.zero, Quaternion.identity) as GameObject;
            selector = this._nodeSelectorGreen;
        }
        else
        {
            // Initialize a red circle gameobject if the selected location is not the right location.
            this._nodeSelectorRed = Instantiate(Resources.Load("Selector"), Vector3.zero, Quaternion.identity) as GameObject;
            selector = this._nodeSelectorRed;
        }

        // Return if the selector gameobject has not been found to avoid errors.
        if (selector == null) return;
        
        // Position the selector to the position of the location gameobject.
        selector.transform.position = hitCollider.transform.position;
    }
}