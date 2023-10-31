using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //Variables


    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            default:
                Debug.Log("If you see this, let me know - Joseph");
                break;

            case "Enemy":
                other.gameObject.SetActive(false);
                break;

            

        }
    }

}
