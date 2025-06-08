using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class spawn : MonoBehaviour
{
    // Create public class to spawn obj
    public GameObject circle;

    // Create public class of front
    public GameObject front;

    // Random class
    Random rnd = new Random();

    // Initialize array of spawned balls
    private GameObject[] circle_arr;

    // Public variable of number of balls
    public int num_balls;

    // Set pos vars and bool
    private int x_pos;
    private int y_pos;

    // Start is called before the first frame update
    void Start()
    {
        // Set dictionary of possible types of balls
        Dictionary<int, Tuple<int, Color32>> types = new Dictionary<int, Tuple<int, Color32>>();
        types.Add(0, Tuple.Create(60, new Color32(255, 94, 94, 255)));
        types.Add(1, Tuple.Create(70, new Color32(255, 197, 94, 255)));
        types.Add(2, Tuple.Create(80, new Color32(255, 255, 94, 255)));
        types.Add(3, Tuple.Create(90, new Color32(94, 255, 110, 255)));
        types.Add(4, Tuple.Create(100, new Color32(138, 255, 244, 255)));

        // Iterate for each of the number of balls
        for (int i = 0; i < num_balls; i++)
        {
            // is_valid is, set is_valid to true by default
            bool is_valid = false;

            // Find a working coord until valid
            while (is_valid == false)
            {
                // Set margin
                int margin = 30; // Used for boundry between obj

                // Find a random point
                x_pos = rnd.Next(-1 * (int)front.transform.localScale.x / 2 + types[4].Item1/2 + margin, (int)front.transform.localScale.x / 2 + 1 - types[4].Item1/2 - margin);
                y_pos = rnd.Next(-1 * (int)front.transform.localScale.y / 2 + types[4].Item1/2 + margin, (int)front.transform.localScale.y / 2 + 1 - types[4].Item1/2 - margin);

                // If no obj, it must be false
                if (circle_arr == null)
                {
                    is_valid = true;
                }

                // Else contunue
                else
                {
                    // Set is_true
                    int is_true = 1;

                    // Iterate through each circle already created
                    foreach (GameObject obj in circle_arr)
                    {
                        // Get point of this obj
                        int x_other_obj = (int)obj.transform.position.x;
                        int y_other_obj = (int)obj.transform.position.y;

                        // Make sure new point is valid
                        if (Math.Abs(x_pos - x_other_obj) <= types[4].Item1/2 + obj.transform.localScale.x/2 + margin && Math.Abs(y_pos - y_other_obj) <= types[4].Item1/2 + obj.transform.localScale.y/2 + margin)
                        {
                            is_true = 0;
                        }
                    }

                    if (is_true == 1)
                    {
                        is_valid = true;
                    }
                }
            }

            // Spawn a ball
            spawn_ball(x_pos, y_pos, types); // (x-coor, y-coor, dict of types)

            // Get array of all spawned balls
            circle_arr = GameObject.FindGameObjectsWithTag("circle");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Spawn ball method
    void spawn_ball(int x, int y, Dictionary<int, Tuple<int, Color32>> dict)
    {
        // Set random type of ball
        int key = rnd.Next(0, 5);

        // Set random velocity of circle
        int vel_x = rnd.Next(-150, 151);
        int vel_y = rnd.Next(-150, 151);

        // Add circle to scene as ball and create rigidbody
        GameObject ball = Instantiate(circle, new Vector3(x, y), Quaternion.identity);
        ball.GetComponent<Rigidbody>().velocity = new Vector3(vel_x, vel_y);

        // Set color and size
        ball.GetComponent<Renderer>().material.color = dict[key].Item2;
        ball.transform.localScale = new Vector2(dict[key].Item1, dict[key].Item1);
    }
}
