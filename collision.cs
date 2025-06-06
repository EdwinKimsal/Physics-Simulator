using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class collision : MonoBehaviour
{
    // Initialize array of spawned balls
    private GameObject[] circle_arr;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Get array of all spawned balls
        circle_arr = GameObject.FindGameObjectsWithTag("circle");

        // Get number of spawned balls
        int num_balls = circle_arr.Length;

        // Iterate through each possible pair of spawned balls
        for (int i = 0; i < (num_balls / 2); i++)
        {
            for (int j = 1; j < num_balls - i; j++)
            {
                // Get position of balls in arr
                int first_ball = i;
                int second_ball = i + j;

                // Get coordinates of balls
                float pos_one_x = circle_arr[first_ball].transform.position.x;
                float pos_one_y = circle_arr[first_ball].transform.position.y;
                float pos_two_x = circle_arr[second_ball].transform.position.x;
                float pos_two_y = circle_arr[second_ball].transform.position.y;

                // Calculate distance between two balls
                float delta_x = Math.Abs(pos_one_x - pos_two_x);
                float delta_y = Math.Abs(pos_one_y - pos_two_y);
                double dist = Math.Sqrt(Math.Pow(delta_x, 2) + Math.Pow(delta_y, 2));

                // Calculate min distance needed
                float min_dist = circle_arr[first_ball].transform.localScale.x / 2 + circle_arr[second_ball].transform.localScale.x / 2;

                // Check if the collision occurred
                if (dist <= min_dist)
                {
                    // Check if the objects are heading towards eachother
                    if (towards(circle_arr[first_ball], circle_arr[second_ball]) == true)
                    {
                        // Make the first ball the highest ball in terms of y
                        if (circle_arr[first_ball].transform.position.y > circle_arr[second_ball].transform.position.y)
                        {
                            detected_collision(circle_arr[first_ball], circle_arr[second_ball]);
                        }
                        else
                        {
                            detected_collision(circle_arr[second_ball], circle_arr[first_ball]);
                        }
                    }
                }
            }
        }
    }


    // Function to see if two objects are heading towards eachother
    bool towards(GameObject first_ball, GameObject second_ball)
    {
        // // Set objects as rigidbodies
        // Rigidbody obj1 = first_ball.GetComponent<Rigidbody>();
        // Rigidbody obj2 = second_ball.GetComponent<Rigidbody>();

        // // Calculate distance between two objects
        // double delta_x = first_ball.transform.position.x - second_ball.transform.position.x;
        // double delta_y = first_ball.transform.position.y - second_ball.transform.position.y;
        // double dist = Math.Sqrt(Math.Pow(delta_x, 2) + Math.Pow(delta_y, 2));

        // // Calculate slope distance (the distance with direction of slope)
        // double slope_dist = dist * Mathf.Sign(((float)delta_x * (float)delta_y) + ((float)delta_x + (float)delta_y) / 1000000000);

        // // Calculate change in velocities of two objects
        // double v_delta_x = obj1.velocity.x - obj2.velocity.x;
        // double v_delta_y = obj1.velocity.y - obj2.velocity.y;
        // double v_delta = Math.Sqrt(Math.Pow(v_delta_x, 2) + Math.Pow(v_delta_y, 2));

        // // Calculate theta
        // double theta = Math.Atan((v_delta_y - delta_y) / (v_delta_x - delta_x)) + extra_degrees(first_ball, obj1, second_ball, obj2);

        // // Heading towards if theta is less than or equal to 90 degrees
        // if (theta < 180)
        // {
        //     return true;
        // }

        // // Else, not heading towards eachother
        // else
        // {
        //     return false;
        // }

        return true;
    }

    // // Function to add extra degrees, if needed, to a calculated theta in the towards function
    // int extra_degrees(GameObject first_ball, Rigidbody obj1, GameObject second_ball, Rigidbody obj2)
    // {
    //     // In Quadrent 1 or 4
    //     if ((first_ball.transform.position.x - second_ball.transform.position.x) * (obj1.velocity.x - obj2.velocity.x) >= 0)
    //     {
    //         return 0;
    //     }

    //     // In Quadrent 2 or 3
    //     else
    //     {
    //         return 180;
    //     }
    // }

    // Function when collisions are detected
    void detected_collision(GameObject first_ball, GameObject second_ball)
    {
        // Get masses of objects in collision (Work in Progress)
        float mass1 = 1;
        float mass2 = 1;

        // Set objects as rigidbodies
        Rigidbody obj1 = first_ball.GetComponent<Rigidbody>();
        Rigidbody obj2 = second_ball.GetComponent<Rigidbody>();

        // Calculate thetas (based on velocities)
        double theta1 = calc_angle(obj1.velocity.y, obj1.velocity.x);
        double theta2 = calc_angle(obj2.velocity.y, obj2.velocity.x);

        // Get coordinates of balls
        float pos_one_x = first_ball.transform.position.x;
        float pos_one_y = first_ball.transform.position.y;
        float pos_two_x = second_ball.transform.position.x;
        float pos_two_y = second_ball.transform.position.y;

        // Calculate distance between two balls
        float delta_x = pos_two_x - pos_one_x;
        float delta_y = pos_two_y - pos_one_y;

        // Calculate phi (based on positions of two balls)
        double phi1 = calc_angle(delta_y, delta_x);
        double phi2 = -1 * phi1;

        // Calculate sigma (based on theta1 and phi, the rotated axis)
        double sigma1 = theta1 - phi1;
        double sigma2 = theta2 - phi2;

        Debug.Log(theta1);
        Debug.Log(phi1);
        Debug.Log(sigma1);

        // Calculate magnitude of velocities
        double vel1 = Math.Sqrt(Math.Pow(obj1.velocity.x, 2) + Math.Pow(obj1.velocity.y, 2)) * Mathf.Sign((obj1.velocity.x * obj1.velocity.y) + (obj1.velocity.x + obj1.velocity.y) / 1000000000);
        double vel2 = Math.Sqrt(Math.Pow(obj2.velocity.x, 2) + Math.Pow(obj2.velocity.y, 2)) * Mathf.Sign(obj2.velocity.x * obj2.velocity.y + (obj2.velocity.x + obj2.velocity.y) / 1000000000);

        // Calculate final velocities looking at the collision in 1D
        double v1f = (2 * mass2 * vel2 + vel1 * (mass1 - mass2)) / (mass1 + mass2);
        double v2f = (2 * mass1 * vel1 + vel2 * (mass2 - mass1)) / (mass2 + mass1);

        // Calculate the final x-direction velocities on the rotated axis
        double v1fxr = v1f * Math.Cos(sigma1);
        double v2fxr = v2f * Math.Cos(sigma2);

        // Calculate final velocity components for collision by rotating the collision back
        double v1fx = v1fxr * Math.Cos(phi1) + vel1 * Math.Sin(sigma1) * Math.Cos(phi1 + Math.PI/2);
        double v1fy = v1fxr * Math.Sin(phi1) + vel1 * Math.Sin(sigma1) * Math.Sin(phi1 + Math.PI/2);
        double v2fx = v2fxr * Math.Cos(phi2) + vel2 * Math.Sin(sigma2) * Math.Cos(phi2 + Math.PI/2);
        double v2fy = v2fxr * Math.Sin(phi2) + vel2 * Math.Sin(sigma2) * Math.Sin(phi2 + Math.PI/2);

        // Set velocities to objects
        obj1.velocity = new Vector3((float)v1fx, (float)v1fy);
        obj2.velocity = new Vector3((float)v2fx, (float)v2fy);

        // TEST
        Debug.Log(v1fx + ", " + v1fy);
        Debug.Log(v2fx + ", " + v2fy);
    }

    // Function to calcualte angle
    double calc_angle(float numer, float denom)
    {
        // Rare cases (when denomenator is 0)
        if (denom == 0)
        {
            if (numer > 0)
            {
                return Math.PI/2;
            }

            else if (numer < 0)
            {
                return -Math.PI/2;
            }

            else
            {
                return Math.PI;
            }
        }

        // Non-rare cases
        else
        {
            return Math.Tan(numer/denom);
        }
    }
}