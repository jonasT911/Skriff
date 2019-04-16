using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LGHolder : MonoBehaviour {

    
    private float xpos, ypos = 0;
    public float maxX, minX, maxY, minY;

    private CameraScript Maincamera;

    private PlayerControl player;
    // Use this for initialization
    void Start()
    {
        Maincamera = FindObjectOfType<CameraScript>();
        player = GameObject.FindObjectOfType<PlayerControl>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        maxX = Maincamera.maxX;
        maxY = Maincamera.maxY;
        minX = Maincamera.minX;
        minY = Maincamera.minY;

        xpos = player.transform.position.x;
        ypos = player.transform.position.y;


        if (xpos > maxX)
        {
            xpos = maxX;
        }
        else if (xpos < minX)
        {
            xpos = minX;
        }

        if (ypos < minY)
        {
            ypos = minY;
        }
        else if (ypos > maxY)
        {
            ypos = maxY;
        }


        gameObject.transform.position = new Vector3(xpos - 4.75f, (ypos / 2) + 6.7f, transform.position.z);
    }
}

