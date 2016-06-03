using UnityEngine;
using System.Collections;

public class PreSceneScript : MonoBehaviour
{

    public GameObject tileSelectionMarker;
    private GameObject selectorSprite;
    private GameObject startNode;
    private GameObject endNode;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            Debug.Log("mouse pos " + mousePosition.x + " y " + mousePosition.y + " ");


            if (hitCollider)
            {
                if (hitCollider.CompareTag("Node"))
                {
                    if (startNode == null)
                    {
                        selectorSprite = Instantiate(Resources.Load("Selector"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                        if (selectorSprite != null)
                        {
                            Vector3 position = selectorSprite.transform.position;
                            position.x = hitCollider.transform.position.x;
                            position.y = hitCollider.transform.position.y;
                            selectorSprite.transform.position = position;

                            Debug.Log("Node gevonden! Hit " + hitCollider.transform.name + " x" + hitCollider.transform.position.x + " y " +
                                  hitCollider.transform.position.y + " GameObject:" + hitCollider);
                        }

                        startNode = hitCollider.gameObject;
                    }
                    else if (endNode == null)
                    {
                        selectorSprite = Instantiate(Resources.Load("Selector"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                        if (selectorSprite != null)
                        {
                            Vector3 position = selectorSprite.transform.position;
                            position.x = hitCollider.transform.position.x;
                            position.y = hitCollider.transform.position.y;
                            selectorSprite.transform.position = position;

                            Debug.Log("Node gevonden! Hit " + hitCollider.transform.name + " x" + hitCollider.transform.position.x + " y " +
                                  hitCollider.transform.position.y + " GameObject:" + hitCollider);
                        }

                        endNode = hitCollider.gameObject;
                    }
                    else
                    {
                        Debug.Log("Je hebt al een start- en eindpunt geselecteerd.");
                    }     
                }
                else
                {
                    Debug.Log("Geen node gevonden. Hit " + hitCollider.transform.name + " x" + hitCollider.transform.position.x + " y " +
                          hitCollider.transform.position.y);
                }
            }
        }
    }
}