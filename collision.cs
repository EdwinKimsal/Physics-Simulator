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
        for (int i = 0; i < num_balls; i++)
        {
            for (int j = i+1; j < num_balls; j++)
            {
                // Get position of balls in arr
                int first_ball = i;
                int second_ball = j;

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
                        detected_collision(circle_arr[first_ball], circle_arr[second_ball]);
                    }
                }
            }
        }
    }


    // Function to see if two objects are heading towards eachother
    bool towards(GameObject first_ball, GameObject second_ball)
    {
        // Set objects as rigidbodies
        Rigidbody obj1 = first_ball.GetComponent<Rigidbody>();
        Rigidbody obj2 = second_ball.GetComponent<Rigidbody>();

        // Calculate velocity difference and displacement difference
        Vector2 vel_diff = new Vector2(second_ball.transform.position.x - first_ball.transform.position.x, second_ball.transform.position.y - first_ball.transform.position.y);
        Vector2 displ_diff = obj2.velocity - obj1.velocity;

        // Calculate dot product
        Vector2 dot_prod = vel_diff * displ_diff;

        // If any of the values are negative in the dot product, the objects are moving towards eachother
        if (dot_prod.x < 0 || dot_prod.y < 0)
        {
            return true;
        }

        // Else, they are moving away
        return false;
    }

    // Function when collisions are detected
    void detected_collision(GameObject first_ball, GameObject second_ball)
    {
        // Get masses of objects in collision (Work in Progress)
        double mass1 = calc_mass(first_ball);
        double mass2 = calc_mass(second_ball);

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
        double phi = calc_angle(delta_y, delta_x);

        // Calculate sigma (based on theta1 and phi, the rotated axis)
        double sigma1 = theta1 - phi;
        double sigma2 = theta2 - phi;

        // Calculate magnitude of velocities
        double vel1 = Math.Sqrt(Math.Pow(obj1.velocity.x, 2) + Math.Pow(obj1.velocity.y, 2));
        double vel2 = Math.Sqrt(Math.Pow(obj2.velocity.x, 2) + Math.Pow(obj2.velocity.y, 2));

        // Calculate final rotated velocities looking at the collision in 1D
        double v1fxr = (2 * mass2 * vel2 * Math.Cos(sigma2) + vel1 * (mass1 - mass2) * Math.Cos(sigma1)) / (mass1 + mass2);
        double v2fxr = (2 * mass1 * vel1 * Math.Cos(sigma1) + vel2 * (mass2 - mass1) * Math.Cos(sigma2)) / (mass2 + mass1);

        // Calculate final velocity components for collision by rotating the collision back
        double v1fx = v1fxr * Math.Cos(phi) + vel1 * Math.Sin(sigma1) * Math.Cos(phi + Math.PI / 2);
        double v1fy = v1fxr * Math.Sin(phi) + vel1 * Math.Sin(sigma1) * Math.Sin(phi + Math.PI / 2);
        double v2fx = v2fxr * Math.Cos(phi) + vel2 * Math.Sin(sigma2) * Math.Cos(phi + Math.PI / 2);
        double v2fy = v2fxr * Math.Sin(phi) + vel2 * Math.Sin(sigma2) * Math.Sin(phi + Math.PI / 2);

        // Set velocities to objects
        obj1.velocity = new Vector3((float)v1fx, (float)v1fy);
        obj2.velocity = new Vector3((float)v2fx, (float)v2fy);
    }

    // Function to calculate mass
    double calc_mass(GameObject obj)
    {
        return Math.PI * Math.Pow(obj.transform.localScale.x / 20, 2);
    }

    // Function to calcualte angle
    double calc_angle(float numer, float denom)
    {
        // Rare cases (when denomenator is 0)
        if (denom == 0)
        {
            if (numer > 0)
            {
                return Math.PI / 2;
            }

            else if (numer < 0)
            {
                return -Math.PI / 2;
            }

            else
            {
                return Math.PI;
            }
        }

        // Return the tangent of numer/denom plus 180 deg (3.14 rad) if only in quadrent 2 or 3
        else if (denom < 0)
        {
            return Math.Atan(numer / denom) + Math.PI;
        }

        // Else angle is numer divided by denom
        else
        {
            return Math.Atan(numer / denom);
        }
    }
}