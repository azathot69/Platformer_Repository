using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Acuna Joseph
 * [10/24/23]
 * Controls the player's movement
 * Controls the player's attack
 */

public class PlayerControl : MonoBehaviour
{

    //Variables
    private int lives = 3;
    private int points = 0;

    private bool attacking = false;
    private float attackRate = 1.3f;
    private float deathFall = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check input for movement

        //Check input for Jump

        //Check input for attack

        //Check for pitfall
    }

    //Functions

    /// <summary>
    /// Handles the player's attack
    /// </summary>
    private void Attack()
    {
        if (attacking == false)
        {
            attacking = true;
            Debug.Log("Player is attacking");

            //Insert coroutine for attack cooldown
        }
    }

    /// <summary>
    /// Handles the player's movement
    /// </summary>
    private void Movement()
    {

    }

    /// <summary>
    /// Handles the player's jump
    /// </summary>
    private void HandleJump()
    {

    }

    /// <summary>
    /// MaMakes the player lose a life.
    /// If there is no lives left, perish
    /// </summary>
    private void Hurt()
    {
        if (lives != 0)
        {
            lives--;
            
            //Return to last spawn point
        }
        else
        {
            Debug.Log("The player has died");
        }
    }
}
