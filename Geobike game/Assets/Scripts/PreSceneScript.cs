using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PreSceneScript : MonoBehaviour
{
    public GameObject locations;

    public KeyCode cycling;
    public KeyCode rightSteeringWheel;
    public KeyCode leftSteeringWheel;

    public KeyCode cyclingFromKeyboard;
    public KeyCode rightSteeringWheelFromKeyboard;
    public KeyCode leftSteeringWheelFromKeyboard;

    private GameObject selectorSprite;

    private GameObject startNode;

    private int loopNodes;

    public bool done;

    private GameObject nodeSelector;
    private List<GameObject> playerNodes;

    private void Start()
    {
        loopNodes = 1;

        done = false;

        playerNodes = new List<GameObject>();

        foreach (Transform location in locations.GetComponentInChildren<Transform>())
        {
            playerNodes.Add(location.gameObject);
        }

        nodeSelector = Instantiate(Resources.Load("NodeSelector"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        nodeSelector.transform.position = playerNodes[0].transform.position;
    }

    private void Update()
    {
        if(!done)
        {
            MoveSelector();
        }
        
        if (startNode != null)
        {
            done = true;
        }
    }

    public void MoveSelector()
    {
        if (Input.GetKeyDown(rightSteeringWheelFromKeyboard) || Input.GetKeyDown(rightSteeringWheel))
        {
            loopNodes++;
            if (loopNodes >= playerNodes.Count)
            {
                loopNodes = 0;
            }

            nodeSelector.transform.position = playerNodes[loopNodes].transform.position;
        }
        if (Input.GetKeyDown(leftSteeringWheelFromKeyboard) || Input.GetKeyDown(leftSteeringWheel))
        {
            loopNodes--;
            if (loopNodes <  0)
            {
                loopNodes = playerNodes.Count - 1;
            }

            nodeSelector.transform.position = playerNodes[loopNodes].transform.position;
        }

        if (Input.GetKeyDown(cyclingFromKeyboard) || Input.GetKeyDown(cycling))
        {
            foreach (GameObject node in playerNodes)
            {
                if (nodeSelector != null && node.transform.position == nodeSelector.transform.position)
                {
                    if(node.GetComponent<LocationInfo>().fullName == StaticObjects.startPoint)
                    {
                        DrawSelection(nodeSelector);
                        done = true;
                        Destroy(nodeSelector);
                        nodeSelector = null;
                        loopNodes = 0;
                    }
                }
            }
        }
    }

    private void DrawSelection(GameObject hitCollider)
    {
        selectorSprite = Instantiate(Resources.Load("Selector"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        if (selectorSprite != null)
        {
            Vector3 position = selectorSprite.transform.position;
            position.x = hitCollider.transform.position.x;
            position.y = hitCollider.transform.position.y;
            selectorSprite.transform.position = position;
        }
    }
}