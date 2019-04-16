using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGainBar : MonoBehaviour {


    private float xpos, ypos = 0;
    private float maxX, minX, maxY, minY;
    private float lifebarSize = 0;

    private PlayerControl player;

    private CameraScript Maincamera;

    // Use this for initialization
    void Start()
    {
        Maincamera = FindObjectOfType<CameraScript>();
       

        player = GameObject.FindObjectOfType<PlayerControl>();


    }

    // Update is called once per frame
    void LateUpdate()
    {
        maxX=Maincamera.maxX  ;
        maxY= Maincamera.maxY ;
        minX = Maincamera.minX;
         minY= Maincamera.minY;

        lifebarSize =  player.getLifeGain() / 8.51f;
        ypos = player.transform.position.y;
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


        transform.localScale = new Vector3(lifebarSize, .425f, 1);
        gameObject.transform.position = new Vector3(xpos - 14.75f + player.getLifeGain(), (ypos / 2) + 10.7f / 2.1f, transform.position.z);
    }
}

//