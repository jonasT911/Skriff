using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToRivalFight : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals("Player"))
        {
            SceneManager.LoadScene("RivalFight");
        }
    }
}
