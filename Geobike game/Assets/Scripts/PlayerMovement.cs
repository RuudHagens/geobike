using System;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0f;
    public Collider2D collider;
    private bool up;
    public bool player1;

    private float pressesp1;
    private float pressesp2;
    // Use this for initialization
    void Start () {
	    up = true;
        InvokeRepeating("CalculateSpeed", 0, 5);
	}

    void Update()
    {
        if (player1)
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

                transform.position += Vector3.up*speed*Time.deltaTime;

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.position += Vector3.left*speed*Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.position += Vector3.right*speed*Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    speed = speed + 0.15f;
                    pressesp1++;
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

                transform.position += Vector3.up*speed*Time.deltaTime;

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.position += Vector3.left*speed*Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.position += Vector3.right*speed*Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    speed = speed - 0.15f;
                    pressesp1++;
                }
            }
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

                transform.position += Vector3.up * speed * Time.deltaTime;

                if (Input.GetKey(KeyCode.A))
                {
                    transform.position += Vector3.left * speed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.position += Vector3.right * speed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    speed = speed + 0.15f;
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

                transform.position += Vector3.up * speed * Time.deltaTime;

                if (Input.GetKey(KeyCode.D))
                {
                    transform.position += Vector3.left * speed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.position += Vector3.right * speed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    speed = speed - 0.15f;
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
            float rpm1 = pressesp1 * 12;
            float mh1 = 1.5f * rpm1 * 0.10472f;
            float kmh1 = mh1 * 3.6f;
            Debug.Log("speed p1 spekkoen: " + kmh1);
            pressesp1 = 0;
        }
        
        if (!player1)
        {
            float rpm2 = pressesp2 * 12;
            float mh2 = 1.5f * rpm2 * 0.10472f;
            float kmh2 = mh2 * 3.6f;
            Debug.Log("speed p2 scharnier: " + kmh2);
            pressesp2 = 0;
        }
    }
}
