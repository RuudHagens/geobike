using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0f;
    public float speedmult = 0.15f;
    public Collider2D collider;
    private bool up;
    public bool player1;
    public GameObject selectedNodePlayer1;

    private float pressesp1;
    private float pressesp2;

    public Text speedp1;
    public Text speedp2;
    // Use this for initialization
    void Start () {
	    up = true;
        InvokeRepeating("CalculateSpeed", 0, 2);
        selectedNodePlayer1 = GameObject.Find("Location-Almelo 1");
        transform.position = selectedNodePlayer1.transform.position;
        selectedNodePlayer1 = GameObject.Find("Location-Rotterdam 1");
    }

    void Update()
    {
        if (player1)
        {
            //if (up)
            //{
                if (speed > 0f)
                {
                    speed = speed - 0.05f;
                }

                if (speed < 0f)
                {
                    speed = 0f;
                }

            //Quaternion rotation = Quaternion.LookRotation
            //    (selectedNodePlayer1.transform.position - transform.position, transform.TransformDirection(Vector3.up));
            //transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
            //rotation = new Quaternion(0, 0, rotation.z, rotation.w);
            //Debug.Log(selectedNodePlayer1.name);

            transform.position += (selectedNodePlayer1.transform.position - transform.position).normalized*speed*Time.deltaTime*10;
            //transform.LookAt(selectedNodePlayer1.transform);

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    transform.position += Vector3.left*speed*Time.deltaTime * 10;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    transform.position += Vector3.right*speed*Time.deltaTime * 10;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    speed = speed + speedmult;
                    pressesp1++;               
                }
            //}
            //else
            //{
            //    if (speed < 0f)
            //    {
            //        speed = speed + 0.05f;
            //    }

            //    if (speed > 0f)
            //    {
            //        speed = 0f;
            //    }

            //    transform.position += Vector3.up*speed*Time.deltaTime * 10;

            //    if (Input.GetKeyDown(KeyCode.RightArrow))
            //    {
            //        transform.position += Vector3.left*speed*Time.deltaTime * 10;
            //    }
            //    if (Input.GetKeyDown(KeyCode.LeftArrow))
            //    {
            //        transform.position += Vector3.right*speed*Time.deltaTime * 10;
            //    }
            //    if (Input.GetKeyDown(KeyCode.Space))
            //    {
            //        speed = speed - 0.15f;
            //        pressesp1++;
            //    }
            //}
        }
        else
        {
            if (up)
            {
                if (speed > 0f)
                {
                    speed = speed - 0.05f;
                }

                if (speed < 0f)
                {
                    speed = 0f;
                }

                transform.position += Vector3.up * speed * Time.deltaTime * 10;

                if (Input.GetKeyDown(KeyCode.A))
                {
                    transform.position += Vector3.left * speed * Time.deltaTime * 10;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    transform.position += Vector3.right * speed * Time.deltaTime * 10;
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    speed = speed + speedmult;
                    pressesp2++;
                }
            }
            else
            {
                if (speed < 0f)
                {
                    speed = speed + 0.05f;
                }

                if (speed > 0f)
                {
                    speed = 0f;
                }

                transform.position += Vector3.up * speed * Time.deltaTime*10;

                if (Input.GetKeyDown(KeyCode.D))
                {
                    transform.position += Vector3.left * speed * Time.deltaTime * 10;
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    transform.position += Vector3.right * speed * Time.deltaTime * 10;
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    speed = speed - speedmult;
                    pressesp2++;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collider"))
        {
            if (up)
            {
                up = false;
            }
            else
            {
                up = true;
            }
        } 
    }

    public void CalculateSpeed()
    {

        if (player1)
        {
            float rpm1 = pressesp1 * 30;
            float ms1 = 0.45f * rpm1 * 0.10472f;
            float kmh1 = ms1 * 3.6f;
            speedp1.text = Mathf.Round(kmh1 * 10) / 10 + "KM/h";
            Debug.Log("speed p1 spekkoen: " + kmh1);
            pressesp1 = 0;
        }
        else
        {
            float rpm2 = pressesp2 * 30;
            float ms2 = 0.45f * rpm2 * 0.10472f;
            float kmh2 = ms2 * 3.6f;
            speedp2.text = Mathf.Round(kmh2 * 10) / 10 + "KM/h";
            Debug.Log("speed p2 scharnier: " + kmh2);
            pressesp2 = 0;
        }
    }
}

//rpm 90
//omtrek m = 2 * pi * radius
//tijd in km/h = rpm * omtrek / 60 * 3.6
