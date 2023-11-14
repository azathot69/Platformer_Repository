using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

//Liebert, Jasper
//11/13/2023
//manages different collisions and location of the attack object

public class PlayerAttack : MonoBehaviour
{
    //Variables
    public GameObject playerPrefab;
    private Vector3 position;

    

    // Start is called before the first frame update
    void Start()
    {
        //so players can't always see the attack
        this.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //sets attacks position to where the player is 
        Vector3 playerPosition = playerPrefab.transform.position;
        transform.position = playerPosition;

    }

    /// <summary>
    /// when a players attack collides with something, find that objects tag and do the correct thing
    /// </summary>
    /// <param name="other">Trigger Collider</param>
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            default:

                break;

            //set enemy to inactive
            case "Enemy":
                other.gameObject.SetActive(false);
                //Debug.Log("Player hit an enemy!");
                break;

            //set spiked enemy to inactive
            case "Spike Enemy":
                other.gameObject.SetActive(false);
                //Debug.Log("Player hit an spiked enemy!");
                break;

            //kills the player when they try to attack a shield
            case "Shield":
                playerPrefab.GetComponent<PlayerControl>().Respawn();
                break;

            //Smashes the crate and spawns 5 wumps
            case "Crate":
                //Debug.Log("Crate");
                other.gameObject.GetComponent<Crate>().Smash(5);
                break;

        }
    }

}
