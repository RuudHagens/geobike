using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PreSceneScript : MonoBehaviour
{
    /// <summary>
    /// The locations game object, holding the various location objects.
    /// </summary>
    public GameObject locations;

    /// <summary>
    /// The keycode used to cycle through the locations.
    /// </summary>
    public KeyCode cycling;

    /// <summary>
    /// The keycode used to steer to the right.
    /// </summary>
    public KeyCode rightSteeringWheel;

    /// <summary>
    /// The keycode used to steer to the left.
    /// </summary>
    public KeyCode leftSteeringWheel;

    /// <summary>
    /// The keyboard keycode used to cycle through the locations.
    /// </summary>
    public KeyCode cyclingFromKeyboard;

    /// <summary>
    /// The keyboard keycode used to steer to the right.
    /// </summary>
    public KeyCode rightSteeringWheelFromKeyboard;

    /// <summary>
    /// The keyboard keycode used to steer to the left.
    /// </summary>
    public KeyCode leftSteeringWheelFromKeyboard;

    /// <summary>
    /// A boolean indicating whether the right node has been pressed and the player is ready.
    /// </summary>
    public bool done;

    /// <summary>
    /// The startNode object.
    /// </summary>
    private GameObject startNode;

    /// <summary>
    /// Used to store the index of the selected node.
    /// </summary>
    private int loopNodes;

    /// <summary>
    /// The red nodeselector object.
    /// </summary>
    private GameObject nodeSelectorBlue;

    /// <summary>
    /// The green nodeselector object.
    /// </summary>
    private GameObject nodeSelectorGreen;

    /// <summary>
    /// The red nodeselector object.
    /// </summary>
    private GameObject nodeSelectorRed;

    /// <summary>
    /// A list of player node objects.
    /// </summary>
    private List<GameObject> playerNodes;

    private void Start()
    {
        loopNodes = 1;

        done = false;

        playerNodes = new List<GameObject>();

        foreach(Transform location in locations.GetComponentInChildren<Transform>())
        {
            playerNodes.Add(location.gameObject);
        }

        nodeSelectorBlue = Instantiate(Resources.Load("NodeSelector"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        nodeSelectorBlue.transform.position = playerNodes[0].transform.position;
    }

    private void Update()
    {
        detectPressedKeyOrButton();

        if (!done)
        {
            MoveSelector();
        }
        
        if (startNode != null)
        {
            done = true;
        }
    }
    
    /// <summary>
    /// Method used to print a pressed key (used for debugging).
    /// </summary>
    public void detectPressedKeyOrButton()
    {
        foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                Debug.Log("KeyCode down: " + kcode);
            }
        }
    }

    /// <summary>
    /// Method to move the selector to the next or previous location node.
    /// </summary>
    public void MoveSelector()
    {
        if (Input.GetKeyDown(rightSteeringWheelFromKeyboard) || Input.GetKeyDown(rightSteeringWheel))
        {
            loopNodes++;
            if (loopNodes >= playerNodes.Count)
            {
                loopNodes = 0;
            }

            nodeSelectorBlue.transform.position = playerNodes[loopNodes].transform.position;

            Destroy(this.nodeSelectorRed);
            this.nodeSelectorRed = null;

        }
        if (Input.GetKeyDown(leftSteeringWheelFromKeyboard) || Input.GetKeyDown(leftSteeringWheel))
        {
            loopNodes--;
            if (loopNodes <  0)
            {
                loopNodes = playerNodes.Count - 1;
            }

            nodeSelectorBlue.transform.position = playerNodes[loopNodes].transform.position;

            Destroy(this.nodeSelectorRed);
            this.nodeSelectorRed = null;
        }

        if (Input.GetKeyDown(cyclingFromKeyboard) || Input.GetKeyDown(cycling))
        {
            Destroy(this.nodeSelectorRed);
            this.nodeSelectorRed = null;

            foreach (GameObject node in playerNodes)
            {
                if (nodeSelectorBlue != null && node.transform.position == nodeSelectorBlue.transform.position)
                {
                    if (node.GetComponent<LocationInfo>().fullName == StaticObjects.startPoint)
                    {
                        DrawSelection(nodeSelectorBlue, true);
                        done = true;
                        Destroy(nodeSelectorBlue);
                        nodeSelectorBlue = null;
                        loopNodes = 0;
                    }
                    else
                    {
                        DrawSelection(nodeSelectorBlue, false);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Method used to draw a hitcollider.
    /// </summary>
    /// <param name="hitCollider"></param>
    private void DrawSelection(GameObject hitCollider, bool rightLocation)
    {
        if (rightLocation)
        {
            this.nodeSelectorGreen = Instantiate(Resources.Load("Selector_green"), Vector3.zero, Quaternion.identity) as GameObject;

            if (nodeSelectorGreen != null)
            {
                Vector3 position = nodeSelectorGreen.transform.position;
                position.x = hitCollider.transform.position.x;
                position.y = hitCollider.transform.position.y;
                nodeSelectorGreen.transform.position = position;
            }
        }
        else
        {
            this.nodeSelectorRed = Instantiate(Resources.Load("Selector"), Vector3.zero, Quaternion.identity) as GameObject;

            if (this.nodeSelectorRed != null)
            {
                Vector3 position = this.nodeSelectorRed.transform.position;
                position.x = hitCollider.transform.position.x;
                position.y = hitCollider.transform.position.y;
                position.z = -0.01f;
                this.nodeSelectorRed.transform.position = position;
            }
        }
    }
}