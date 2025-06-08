using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class spawn : MonoBehaviour
{
    // Create public class to spawn obj
    public GameObject circle;

    // Random class
    Random rnd = new Random();

    // Start is called before the first frame update
    void Start()
    {
        // Set dictionary of possible types of balls
        Dictionary<int, Tuple<int, string>> types = new Dictionary<int, Tuple<int, string>>();
        types.Add(0, Tuple.Create(60, "#ff9da6"));
        types.Add(1, Tuple.Create(65, "#FFDA9D"));
        types.Add(2, Tuple.Create(70, "#FFFC9D"));
        types.Add(3, Tuple.Create(75, "#9dffff"));
        types.Add(4, Tuple.Create(80, "#9dffff"));

        // Spawn balls when game is started (x, y)
        spawn_ball(400, 200, types);
        spawn_ball(400, -200, types);
        spawn_ball(400, 0, types);
        spawn_ball(-400, 200, types);
        spawn_ball(-400, -200, types);
        spawn_ball(-400, 0, types);
        spawn_ball(0, -200, types);
        spawn_ball(0, 200, types);
        spawn_ball(0, 0, types);
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Spawn ball method
    void spawn_ball(int x, int y, Dictionary<int, Tuple<int, string>> dict)
    {
        // Set random type of ball
        int key = rnd.Next(0, 4);

        // Set random velocity of circle
        int vel_x = rnd.Next(-200, 201);
        int vel_y = rnd.Next(-200, 201);

        // Add circle to scene as ball and create rigidbody
        GameObject ball = Instantiate(circle, new Vector3(x, y), Quaternion.identity);
        ball.GetComponent<Rigidbody>().velocity = new Vector3(vel_x, vel_y);

        // Set color and size
        // ball.GetComponent<Renderer>().material.color = (Color) ColorConverter.ConvertFromString(dict[key].Item2);
        ball.transform.localScale = new Vector2(dict[key].Item1, dict[key].Item1);
    }
}
