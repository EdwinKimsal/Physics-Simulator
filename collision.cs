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
                    detected_collision(first_ball, second_ball);
                }
            }
        }
    }

    // Function when collisions are detected
    void detected_collision(int first_ball, int second_ball)
    {
        Debug.Log("Hit! Balls: " + first_ball + " & " + second_ball);
    }
}
