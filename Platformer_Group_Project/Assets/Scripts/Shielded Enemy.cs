using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Liebert, Jasper
//10/24/2023
//This player will manage moving enemy models and will change their direction when hitting a wall

public class ShieldEnemy : MonoBehaviour
{
    public float bounce = 5.0f;
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
        if (Physics.Raycast(transform.position, Vector3.right, out hit, 0.5f) && goingRight && hit.collider.name != "test_Player" && hit.collider.name != "Attack")
        {
            temp = Vector3.left;
            goingRight = false;
        }
        if (Physics.Raycast(transform.position, Vector3.left, out hit, 0.5f) && !goingRight && hit.collider.name != "test_Player" && hit.collider.name != "Attack")
        {
            temp = Vector3.right;
            goingRight = true;
        }

        //destroys enemy if a player
     
        if (Physics.Raycast(transform.position, Vector3.up, out hit, 1.5f) && hit.collider.name == "test_Player")
        {
            Debug.Log("Enemy Collision");
            hit.collider.attachedRigidbody.AddForce(Vector3.up * bounce, ForceMode.Impulse);
            this.gameObject.SetActive(false);
        }
       
        transform.position += temp * speed * Time.deltaTime;
    }

}
