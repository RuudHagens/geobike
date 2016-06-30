using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{

    private float timer = 0.0f;
    public Text timep1;
    public Text timep2;
    private bool timeron = false;

    public GameObject player1;
    public GameObject player2;

    public AudioClip select;
    public float minutes { get; set; }
    public float seconds { get; set; }

    public void StartTimer()
    {
        timer = 300.0f;
        timeron = true;
    }

    public void StopTimer()
    {
        string finaltime = timer.ToString();
        timeron = false;
        Debug.Log(finaltime);
    }

	// Use this for initialization
	void Start ()
    {
        StartTimer();
        
        GUImanager.instance.SetAssignmentText();

        Camera.main.GetComponent<AudioSource>().PlayOneShot(select, 1.0f);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(player1.GetComponent<PlayerMovementPerPlayer>().done && player2.GetComponent<PlayerMovementPerPlayer>().done)
        {
            StopTimer();
            SceneManager.LoadScene("end scene");
        }
	}

    void FixedUpdate()
    {
        if (timer > 0 && timeron)
        {
            timer -= Time.deltaTime;
            minutes = Mathf.Floor(timer / 60);
            seconds = Mathf.Floor(timer % 60);
            timep1.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            timep2.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        else
        {
            timep1.text = "00:00";
            timep2.text = "00:00";
            SceneManager.LoadScene("end scene");
        }
    }
}
