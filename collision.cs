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
        for (int i = 0; i < (num_balls/2); i++)
        {
            for (int j = 1; j < num_balls-i; j++)
            {
                int second_ball = i + j;
                Debug.Log(i + ", " + second_ball);
            }
        }
    }
}
