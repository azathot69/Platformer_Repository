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

    public float deathYLevel = -2;

    private bool attacking = false;
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
        //Check input for movement, jumping, attacking
        Movement();
   

        //Check for pitfall
        if (transform.position.y <= deathYLevel)
        {
            Respawn();
        }
    }

    //Functions

    /// <summary>
    /// brings in attack object and starts a cooldown to despawn, and when you can press it again
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

            //Despawn attack
            StartCoroutine(Despawn());

            //Debug.Log("Now Attacking");
        }
    }

    /// <summary>
    /// Handles the player's movement, jumping and attacking
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

        /* Trying to make enemies bounce off heads (didn't work)
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
    /// Deals with triggerable objects the player collides with
    /// </summary>
    /// <param name="other">The object being collided with</param>
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            default:
                //Debug.Log("If you see this, let me know - Joseph");
                break;

            //sets enemy to inactive if the player is at a higher y position, if not player dies
            case "Enemy":
                if (other.transform.position.y <= transform.position.y)
                {
                    other.gameObject.SetActive(false);
                    break;
                }
                else
                {
                    //Debug.Log("Player dies by enemy");
                    Respawn();
                    break;
                }

            //kills you when you step on a spike
            case "Spike":
                Respawn();
                break;

            //Wumpa fruit, adds points until you have 100, then converts 100 fruit into lives
            case "Coin":
                if (points <= 100)
                {
                    points++;
                    other.gameObject.SetActive(false);
                    //Debug.Log("Player collect fruit");
                }
                else if (points >= 100)
                {
                    points -= 100;
                    points++;
                    lives++;
                }


                break;

            //Adds lives
            case "Lives":
                lives++;
                //add to lives
                break;

            //finds the portal teleport location and teleports player to the new location and sets their new spawn point
            case "Portal":
                //Debug.Log("collided with portal");
                Portal tempPortal = other.gameObject.GetComponent<Portal>();
                transform.position = tempPortal.portalLocation.transform.position;
                spawnPoint = tempPortal.portalLocation;
                break;

            //kills player when they collide with a shield
            case "Shield":
                //Debug.Log("collided with spikes");
                Respawn();
                break;

            //kills player when they collide with a spike enemy
            case "Spike Enemy":
                Respawn();
                break;

            //destroys the crate if the players position is higher and spawn 5 wumpa fruit
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

    /// <summary>
    /// Stops player from spamming attacks
    /// </summary>
    /// <returns> wait time </returns>
    IEnumerator AttackCooldown()
    {
        
        yield return new WaitForSeconds(attackRate);
        attacking = false;
    }

    /// <summary>
    /// How long the attack will last
    /// </summary>
    /// <returns> how many seconds </returns>
    IEnumerator Despawn()
    {
        Debug.Log("Attack despawn");
        yield return new WaitForSeconds(despawnTimer);
        attackPrefab.gameObject.SetActive(false);

    }
}
