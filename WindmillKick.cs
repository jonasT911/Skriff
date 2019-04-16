using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillKick : MonoBehaviour {

    private PlayerControl player;
    private float xpos, ypos = 0;


    // Use this for initialization
    void Start()
    {
        Invoke("DestroyThis", .2f);
        player = FindObjectOfType<PlayerControl>();
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Enemy"))
        {
            player.windmillConnect();
        }
    }

            // Update is called once per frame
            void FixedUpdate()
    {
        if (player.isRight())
        {
            xpos = player.transform.position.x + .1f;
        }
        else
        {
            xpos = player.transform.position.x -.1f;
        }

        ypos = player.transform.position.y-1.05f;

        transform.position = new Vector2(xpos, ypos);
    }
}
