using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour {


    private PlayerControl player;
    private float xmov = .6f;
    private float ymov,noise = 0;
    public int right = 1;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerControl>();
        noise = Random.Range(.75f,1.25f);
        ymov = noise*.002f-.002f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals("Player"))
        {
           
                if (!player.isInvulnerable())
                {

                    player.takeDamage();
                }
            
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        xmov -= .03f*noise;

        ymov += .001f*noise;

        

        transform.Translate(right*xmov, ymov, 0);

        if (xmov < 0)
        {
            Destroy(gameObject);
        }
	}
}
