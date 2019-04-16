using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifebar : MonoBehaviour {

    public AudioSource Sfx;
    public AudioClip fist;

    private float xpos, ypos = 0;
    public float maxX, minX, maxY, minY;
    private float lifebarSize = 0;
    private int oldlife = 0;
  

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
      

        if (oldlife < player.getLife())
        {
            Sfx.clip = fist;
            Sfx.pitch = 0.53f;
            Sfx.Play(0);
        }
        oldlife = player.getLife();
        maxX = Maincamera.maxX;
        maxY = Maincamera.maxY;
        minX = Maincamera.minX;
        minY = Maincamera.minY;

        lifebarSize = 1.03f-player.getLife()/8.5f;
        xpos = player.transform.position.x;
        ypos = player.transform.position.y;
        if (lifebarSize < 0)
        {
            lifebarSize = 0;
        }


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
        transform.localScale = new Vector3(lifebarSize, 1, 1);
        gameObject.transform.position = new Vector3(xpos-10.5f-(float)player.getLife()/2, (ypos / 2)+7.6f, transform.position.z);
    }
}
