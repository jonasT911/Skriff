using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmPoint : MonoBehaviour {


    private PlayerControl player;

    private float ydist, xdist, slope = 0;


    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerControl>();

        Invoke("destroyThis", 2.5f);

    }

    public void destroyThis()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate () {
        xdist = player.transform.position.x - transform.position.x;
        ydist = player.transform.position.y - transform.position.y;



        if (xdist == 0)
        {
            if (ydist >= 0)
            {
                transform.Rotate(0, 0, 90);
            }
            else
            {
                transform.Rotate(0, 0, -90);
            }
        }
        else
        {
            slope = ydist / xdist;

            slope = Mathf.Atan(slope);
            slope = slope / 3.14f * 180;


            if (xdist >= 0)
            {
              // transform.Rotate(0, 0, slope);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + slope);
                
            }
            else
            {
                slope = slope + 180;


                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + slope);
               // transform.Rotate(0, 0, slope);
            }

        }
    }
}
