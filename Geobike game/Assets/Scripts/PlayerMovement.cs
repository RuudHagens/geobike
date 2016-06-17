using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0f;
    public float speedmult = 0.15f;
    public Collider2D collider;
    private bool up;
    public bool player1;
    public GameObject selectedNodePlayer1;
    public bool travel;
    private GameObject nodeSelector;
    private GameObject player1Camera;
    private bool nodeSelectionMoment;
    private List<GameObject> player1nodes;
    private int loopNodes;

    private float pressesp1;
    private float pressesp2;

    public Text speedp1;
    public Text speedp2;

    private Dijkstra dijkstra;

    // Use this for initialization
    void Start () {
	    up = true;
        InvokeRepeating("CalculateSpeed", 0, 2);
        selectedNodePlayer1 = GameObject.Find("Location-Almelo 1");
        transform.position = selectedNodePlayer1.transform.position;
        //selectedNodePlayer1 = GameObject.Find("Location-Enschede 1");
        travel = false;
        nodeSelectionMoment = true;
        dijkstra = Dijkstra.Instance;
        //setUpDijkstra();
        player1Camera = GameObject.Find("Player 1 Camera");
        loopNodes = 1;
    }

    /*private void setUpDijkstra()
    {
        //DE lijst
        List<GraphNode> graphAslist = new List<GraphNode>();

        //DE lijst binnen de lijst
        ArrayList vertexForList = new ArrayList();

        vertexForList.Add(new Vertex("haa", 35.7f));
        vertexForList.Add(new Vertex("ams", 42.5f));
        vertexForList.Add(new Vertex("lel", 74.6f));
        vertexForList.Add(new Vertex("sne", 95.6f));
        graphAslist.Add(new GraphNode("alk", new ArrayList(vertexForList)));
        vertexForList.Clear();


        vertexForList.Add(new Vertex("alk", 35.7f));
        vertexForList.Add(new Vertex("ams", 19.5f));
        vertexForList.Add(new Vertex("lei", 33.7f));
        graphAslist.Add(new GraphNode("haa", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("alk", 42.5f));
        vertexForList.Add(new Vertex("haa", 19.5f));
        vertexForList.Add(new Vertex("alm", 33.5f));
        vertexForList.Add(new Vertex("utr", 43.5f));
        vertexForList.Add(new Vertex("lei", 46.9f));
        graphAslist.Add(new GraphNode("ams", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("haa", 33.7f));
        vertexForList.Add(new Vertex("ams", 46.9f));
        vertexForList.Add(new Vertex("utr", 58.8f));
        vertexForList.Add(new Vertex("dha", 24.1f));
        graphAslist.Add(new GraphNode("lei", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("lei", 24.1f));
        vertexForList.Add(new Vertex("utr", 68.5f));
        vertexForList.Add(new Vertex("del", 11.6f));
        graphAslist.Add(new GraphNode("dha", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("dha", 11.6f));
        vertexForList.Add(new Vertex("rot", 15.6f));
        graphAslist.Add(new GraphNode("del", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("del", 15.6f));
        vertexForList.Add(new Vertex("utr", 61.8f));
        vertexForList.Add(new Vertex("dor", 25.5f));
        vertexForList.Add(new Vertex("zie", 66.2f));
        graphAslist.Add(new GraphNode("rot", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ams", 43.5f));
        vertexForList.Add(new Vertex("lei", 58.8f));
        vertexForList.Add(new Vertex("dha", 68.5f));
        vertexForList.Add(new Vertex("rot", 61.8f));
        vertexForList.Add(new Vertex("dor", 65.8f));
        vertexForList.Add(new Vertex("bos", 56.9f));
        vertexForList.Add(new Vertex("nij", 84.6f));
        vertexForList.Add(new Vertex("ame", 23.5f));
        vertexForList.Add(new Vertex("alm", 41.3f));
        graphAslist.Add(new GraphNode("utr", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("rot", 25.5f));
        vertexForList.Add(new Vertex("utr", 65.8f));
        vertexForList.Add(new Vertex("bos", 62.8f));
        vertexForList.Add(new Vertex("bre", 35.6f));
        vertexForList.Add(new Vertex("roo", 44.7f));
        graphAslist.Add(new GraphNode("dor", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("rot", 66.2f));
        vertexForList.Add(new Vertex("roo", 59.4f));
        vertexForList.Add(new Vertex("mid", 42.3f));
        vertexForList.Add(new Vertex("goe", 22.9f));
        graphAslist.Add(new GraphNode("zie", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("zie", 42.3f));
        vertexForList.Add(new Vertex("goe", 25.8f));
        vertexForList.Add(new Vertex("ter", 32.8f));
        graphAslist.Add(new GraphNode("mid", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("mid", 32.8f));
        vertexForList.Add(new Vertex("goe", 35.4f));
        graphAslist.Add(new GraphNode("ter", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ter", 35.4f));
        vertexForList.Add(new Vertex("mid", 25.8f));
        vertexForList.Add(new Vertex("zie", 22.9f));
        graphAslist.Add(new GraphNode("goe", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("zie", 59.4f));
        vertexForList.Add(new Vertex("dor", 44.7f));
        vertexForList.Add(new Vertex("bre", 24.6f));
        graphAslist.Add(new GraphNode("roo", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("roo", 24.6f));
        vertexForList.Add(new Vertex("dor", 35.6f));
        vertexForList.Add(new Vertex("til", 28.0f));
        graphAslist.Add(new GraphNode("bre", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("bre", 28.0f));
        vertexForList.Add(new Vertex("bos", 24.1f));
        vertexForList.Add(new Vertex("ein", 34.7f));
        graphAslist.Add(new GraphNode("til", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("til", 34.7f));
        vertexForList.Add(new Vertex("bos", 32.8f));
        vertexForList.Add(new Vertex("nij", 61.3f));
        vertexForList.Add(new Vertex("ven", 58.6f));
        vertexForList.Add(new Vertex("roe", 51.2f));
        graphAslist.Add(new GraphNode("ein", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ein", 58.6f));
        vertexForList.Add(new Vertex("nij", 62.4f));
        vertexForList.Add(new Vertex("roe", 29.3f));
        graphAslist.Add(new GraphNode("ven", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ven", 29.3f));
        vertexForList.Add(new Vertex("ein", 31.2f));
        vertexForList.Add(new Vertex("maa", 48.3f));
        vertexForList.Add(new Vertex("hrl", 40.9f));
        graphAslist.Add(new GraphNode("roe", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("roe", 48.3f));
        vertexForList.Add(new Vertex("hrl", 24.9f));
        graphAslist.Add(new GraphNode("maa", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("maa", 24.9f));
        vertexForList.Add(new Vertex("roe", 40.9f));
        graphAslist.Add(new GraphNode("hrl", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("til", 24.1f));
        vertexForList.Add(new Vertex("ein", 32.8f));
        vertexForList.Add(new Vertex("nij", 48.3f));
        vertexForList.Add(new Vertex("utr", 56.9f));
        vertexForList.Add(new Vertex("dor", 62.8f));
        graphAslist.Add(new GraphNode("bos", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ven", 62.4f));
        vertexForList.Add(new Vertex("ein", 61.3f));
        vertexForList.Add(new Vertex("bos", 48.3f));
        vertexForList.Add(new Vertex("utr", 84.6f));
        vertexForList.Add(new Vertex("arn", 23.1f));
        graphAslist.Add(new GraphNode("nij", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("nij", 23.1f));
        vertexForList.Add(new Vertex("ame", 51.8f));
        vertexForList.Add(new Vertex("ens", 95.7f));
        vertexForList.Add(new Vertex("ape", 31.7f));
        graphAslist.Add(new GraphNode("arn", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("arn", 51.8f));
        vertexForList.Add(new Vertex("utr", 23.4f));
        vertexForList.Add(new Vertex("alm", 41.5f));
        graphAslist.Add(new GraphNode("ame", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ame", 41.5f));
        vertexForList.Add(new Vertex("utr", 41.3f));
        vertexForList.Add(new Vertex("ams", 33.5f));
        vertexForList.Add(new Vertex("lel", 30.5f));
        vertexForList.Add(new Vertex("ape", 74.1f));
        graphAslist.Add(new GraphNode("alm", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("alm", 30.5f));
        vertexForList.Add(new Vertex("alk", 74.6f));
        vertexForList.Add(new Vertex("zwo", 49.6f));
        graphAslist.Add(new GraphNode("lel", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("arn", 31.7f));
        vertexForList.Add(new Vertex("alm", 74.1f));
        vertexForList.Add(new Vertex("zwo", 39.0f));
        vertexForList.Add(new Vertex("alo", 60.3f));
        graphAslist.Add(new GraphNode("ape", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("arn", 95.7f));
        vertexForList.Add(new Vertex("alo", 29.4f));
        vertexForList.Add(new Vertex("hoo", 74.3f));
        graphAslist.Add(new GraphNode("ens", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ens", 29.4f));
        vertexForList.Add(new Vertex("ape", 60.3f));
        vertexForList.Add(new Vertex("zwo", 52.3f));
        vertexForList.Add(new Vertex("hoo", 51.6f));
        graphAslist.Add(new GraphNode("alo", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("lel", 49.6f));
        vertexForList.Add(new Vertex("ape", 39.0f));
        vertexForList.Add(new Vertex("alo", 52.3f));
        vertexForList.Add(new Vertex("hoo", 53.5f));
        vertexForList.Add(new Vertex("hee", 62.9f));
        graphAslist.Add(new GraphNode("zwo", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ens", 74.3f));
        vertexForList.Add(new Vertex("alo", 51.6f));
        vertexForList.Add(new Vertex("zwo", 43.5f));
        vertexForList.Add(new Vertex("hee", 63.5f));
        vertexForList.Add(new Vertex("ass", 34.3f));
        vertexForList.Add(new Vertex("emm", 33.0f));
        graphAslist.Add(new GraphNode("hoo", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("hoo", 33.0f));
        vertexForList.Add(new Vertex("ass", 38.9f));
        graphAslist.Add(new GraphNode("emm", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("emm", 38.9f));
        vertexForList.Add(new Vertex("hoo", 34.3f));
        vertexForList.Add(new Vertex("gro", 32.2f));
        graphAslist.Add(new GraphNode("ass", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ass", 32.2f));
        vertexForList.Add(new Vertex("hee", 60.0f));
        vertexForList.Add(new Vertex("lee", 60.0f));
        graphAslist.Add(new GraphNode("gro", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("zwo", 62.9f));
        vertexForList.Add(new Vertex("hoo", 63.5f));
        vertexForList.Add(new Vertex("gro", 60.0f));
        vertexForList.Add(new Vertex("lee", 31.2f));
        vertexForList.Add(new Vertex("sne", 25.8f));
        graphAslist.Add(new GraphNode("hee", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("hee", 25.8f));
        vertexForList.Add(new Vertex("lee", 22.0f));
        vertexForList.Add(new Vertex("alk", 95.6f));
        graphAslist.Add(new GraphNode("sne", new ArrayList(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("sne", 22.0f));
        vertexForList.Add(new Vertex("hee", 31.2f));
        vertexForList.Add(new Vertex("gro", 60.0f));
        graphAslist.Add(new GraphNode("lee", new ArrayList(vertexForList)));
        vertexForList.Clear();

        dijkstra = new Dijkstra();

        dijkstra.SetGraph(graphAslist);
    }*/

    void Update()
    {
        if (player1)
        {
            if (nodeSelectionMoment)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (loopNodes == player1nodes.Count)
                    {
                        loopNodes = 0;
                    }

                    nodeSelector.transform.position = player1nodes[loopNodes].transform.position;
                    loopNodes++;
                }

                if (Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    foreach (GameObject node in player1nodes)
                    {
                        if (node.transform.position == nodeSelector.transform.position)
                        {
                            selectedNodePlayer1 = node;
                            Destroy(nodeSelector);
                            player1Camera.GetComponent<Camera>().orthographicSize = 1.65f;
                            nodeSelectionMoment = false;
                            loopNodes = 0;
                        }
                    }
                }
            }
            else
            {
                //if (up)
                //{
                if (speed > 0f)
                {
                    speed = speed - 0.05f;
                }

                if (speed < 0f)
                {
                    speed = 0f;
                }

                //Quaternion rotation = Quaternion.LookRotation
                //    (selectedNodePlayer1.transform.position - transform.position, transform.TransformDirection(Vector3.up));
                //transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
                //rotation = new Quaternion(0, 0, rotation.z, rotation.w);
                //Debug.Log(selectedNodePlayer1.name);

                transform.position += (selectedNodePlayer1.transform.position - transform.position).normalized*speed*
                                      Time.deltaTime*10;
                //transform.LookAt(selectedNodePlayer1.transform);

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    transform.position += Vector3.left*speed*Time.deltaTime*10;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    transform.position += Vector3.right*speed*Time.deltaTime*10;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    speed = speed + speedmult;
                    pressesp1++;
                }
            }
            //}
            //else
            //{
            //    if (speed < 0f)
            //    {
            //        speed = speed + 0.05f;
            //    }

            //    if (speed > 0f)
            //    {
            //        speed = 0f;
            //    }

            //    transform.position += Vector3.up*speed*Time.deltaTime * 10;

            //    if (Input.GetKeyDown(KeyCode.RightArrow))
            //    {
            //        transform.position += Vector3.left*speed*Time.deltaTime * 10;
            //    }
            //    if (Input.GetKeyDown(KeyCode.LeftArrow))
            //    {
            //        transform.position += Vector3.right*speed*Time.deltaTime * 10;
            //    }
            //    if (Input.GetKeyDown(KeyCode.Space))
            //    {
            //        speed = speed - 0.15f;
            //        pressesp1++;
            //    }
            //}
        }
        else
        {
            if (up)
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

                transform.position += Vector3.up * speed * Time.deltaTime*10;

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
        //if (other.gameObject.CompareTag("Collider"))
        //{
        //    if (up)
        //    {
        //        up = false;
        //    }
        //    else
        //    {
        //        up = true;
        //    }
        //}

        if (other.gameObject.CompareTag("Node")/* && transform.position == other.transform.position*/)
        {
            travel = false;
            GameObject[] selectors = GameObject.FindGameObjectsWithTag("NodeSelector");
            foreach (GameObject selector in selectors)
            {
                Debug.Log("Selector gaat dood");
                Destroy(selector);
            }
        }
    }

    //private GameObject foundNode;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Node")/* && transform.position == other.transform.position*/)
        {
            //foundNode = other.gameObject;
            Debug.Log("yooo" + other.name);
            travel = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!travel)
        {
            Vector3 otherArea = other.transform.position;
            otherArea.x += 0.01f;
            otherArea.y += 0.01f;
            if ((transform.position.x >= other.transform.position.x &&
                 transform.position.y >= other.transform.position.y) ||
                (transform.position.x <= otherArea.x && transform.position.y <= otherArea.y))
            {
                transform.position = other.transform.position;

                if (nodeSelector == null)
                {
                    nodeSelector =
                        Instantiate(Resources.Load("NodeSelector"), new Vector3(0, 0, 0), Quaternion.identity) as
                            GameObject;
                    List<string> neighbourNodes =
                        dijkstra.GetNodesAroundNode(selectedNodePlayer1.GetComponent<LocationInfo>().id);

                    player1nodes = new List<GameObject>();
                    foreach (GameObject node in GameObject.FindGameObjectsWithTag("Node"))
                    {
                        foreach (string name in neighbourNodes)
                        {
                            if (node.GetComponent<LocationInfo>().id == name)
                            {
                                if (node.name.Contains("1"))
                                {
                                    player1nodes.Add(node);
                                }
                            }
                        }

                        
                        //if (node.GetComponent<LocationInfo>().id == selectedNodePlayer1.GetComponent<LocationInfo>().id)
                        //{
                            
                        //}
                    }

                    GameObject initialNode = player1nodes.First();
                    nodeSelector.transform.position = initialNode.transform.position;
                    player1Camera.GetComponent<Camera>().orthographicSize = 4.5f;
                    nodeSelectionMoment = true;
                    //dijkstra.GetNodesAroundNode(selectedNodePlayer1.GetComponent<LocationInfo>().fullName);
                    //foreach (string s in dijkstra.GetNodesAroundNode(selectedNodePlayer1.GetComponent<LocationInfo>().fullName))
                    //{
                    //    Debug.Log(s);
                    //}

                    if (nodeSelector != null) nodeSelector.name = "Node Selector";
                }

                travel = true;
                selectedNodePlayer1 = null; //blablabla
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
            speedp1.text = Mathf.Round(kmh1 * 10) / 10 + "KM/h";
            Debug.Log("speed p1 spekkoen: " + kmh1);
            pressesp1 = 0;
        }
        else
        {
            float rpm2 = pressesp2 * 30;
            float ms2 = 0.45f * rpm2 * 0.10472f;
            float kmh2 = ms2 * 3.6f;
            speedp2.text = Mathf.Round(kmh2 * 10) / 10 + "KM/h";
            Debug.Log("speed p2 scharnier: " + kmh2);
            pressesp2 = 0;
        }
    }
}

//rpm 90
//omtrek m = 2 * pi * radius
//tijd in km/h = rpm * omtrek / 60 * 3.6
