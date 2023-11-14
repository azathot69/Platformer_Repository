using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Acuna Joseph
// [11/7/23]
// Controls the movement of the Boogyman

public class Boogeyman : MonoBehaviour
{
    //Variables
    public bool startChase = false;
    public float speed;
    public Vector3 startingX;

    //Seconds before chase
    public int headStart = 5;

    //EZ Mode when caught before
    public int newHeadStart = 9;


    // Start is called before the first frame update
    void Start()
    {
        //Initialize position
        startingX = transform.position;

        //Start countdown
        StartCoroutine(StartRunning(headStart));
    }

    // Update is called once per frame
    void Update()
    {
        //"Chase" the player
        if (startChase)
        {
            //Begin Movement
            transform.position += Vector3.forward * speed * Time.deltaTime;

            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "test_Player")
        {
            ResetPosition();
        }
    }

    //Functions

    //Resets position when player is caught
    private void ResetPosition()
    {
        transform.position = startingX;
        startChase = false;
        StartCoroutine(StartRunning(newHeadStart));

    }

    //Seconds to wait before chasing the player
    IEnumerator StartRunning(int countDown)
    {
        yield return new WaitForSeconds(countDown);
        startChase = true;
    }
}
