using UnityEngine;
using System.Collections;

public class MapSceneScript : MonoBehaviour
{
    public AudioClip click;

	// Use this for initialization
	void Start () {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(click, 1.0f);
    }
}
