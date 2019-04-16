using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveGame 
{
    
    public static Game savedGames = new Game();
    
    public static void Save()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, currentScene);
        file.Close();
    }

    public static void Load()
    {
      //  string nextLevel = "error";
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
           string destination = (string)bf.Deserialize(file);
         
            file.Close();

         
            
            SceneManager.LoadScene(destination);
        }

      //  return nextLevel ;
    }
}
