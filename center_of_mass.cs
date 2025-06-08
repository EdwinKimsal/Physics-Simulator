using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class center_of_mass : MonoBehaviour
{
    // Create variable to see if center of mass is on or off
    public bool use_com;
    // Get circle array
    private GameObject[] circle_arr;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // If not using center of mass make it not visable
        if (use_com == false)
        {
            transform.localScale = new Vector2(0, 0);
        }
        // Else make is visable
        else
        {
            transform.localScale = new Vector2(25, 25);
        }
        
        // Array of all spawned balls
        circle_arr = GameObject.FindGameObjectsWithTag("circle");

        // Store length of array of balls
        float length = circle_arr.Length;

        // Set total x and y coordinates to zero initially
        float tot_x = 0;
        float tot_y = 0;

        // Iterate through each of the spawned balls
        foreach (GameObject circ in circle_arr)
        {
            // Add to total of x and y coordinates
            tot_x += circ.transform.position.x;
            tot_y += circ.transform.position.y;
        }

        // Calculate avg x and y coordinate (these are the coordinates for the center of mass)
        float avg_x = tot_x / length;
        float avg_y = tot_y / length;

        // Set pink circle to the center of mass coordinates
        transform.position = new Vector3(avg_x, avg_y, -1); // -1 on the z-axis to be ahead of the spawned balls
    }
}
