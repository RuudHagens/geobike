using UnityEngine;
using System.Collections;

public class PlaceNameLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        foreach(Transform t in GetComponentInChildren<Transform>())
        {
            t.gameObject.GetComponent<Renderer>().sortingLayerName = "Names";
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
