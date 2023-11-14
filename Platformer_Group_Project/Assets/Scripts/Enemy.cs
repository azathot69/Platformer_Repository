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
    public bool falling = false;
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

        //Turn around when near cliff
        RaycastHit ground;
        if (Physics.Raycast(transform.position, Vector3.down, out ground, 3f))
        {
            //Turn around when wall is hit
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
        }
        else
        {
            StartCoroutine(FallingEnemy());
            if (goingRight && !falling)
            {

                temp = Vector3.left;
                goingRight = false;
            }
            else if (!goingRight && !falling)
            {

                temp = Vector3.right;
                goingRight = true;
            }
            else
            {
                temp = Vector3.down;
                if (transform.position.y <= -20)
                {
                    this.gameObject.SetActive(false);
                }
            }    

        }

        transform.position += temp * speed * Time.deltaTime;
    }

    IEnumerator FallingEnemy()
    {
        RaycastHit ground;
        Debug.Log("yup");
        yield return new WaitForSeconds(1);
        if (!Physics.Raycast(transform.position, Vector3.down, out ground, 3f))
        {
            falling = true;
        }
    }
}
