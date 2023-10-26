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

    // Start is called before the first frame update
    void Start()
    {
        //Get rigidbody component off of the object & stores a reference to it
        rigidbodyRef = GetComponent<Rigidbody>();

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
    /// MaMakes the player lose a life.
    /// If there is no lives left, perish
    /// </summary>
    private void Respawn()
    {
        if (lives != 0)
        {
            lives--;

            //Return to last spawn point
            transform.position = startingX;
            transform.position = startingY;
        }
        else
        {
            Debug.Log("The player has died");

            //Replace below code with proper Game Over Scene
            transform.position = startingX;
            transform.position = startingY;
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
                Debug.Log("If you see this, let me know - Joseph");
                break;

            case "Enemy":
                Respawn();
                break;

            case "Points":
                //add to points
                break;

            case "Lives":
                //add to lives
                break;
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
