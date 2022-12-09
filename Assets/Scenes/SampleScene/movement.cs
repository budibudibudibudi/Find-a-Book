using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float deceleration;
    public float topSpeed;
    public Vector3 direction;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        { 
            AccelerateForward();
            this.transform.position += direction * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            Break();
            this.transform.position += direction * speed * Time.deltaTime;
        }
        

    }

    void AccelerateForward()
    {
        speed += acceleration * Time.deltaTime;
        speed = Mathf.Clamp(speed, 0, topSpeed);
        direction = Vector3.forward;
    }

    void Break()
    {
        speed -= deceleration * Time.deltaTime;
        speed = Mathf.Max(speed, 5);
        direction = Vector3.back;
    }

}