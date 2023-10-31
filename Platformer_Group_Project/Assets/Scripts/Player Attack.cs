using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerAttack : MonoBehaviour
{
    //Variables
    public GameObject playerPrefab;
    private Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = playerPrefab.transform.position;
        transform.position = playerPosition;

    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            default:

                break;

            case "Enemy":
                other.gameObject.SetActive(false);
                break;

            

        }
    }

}
