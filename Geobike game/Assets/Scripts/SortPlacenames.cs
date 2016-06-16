using UnityEngine;
using System.Collections;

public class SortPlacenames : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().sortingLayerName = "Names";
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
