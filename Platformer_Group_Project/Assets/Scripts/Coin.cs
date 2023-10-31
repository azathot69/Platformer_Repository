using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Liebert, Jasper
//10/31/2023
//This script will rotate the coin

public class Coin : MonoBehaviour
{
    private float rotateSpeed = 40;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime, 0.0f, 0.0f);
    }
}
