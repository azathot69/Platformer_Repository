using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Liebert, Jasper
//10/31/2023
//This script will provide a function for swithching scenes
public class EndScreen : MonoBehaviour
{
    /// <summary>
    /// This function will close the application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// This function will switch the scene to an index of your choosing
    /// </summary>
    /// <param name="sceneIndex"> the index of your choosing </param>
    public static void SceneSwitch(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
