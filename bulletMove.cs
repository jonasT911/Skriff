using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour {

    private PlayerControl player;

    private float ydist,xdist,slope=0;

    
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerControl>();
        xdist = player.transform.position.x-transform.position.x;
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
                transform.Rotate(0, 0, slope);
            }
            else
            {
                slope = slope + 180;
             
                transform.Rotate(0, 0,slope);
            }
           
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Solid"))
        {
            Destroy(gameObject);
           
        }
        if (collision.name.Equals("Player"))
        {
            if (!player.isInvulnerable())
            {
                
                player.takeDamage();
                Destroy(gameObject);
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate () {
        transform.Translate(.4f, 0, 0);

        if (transform.position.y > 200) 
        {
            Destroy(gameObject);
        }
	}
}
