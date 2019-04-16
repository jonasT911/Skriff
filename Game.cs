using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]

public class Game 
{

    public static Game current;
    public string currentScene;

    public Game()
    {
        currentScene = "";
        currentScene = SceneManager.GetActiveScene().name;
    }
 
}
