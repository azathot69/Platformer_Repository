using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Liebert, Jasper
//10/31/2023
//this script will move the crate
public class Crate : MonoBehaviour
{
    Vector3 temp = Vector3.up;

    private float topY;
    private float bottomY;
    public bool goingUp = true;
    public float crateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        topY = transform.position.y + 1;
        bottomY = transform.position.y - 1;
    }

    // Update is called once per frame
    void Update()
    {
        Wiggle();
    }

    public void Wiggle()
    {
        if (goingUp)
        {
            if (transform.position.y >= topY)
            {
                temp = Vector3.down;
                goingUp = false;
            }
        }
        else
        {
            if (transform.position.y <= bottomY)
            {
                temp = Vector3.up;
                goingUp = true;
            }
        }
        transform.position += temp * crateSpeed * Time.deltaTime;
    }
}
