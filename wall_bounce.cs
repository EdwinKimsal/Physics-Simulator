using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class wall_bounce : MonoBehaviour
{
    // Get border and wall objects
    public GameObject front;

    // Create private variables used in initilization
    private float mag_x;
    private float mag_y;
    private float ball_radius;


    // Start is called before the first frame update
    void Start()
    {
        // Get the magnitude the edge is from point 0
        mag_x = front.transform.localScale.x/2;
        mag_y = front.transform.localScale.y/2;

        // Get radius of ball
        ball_radius = transform.localScale.x/2;
    }

    // Update is called once per frame
    void Update()
    {
        // Set self Rigidbody as rb
        Rigidbody rb = GetComponent<Rigidbody>();

        // Get positions of self
        float pos_x = transform.position.x;
        float pos_y = transform.position.y;

        // Get velocities of self
        float vel_x = rb.velocity.x;
        float vel_y = rb.velocity.y;

        // Check if ball hit the vertical (x) wall
        if (((pos_x + ball_radius) >= mag_x && vel_x > 0) || ((pos_x - ball_radius) <= -mag_x && vel_x < 0))
        {
            rb.velocity = new Vector3(wall_hit(vel_x), vel_y);
        }

        // Check if ball hit the horizontal (y) wall
        if (((pos_y + ball_radius) >= mag_y && vel_y > 0) || ((pos_y - ball_radius) <= -mag_y && vel_y < 0))
        {
            rb.velocity = new Vector3(vel_x, wall_hit(vel_y));
        }
    }

    // Change velocity when ball hits a wall
    public float wall_hit(float vel)
    {
        return vel * -1;
    }
}
