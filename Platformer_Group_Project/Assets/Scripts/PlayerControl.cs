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
    public int lives = 3;
    public int points = 0;
    public int speed = 5;
    private Rigidbody rigidbodyRef;
    public float jumpForce = 10f;
    public float deathYLevel = -2;

    private bool attacking = false;
    private float attackRate = 1.3f;

    public Vector3 startingX;
    public Vector3 startingY;
    //jaspers additions
    public Vector3 spawnPoint;
    public GameObject tempPortal;

    // Start is called before the first frame update
    void Start()
    {
        //Get rigidbody component off of the object & stores a reference to it
        rigidbodyRef = GetComponent<Rigidbody>();

        //Store object's X coordinates when scene starts
        startingX = transform.position;
        startingY = transform.position;
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Check input for movement
        Movement();
        //Check input for Jump

        //Check input for attack

        //Check for pitfall
        if (transform.position.y <= deathYLevel)
        {
            Respawn();
        }
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
        //Movement
        if (Input.GetKey(KeyCode.A) || Input.GetKey("left"))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey("right"))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey("up"))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey("down"))
        {
            transform.position -= Vector3.forward * speed * Time.deltaTime;
        }

        //Controls
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleJump();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Now Attacking");
        }
    }

    /// <summary>
    /// Handles the player's jump
    /// </summary>
    private void HandleJump()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.3f))
        {
            Debug.Log("Player is touching the ground. Can Jump");
            rigidbodyRef.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Makes the player lose a life.
    /// If there is no lives left, perish
    /// </summary>
    private void Respawn()
    {
        //I rewrote the respawn script because it wasn't telling me when I died
        lives--;
        transform.position = spawnPoint;
        if (lives <= 0)
        {
            Debug.Log("Player Died");
            //swithces scene to game over
            //EndScreen.SceneSwitch(1);
            //just moves player back (for testing)
            transform.position = spawnPoint;
        }

        //this wasn't letting me die until I was at -1 lives, sometimes not at all
        /*if (lives != 0)
        {
            lives--;

            //Return to last spawn point
            transform.position = startingX;
            transform.position = startingY;
            if 
        }
        else
        {
            Debug.Log("The player has died");
            //sceneManager.SceneSwitch(1);
            //Replace below code with proper Game Over Scene
            transform.position = startingX;
            transform.position = startingY;
        }*/

    }

    /// <summary>
    /// Deals with collision
    /// </summary>
    /// <param name="other">The object being collided with</param>
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            default:
                Debug.Log("If you see this, let me know - Joseph");
                break;

            case "Enemy":
                Respawn();
                break;

            case "Points":
                points++;
                other.gameObject.SetActive(false);
                //add to points
                break;

            case "Lives":
                lives++;
                //add to lives
                break;

            case "Portal":
                Debug.Log("collided with portal");
                Portal tempPortal = other.gameObject.GetComponent<Portal>();
                transform.position = tempPortal.portalLocation.transform.position;
                spawnPoint = transform.position;
                break;

        }
    }
}
