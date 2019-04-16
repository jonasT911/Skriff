using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    float ymov=.35f;
   public float xmov = .3f;

    private PlayerControl player;


    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerControl>();
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
        ymov -= .01f;

        transform.Translate(xmov, ymov,0);
    }
}
