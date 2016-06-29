using UnityEngine;
using System.Collections;

public class PlaceNameLayer : MonoBehaviour {

	// Use this for initialization
    private void Start()
    {
        foreach (Transform t in GetComponentInChildren<Transform>())
        {
            t.gameObject.GetComponent<Renderer>().sortingLayerName = "Names";
        }
    }
}
