using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightBullet : MonoBehaviour {

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
        transform.Translate(.4f, 0, 0);
	}
}
