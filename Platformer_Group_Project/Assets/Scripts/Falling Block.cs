using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Liebert, Jasper
//11/02/2023
//This script will detect when a player lands on a falling platform, starts a timer then falls until it destroys itself

public class FallingBlock : MonoBehaviour
{
    //variables
    Vector3 fallDirection = Vector3.down;
    public float speed;
    public float deathYLevel;
    public float waitTime;
    private bool falling = false;


    void Update()
    {
        Fall();
    }

    /// <summary>
    /// starts the fall timer when a collider hits the falling platform
    /// </summary>
    /// <param name="collision"> collider (probably a player) </param>
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("collidedd" + falling);
        StartCoroutine(FallTimer());
    }

    /// <summary>
    /// If "falling" is true, move falling platform down until it hits the death y level
    /// </summary>
    public void Fall()
    {
        if (falling)
        {
            transform.position += fallDirection * speed * Time.deltaTime;
            if (transform.position.y <= deathYLevel)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// waits for 5 seconds, sets "falling" to true
    /// </summary>
    /// <returns> wait time </returns>
    IEnumerator FallTimer()
    {
        yield return new WaitForSeconds(waitTime);
        falling = true;
        //Debug.Log(falling);
    }
}
