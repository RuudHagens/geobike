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

	// Use this for initialization
	void Start ()
    {
        StartTimer();

        Camera.main.GetComponent<AudioSource>().PlayOneShot(select, 1.0f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (StaticObjects.winningPlayer == 0)
        {
            if (player1.GetComponent<PlayerMovementPerPlayer>().done && !player2.GetComponent<PlayerMovementPerPlayer>().done)
            {
                StaticObjects.winningPlayer = 1;
            }
            else if(!player1.GetComponent<PlayerMovementPerPlayer>().done && player2.GetComponent<PlayerMovementPerPlayer>().done)
            {
                StaticObjects.winningPlayer = 2;
            }
        }

	    if(player1.GetComponent<PlayerMovementPerPlayer>().done && player2.GetComponent<PlayerMovementPerPlayer>().done)
        {
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


            if(!player1.GetComponent<PlayerMovementPerPlayer>().done)
            {
                timep1.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            }

            if (!player2.GetComponent<PlayerMovementPerPlayer>().done)
            {
                timep2.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            }    
        }
        else
        {
            timep1.text = "00:00";
            timep2.text = "00:00";
            SceneManager.LoadScene("end scene");
        }
    }
}
