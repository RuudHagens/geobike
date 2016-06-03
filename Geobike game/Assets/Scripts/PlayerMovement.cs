using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0f;
    public Collider2D collider;
    private Rigidbody2D rb;

    private float presscounter;
    public Text lblSpeed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("CalculateSpeed", 0, 5);
	}

    //public float speed = 1.5f;

    void Update()
    {
        if (speed > 0f)
        {
            speed = speed - 0.05f;
        }

        if (speed < 0f)
        {
            speed = 0f;
        }

        transform.position += Vector3.up * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            speed = speed + 0.15f;
            presscounter++;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("Pick Up"))
        //{
        //    other.gameObject.SetActive(false);
        //    count = count + 1;
        //    SetCountText();
        //}

        Quaternion quat = transform.rotation;
        quat.z = 180;
        transform.rotation = quat;
    }

    public void CalculateSpeed()
    {
        float rpm = presscounter * 12;
        Debug.Log(rpm);
        float mh = 1.5f * rpm * 0.10472f;
        float kmh = mh * 3.6f;

        lblSpeed.text = "Snelheid: " + kmh;
        presscounter = 0;
    }
}
