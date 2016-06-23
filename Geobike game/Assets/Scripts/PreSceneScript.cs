using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PreSceneScript : MonoBehaviour
{
    public GameObject locations;

    public KeyCode RightSteeringWheel;
    public KeyCode LeftSteeringWheel;

    private GameObject selectorSprite;

    private GameObject startNode;

    private int loopNodes;

    public bool done;

    private GameObject nodeSelector;
    private List<GameObject> playerNodes;

    private void Start()
    {
        done = false;

        playerNodes = new List<GameObject>();

        foreach (Transform location in locations.GetComponentInChildren<Transform>())
        {
            playerNodes.Add(location.gameObject);
        }

        nodeSelector = Instantiate(Resources.Load("NodeSelector"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        nodeSelector.transform.position = playerNodes[1].transform.position;
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
                    DrawSelection(nodeSelector);
                    done = true;
                    Destroy(nodeSelector);
                    nodeSelector = null;
                    loopNodes = 0;
                }
            }
            playerNodes.Clear();
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