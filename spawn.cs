using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        // Spawn balls when game is started (x, y)
        // spawn_ball(400, 200);
        // spawn_ball(400, -200);
        // spawn_ball(400, 0);
        // spawn_ball(-400, 200);
        // spawn_ball(-400, -200);
        spawn_ball(-400, 0);
        // spawn_ball(0, -200);
        // spawn_ball(0, 200);
        spawn_ball(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
    } 

    // Spawn ball method
    void spawn_ball(int x, int y){
        // Set random velocity of circle
        int vel_x = 100; //rnd.Next(-200, 200);
        int vel_y = 0; //rnd.Next(-200, 200);

        // Add circle to scene as ball and create rigidbody
        GameObject ball = Instantiate(circle, new Vector3(x, y), Quaternion.identity);
        ball.GetComponent<Rigidbody>().velocity = new Vector3(vel_x, vel_y);
    }
}
