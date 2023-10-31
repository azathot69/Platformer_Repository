using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Liebert, Jasper
//10/31/2023
//This script will update the text to match the players coins and lives

public class UIManager : MonoBehaviour
{
    //variables
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
        TextUpdate();
    }

    /// <summary>
    /// updates TMP text to display points and lives
    /// </summary>
    private void TextUpdate()
    {
        scoreText.text = "Points: " + playerControl.points;
        livesText.text = "Lives: " + playerControl.lives;
    }    
}
