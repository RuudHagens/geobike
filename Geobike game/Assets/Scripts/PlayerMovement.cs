using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0f;
    public float speedmult = 0.15f;
    public Collider2D collider;
    public bool player1;
    public GameObject selectedNodePlayer;
    public bool inNode;
    private GameObject nodeSelector;
    private GameObject player1Camera;
    public bool nodeSelectionMoment;
    private List<GameObject> player1nodes;
    private int loopNodes;
    //public List<string> neighbourNodes;

    private float pressesp1;
    private float pressesp2;

    public Text speedp1;
    public Text speedp2;

    private Dijkstra dijkstra;

    public GameObject placesplayer1;
    public GameObject placesplayer2;

    public GameObject locations;

    // Use this for initialization
    void Start()
    {
        //player 1
        InvokeRepeating("CalculateSpeed", 0, 2);   
        inNode = true;
        nodeSelectionMoment = true;
        dijkstra = StaticObjects.dijkstraInstance;
        player1Camera = GameObject.Find("Player 1 Camera");
        loopNodes = 1;

        selectedNodePlayer = null;

        foreach (Transform location in locations.GetComponentInChildren<Transform>())
        {
            //Debug.Log(location.gameObject.GetComponent<LocationInfo>().fullName);
            if (location.gameObject.GetComponent<LocationInfo>().fullName == StaticObjects.startPoint)
            {
                selectedNodePlayer = location.gameObject;
            }
        }

        if(selectedNodePlayer != null)
        {
            transform.position = selectedNodePlayer.transform.position;
        }
        else
        {
            Debug.Log("error");
        }

        GUImanager.instance.setTargetText();
    }

    void Update()
    {
        if (player1)
        {
            if (nodeSelectionMoment)
            {
                if (StaticObjects.enableCityNames)
                {
                    placesplayer1.SetActive(true);
                }
                else
                {
                    placesplayer1.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Joystick1Button0))
                {
                    if (loopNodes == player1nodes.Count)
                    {
                        loopNodes = 0;
                    }

                    nodeSelector.transform.position = player1nodes[loopNodes].transform.position;
                    loopNodes++;
                }

                if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Joystick1Button3))
                {
                    foreach (GameObject node in player1nodes)
                    {
                        if (nodeSelector != null && node.transform.position == nodeSelector.transform.position)
                        {
                            selectedNodePlayer = node;
                            Destroy(nodeSelector);
                            nodeSelector = null;
                            player1Camera.GetComponent<Camera>().orthographicSize = 1.65f;
                            nodeSelectionMoment = false;
                            loopNodes = 0;
                            placesplayer1.SetActive(false);
                        }
                    }
                    player1nodes.Clear();
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

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button2))
                {
                    speed = speed + speedmult;
                    pressesp1++;
                }
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

            transform.position += Vector3.up * speed * Time.deltaTime * 10;

            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.position += Vector3.left * speed * Time.deltaTime * 10;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.position += Vector3.right * speed * Time.deltaTime * 10;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                speed = speed + speedmult;
                pressesp2++;
            }
            else
            {
                if (speed < 0f)
                {
                    speed = speed + 0.05f;
                }

                if (speed > 0f)
                {
                    speed = 0f;
                }

                transform.position += Vector3.up * speed * Time.deltaTime * 10;

                if (Input.GetKeyDown(KeyCode.D))
                {
                    transform.position += Vector3.left * speed * Time.deltaTime * 10;
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    transform.position += Vector3.right * speed * Time.deltaTime * 10;
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    speed = speed - speedmult;
                    pressesp2++;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (inNode && other.gameObject.CompareTag("Node"))
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
            inNode = true;
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

            //if (transform.position.x >= otherPosition.x - 0.01f && transform.position.x <= otherPosition.x + 0.01f &&
            //    transform.position.y >= otherPosition.y - 0.01f && transform.position.y <=  otherPosition.y + 0.01f)


            if ((transform.position.x >= otherNode.transform.position.x &&
                 transform.position.y >= otherNode.transform.position.y) ||
                (transform.position.x <= otherPositionPlus.x && transform.position.y <= otherPositionPlus.y) ||
                (transform.position.x >= otherPositionMin.x && transform.position.y >= otherPositionMin.y))
            {
                transform.position = otherNode.transform.position;

                if (nodeSelector == null && !GameObject.FindWithTag("NodeSelector") && selectedNodePlayer != null)
                {
                    nodeSelector = Instantiate(Resources.Load("NodeSelector"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    List<string> neighbourNodes = dijkstra.GetNodesAroundNode(selectedNodePlayer.GetComponent<LocationInfo>().id);

                    player1nodes = new List<GameObject>();

                    foreach (Transform location in locations.GetComponentInChildren<Transform>())
                    {
                        foreach (string name in neighbourNodes)
                        {
                            if (location.gameObject.GetComponent<LocationInfo>().id == name)
                            {
                                    player1nodes.Add(location.gameObject);
                            }
                        }
                    }

                    GameObject initialNode = player1nodes.First();
                    nodeSelector.transform.position = initialNode.transform.position;
                    player1Camera.GetComponent<Camera>().orthographicSize = 4.5f;
                    nodeSelectionMoment = true;
                    selectedNodePlayer = null;

                    if (nodeSelector != null) nodeSelector.name = "Node Selector";
                }

                inNode = false;
                //selectedNodePlayer1 = null; //blablabla
            }
        }
    }

    public void CalculateSpeed()
    {
        if (player1)
        {
            float rpm1 = pressesp1 * 30;
            float ms1 = 0.45f * rpm1 * 0.10472f;
            float kmh1 = ms1 * 3.6f;
            speedp1.text = Mathf.Round(kmh1 * 10 * 2) / 10 + "KM/h";
            Debug.Log("speed p1 spekkoen: " + kmh1);
            pressesp1 = 0;
        }
        else
        {
            float rpm2 = pressesp2 * 30;
            float ms2 = 0.45f * rpm2 * 0.10472f;
            float kmh2 = ms2 * 3.6f;
            speedp2.text = Mathf.Round(kmh2 * 10 * 2) / 10 + "KM/h";
            Debug.Log("speed p2 scharnier: " + kmh2);
            pressesp2 = 0;
        }
    }
}

//rpm 90
//omtrek m = 2 * pi * radius
//tijd in km/h = rpm * omtrek / 60 * 3.6
