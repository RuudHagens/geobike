using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerMovementPerPlayer : MonoBehaviour
{
    public GameObject bike;
    public float speedmult = 0.15f;
    public Text lblspeed;
    public GameObject playerCamera;  
    public GameObject locationLabels;
    public GameObject locations;
    public KeyCode Cycling;
    public KeyCode RightSteeringWheel;
    public KeyCode LeftSteeringWheel;

    private int loopNodes;
    private float speed = 0f;
    private float presses;
    private Dijkstra dijkstra;
    private List<GameObject> playerNodes;
    private bool inNode;
    private bool nodeSelectionMoment;
    private GameObject nodeSelector;
    private GameObject selectedNodePlayer;

    // Use this for initialization
    void Start()
    {
        //player setup
        InvokeRepeating("CalculateSpeed", 0, 2);
        inNode = true;
        nodeSelectionMoment = true;
        dijkstra = StaticObjects.dijkstraInstance;
        loopNodes = 1;

        selectedNodePlayer = null;

        foreach (Transform location in locations.GetComponentInChildren<Transform>())
        {
            if (location.gameObject.GetComponent<LocationInfo>().fullName == StaticObjects.startPoint)
            {
                selectedNodePlayer = location.gameObject;
            }
        }

        if (selectedNodePlayer != null)
        {
            transform.position = selectedNodePlayer.transform.position;
        }
        else
        {
            Debug.Log("error");
        }

    }

    void Update()
    {
        if (nodeSelectionMoment)
        {
            locationLabels.SetActive(StaticObjects.enableCityNames);

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(RightSteeringWheel))
            {
                if (loopNodes == playerNodes.Count)
                {
                    loopNodes = 0;
                }

                nodeSelector.transform.position = playerNodes[loopNodes].transform.position;
                loopNodes++;
            }

            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(LeftSteeringWheel))
            {
                foreach (GameObject node in playerNodes)
                {
                    if (nodeSelector != null && node.transform.position == nodeSelector.transform.position)
                    {
                        selectedNodePlayer = node;
                        bike.GetComponent<BikeInput>().RotateBike(selectedNodePlayer);
                        Destroy(nodeSelector);
                        nodeSelector = null;
                        playerCamera.GetComponent<Camera>().orthographicSize = 1.65f;
                        nodeSelectionMoment = false;
                        loopNodes = 0;
                        locationLabels.SetActive(false);
                    }
                }
                playerNodes.Clear();
            }
        }
        else
        {
            if (speed > 0f)
            {
                speed = speed - 0.05f;
            }

            if (speed < 0f)
            {
                speed = 0f;
            }

            transform.position += (selectedNodePlayer.transform.position - transform.position).normalized * speed *
                                  Time.deltaTime * 10;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(Cycling))
            {
                speed = speed + speedmult;
                presses++;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Node"))
        {
            inNode = false;
            GameObject[] selectors = GameObject.FindGameObjectsWithTag("NodeSelector");
            foreach (GameObject selector in selectors)
            {
                Destroy(selector);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Node"))
        {
            inNode = true;

            if(this.name == "Player 1")
            {
                StaticObjects.visitedLocationsPlayer1.Add(other.gameObject.GetComponent<LocationInfo>().id);
            }
            else if (this.name == "Player 2")
            {
                StaticObjects.visitedLocationsPlayer2.Add(other.gameObject.GetComponent<LocationInfo>().id);
            }

            if (other.gameObject.GetComponent<LocationInfo>().fullName == StaticObjects.endPoint)
            {
                SceneManager.LoadScene("end scene");
            }
        }
    }

    void OnTriggerStay2D(Collider2D otherNode)
    {
        if (inNode && otherNode.gameObject.CompareTag("Node"))
        {
            Vector3 otherPositionPlus = otherNode.transform.position;
            otherPositionPlus.x += 0.01f;
            otherPositionPlus.y += 0.01f;

            Vector3 otherPositionMin = otherNode.transform.position;
            otherPositionMin.x -= 0.01f;
            otherPositionMin.y -= 0.01f;

            if ((transform.position.x >= otherNode.transform.position.x &&
                 transform.position.y >= otherNode.transform.position.y) ||
                (transform.position.x <= otherPositionPlus.x && transform.position.y <= otherPositionPlus.y) ||
                (transform.position.x >= otherPositionMin.x && transform.position.y >= otherPositionMin.y))
            {
                transform.position = otherNode.transform.position;

                if (nodeSelector == null && selectedNodePlayer != null)
                {
                    nodeSelector = Instantiate(Resources.Load("NodeSelector"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    List<string> neighbourNodes = dijkstra.GetNodesAroundNode(selectedNodePlayer.GetComponent<LocationInfo>().id);

                    playerNodes = new List<GameObject>();

                    foreach (Transform location in locations.GetComponentInChildren<Transform>())
                    {
                        foreach (string name in neighbourNodes)
                        {
                            if (location.gameObject.GetComponent<LocationInfo>().id == name)
                            {
                                playerNodes.Add(location.gameObject);
                            }
                        }
                    }

                    GameObject initialNode = playerNodes.First();
                    nodeSelector.transform.position = initialNode.transform.position;
                    playerCamera.GetComponent<Camera>().orthographicSize = 4.5f;
                    nodeSelectionMoment = true;
                    selectedNodePlayer = null;

                    if (nodeSelector != null) nodeSelector.name = "Node Selector";
                }

                inNode = false;
            }
        }
    }

    public void CalculateSpeed()
    {
        float rpm1 = presses * 30;
        float ms1 = 0.45f * rpm1 * 0.10472f;
        float kmh1 = ms1 * 3.6f;
        float totalKmh = Mathf.Round(kmh1 * 10 * 2) / 10;
        lblspeed.text = totalKmh + "KM/h";
        this.bike.GetComponent<BikeInput>().Swing(totalKmh);
        presses = 0;
    }
}
