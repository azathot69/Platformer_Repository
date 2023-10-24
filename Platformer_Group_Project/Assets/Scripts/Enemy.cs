using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Liebert, Jasper
//10/24/2023
//This player will manage moving enemy models and will change their direction when hitting a wall

public class Enemy : MonoBehaviour
{
    public float speed;
    public bool goingRight = true;
    public GameObject rightPos;
    public GameObject leftPos;

    private Vector3 temp = Vector3.right;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (goingRight)
        {
            if (transform.position.x >= rightPos.transform.position.x)
            {
                temp = Vector3.left;
                goingRight = false;
            }
        }
        else
        {
            if (transform.position.x <= leftPos.transform.position.x)
            {
                temp = Vector3.right;
                goingRight = true;
            }
        }
        transform.position += temp * speed * Time.deltaTime;
    }

}
