using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControler : MonoBehaviour
{
    
    public void PlayGame()
    {
        // Load the game scene
        SceneManager.LoadScene("Minigame");
    }

    public void ExitGame()
    {
        // Exit the application
        Application.Quit();
    }
}