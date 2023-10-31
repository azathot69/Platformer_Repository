using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Liebert, Jasper
//10/24/2023
//This player will manage moving enemy models and will change their direction when hitting a wall

public class Enemy : MonoBehaviour
{
    public float speed;
    public bool goingRight = true;
    //public GameObject rightPos;
    //public GameObject leftPos;

    private Vector3 temp = Vector3.right;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.right, out hit, 1.3f)){
            if (hit.collider.tag == "Attack")
            {
                return;
            }
        }

        if (Physics.Raycast(transform.position, Vector3.right, out hit, 1.3f) && goingRight)
        {
            temp = Vector3.left;
            goingRight = false;
        }
        if (Physics.Raycast(transform.position, Vector3.left, out hit, 1.3f) && !goingRight)
        {
            temp = Vector3.right;
            goingRight = true;
        }
        //destroys enemy if a player 
        if (Physics.Raycast(transform.position, Vector3.up, out hit, 1.5f))
        {
            Debug.Log("Enemy Collision");
            if (hit.collider.name == "test_Player")
            {
                this.gameObject.SetActive(false);

            }
        }
       
        transform.position += temp * speed * Time.deltaTime;
    }

}
