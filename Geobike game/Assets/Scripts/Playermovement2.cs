using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class Playermovement2 : MonoBehaviour
{

    public float speed = 0f;
    public float speedmult = 0.15f;
    public GameObject selectedNodePlayer;
    public bool inNode;
    private GameObject nodeSelector;
    public GameObject playerCamera;
    public bool nodeSelectionMoment;
    private List<GameObject> playernodes;
    private int loopNodes;

    private float presses;

    public Text lblspeed;

    private Dijkstra dijkstra;

    public GameObject places;

    public GameObject locations;

    // Use this for initialization
    void Start()
    {
        //player 1
        InvokeRepeating("CalculateSpeed", 0, 2);
        inNode = false;
        nodeSelectionMoment = true;
        dijkstra = StaticObjects.dijkstraInstance;
        loopNodes = 1;

        selectedNodePlayer = null;

        foreach (Transform location in locations.GetComponentInChildren<Transform>())
        {
            Debug.Log(location.gameObject.GetComponent<LocationInfo>().fullName);
            if (location.gameObject.GetComponent<LocationInfo>().fullName == StaticObjects.startPointp1)
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
            if (StaticObjects.enableCityNames)
            {
                places.SetActive(true);
            }
            else
            {
                places.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Joystick2Button0))
            {
                if (loopNodes == playernodes.Count)
                {
                    loopNodes = 0;
                }

                nodeSelector.transform.position = playernodes[loopNodes].transform.position;
                loopNodes++;
            }

            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.Joystick2Button3))
            {
                foreach (GameObject node in playernodes)
                {
                    if (nodeSelector != null && node.transform.position == nodeSelector.transform.position)
                    {
                        selectedNodePlayer = node;
                        Destroy(nodeSelector);
                        nodeSelector = null;
                        playerCamera.GetComponent<Camera>().orthographicSize = 1.65f;
                        nodeSelectionMoment = false;
                        loopNodes = 0;
                        places.SetActive(false);
                    }
                }
                playernodes.Clear();
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

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Joystick2Button2))
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
                Debug.Log("Selector gaat dood");
                Destroy(selector);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Node"))
        {
            inNode = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!inNode)
        {
            Vector3 otherArea = other.transform.position;
            otherArea.x += 0.01f;
            otherArea.y += 0.01f;
            if ((transform.position.x >= other.transform.position.x &&
                 transform.position.y >= other.transform.position.y) ||
                (transform.position.x <= otherArea.x && transform.position.y <= otherArea.y))
            {
                transform.position = other.transform.position;

                if (nodeSelector == null && !GameObject.FindWithTag("NodeSelector"))
                {
                    nodeSelector = Instantiate(Resources.Load("NodeSelector"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    List<string> neighbourNodes = dijkstra.GetNodesAroundNode(selectedNodePlayer.GetComponent<LocationInfo>().id);

                    playernodes = new List<GameObject>();
                    foreach (GameObject node in GameObject.FindGameObjectsWithTag("Node"))
                    {
                        foreach (string name in neighbourNodes)
                        {
                            if (node.GetComponent<LocationInfo>().id == name)
                            {
                                if (node.name.Contains("1"))
                                {
                                    playernodes.Add(node);
                                }
                            }
                        }
                    }

                    GameObject initialNode = playernodes.First();
                    nodeSelector.transform.position = initialNode.transform.position;
                    playerCamera.GetComponent<Camera>().orthographicSize = 4.5f;
                    nodeSelectionMoment = true;
                    selectedNodePlayer = null;
                    //player1nodes.Clear();
                    //dijkstra.GetNodesAroundNode(selectedNodePlayer1.GetComponent<LocationInfo>().fullName);
                    //foreach (string s in dijkstra.GetNodesAroundNode(selectedNodePlayer1.GetComponent<LocationInfo>().fullName))
                    //{
                    //    Debug.Log(s);
                    //}

                    if (nodeSelector != null) nodeSelector.name = "Node Selector";
                }

                inNode = true;
                //selectedNodePlayer1 = null; //blablabla
            }
        }
    }

    public void CalculateSpeed()
    {
        float rpm1 = presses * 30;
        float ms1 = 0.45f * rpm1 * 0.10472f;
        float kmh1 = ms1 * 3.6f;
        lblspeed.text = Mathf.Round(kmh1 * 10 * 2) / 10 + "KM/h";
        presses = 0;
    }
}
