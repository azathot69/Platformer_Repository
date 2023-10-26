using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public PlayerControl playerControl;
    public TMP_Text scoreText;
    public TMP_Text livesText;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        //make points and score public variables
        scoreText.text = "Score: ";
        livesText.text = "Lives: ";
    }
}
