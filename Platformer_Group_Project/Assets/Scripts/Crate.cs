using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Liebert, Jasper
//10/31/2023
//this script will move the crate
public class Crate : MonoBehaviour
{
    Vector3 temp = Vector3.up;
    public GameObject wumpPrefab;

    private float topY;
    private float bottomY;
    public bool goingUp = true;
    public float crateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        topY = transform.position.y + 1;
        bottomY = transform.position.y - 1;
    }

    // Update is called once per frame
    void Update()
    {
        Wiggle();
        Smash(5);
    }

    /// <summary>
    /// This will make the crate bob up and down 
    /// </summary>
    public void Wiggle()
    {
        if (goingUp)
        {
            if (transform.position.y >= topY)
            {
                temp = Vector3.down;
                goingUp = false;
            }
        }
        else
        {
            if (transform.position.y <= bottomY)
            {
                temp = Vector3.up;
                goingUp = true;
            }
        }
        transform.position += temp * crateSpeed * Time.deltaTime;
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(5.0f);
        Smash(1);
    }

    /// <summary>
    /// Smashes the crate and spawns a number of wumpa fruit in random positions nearby
    /// </summary>
    /// <param name="wumps"></param>
    public void Smash(int wumps)
    {
        for (int i=0; i<wumps; i++)
        {
            Instantiate(wumpPrefab, ChangePosition(this.gameObject.transform.position), transform.rotation);
        }
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// takes an input vector3 and changes the position by 1 to make the wumps separate from eachother
    /// </summary>
    /// <param name="input"> vector 3 you are wanting to change </param>
    /// <returns></returns>
    public Vector3 ChangePosition(Vector3 input)
    {
        float x = input.x;
        float y = input.y;
        float z = input.z;
        float randomX = Random.Range(x - 1, x + 1);
        float randomZ = Random.Range(z - 1, z + 1);
        temp = new Vector3(randomX, y, randomZ);
        return temp;
    }
}
