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
    public int speed = 7;
    
    private Rigidbody rigidbodyRef;
    public GameObject attackPrefab;

    public float despawnTimer = 1f;

    public Transform child;

    public float jumpForce = 10f;
    //bouncing off an enemies head
    public float bounce = 5.0f;

    public float deathYLevel = -2;

    private bool attacking = false;
    //bouncing off an enemies head
    public bool bouncy = false;
    private float attackRate = 1.5f;

    public Vector3 startingX;
    public Vector3 startingY;
    public GameObject spawnPoint;
    public GameObject tempPortal;


    // Start is called before the first frame update
    void Start()
    {
        //Get rigidbody component off of the object & stores a reference to it
        rigidbodyRef = GetComponent<Rigidbody>();

        //Move Player to start point
        transform.position = spawnPoint.transform.position;

        //Store object's X coordinates when scene starts
        startingX = transform.position;
        startingY = transform.position;
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

            //Set Attack Active
            attackPrefab.gameObject.SetActive(true);

            //Attack cooldown
            StartCoroutine(AttackCooldown());

            //Deapwn attack
            StartCoroutine(Despawn());


            Debug.Log("Now Attacking");
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

        
        //Attacking
        if (Input.GetKeyDown(KeyCode.L) && !attacking)
        {
            Attack();
        }

        /* Trying to make enemies bounce off heads
        if (bouncy)
        {
            rigidbodyRef.AddForce(Vector3.up * bounce, ForceMode.Impulse);
            bouncy = false;
        } */
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
    public void Respawn()
    {
        //I rewrote the respawn script because it wasn't telling me when I died
        lives--;
        transform.position = spawnPoint.transform.position;
        if (lives <= 0)
        {
            Debug.Log("Player Died");
            //swithces scene to game over
            EndScreen.SceneSwitch(1);
            //just moves player back (for testing)
            transform.position = spawnPoint.transform.position;
        }

        

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
                //Debug.Log("If you see this, let me know - Joseph");
                break;

            case "Enemy":
                if (other.transform.position.y <= transform.position.y)
                {
                    other.gameObject.SetActive(false);
                    //rigidbodyRef.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    //bouncy = true;
                    break;
                }
                else
                {
                    Debug.Log("Player dies by enemy");
                    Respawn();
                    break;
                }

            case "Spike":
                Respawn();
                break;

            case "Coin":
                points++;
                other.gameObject.SetActive(false);
                Debug.Log("Player collect fruit");
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
                spawnPoint = tempPortal.portalLocation;
                break;

            case "Shield":
                Debug.Log("collided with spikes");
                Respawn();
                break;

            case "Spike Enemy":
                Respawn();
                break;

            case "Crate":
                if (other.transform.position.y <= transform.position.y)
                {
                    other.gameObject.GetComponent<Crate>().Smash(5);
                    break;
                }
                else
                {
                    break;
                }
        }
    }


    //Prevents player from spamming attack
    IEnumerator AttackCooldown()
    {
        
        yield return new WaitForSeconds(attackRate);
        attacking = false;
    }

    //Set Attack False
    IEnumerator Despawn()
    {
        Debug.Log("Attack despawn");
        yield return new WaitForSeconds(despawnTimer);
        attackPrefab.gameObject.SetActive(false);

    }
}
