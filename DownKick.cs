using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownKick : MonoBehaviour
{

    private PlayerControl player;
    private float xpos, ypos = 0;


    // Use this for initialization
    void Start()
    {
      
        player = FindObjectOfType<PlayerControl>();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.isRight())
        {
            xpos = player.transform.position.x + .5f;
        }
        else
        {
            xpos = player.transform.position.x - .5f;
        }

        if (!player.Diving())
        {

            Destroy(gameObject);
        }
        ypos = player.transform.position.y-2;

        transform.position = new Vector2(xpos, ypos);
    }
}
