using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class center_of_mass : MonoBehaviour
{
    // Get circle array
    private GameObject[] circle_arr;
    private int number;

    // Start is called before the first frame update
    void Start()
    {
        circle_arr = GameObject.FindGameObjectsWithTag("circle");
        number = 5;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject circ in circle_arr)
        {
            Debug.Log(circ);
            Debug.Log(number);
        }
    }
}
