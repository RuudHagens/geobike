using System;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0f;
    public Collider2D collider;
    private bool up;
    public bool player1;
	// Use this for initialization
	void Start () {
	    up = true;
	}

    //public float speed = 1.5f;

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
}
