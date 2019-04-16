using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flipKickScript : MonoBehaviour {

    private PlayerControl player;
    private float xpos, ypos = 0;


    // Use this for initialization
    void Start()
    {
        Invoke("DestroyThis", .05f);
        player = FindObjectOfType<PlayerControl>();
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.isRight())
        {
            xpos = player.transform.position.x - 1;
        }
        else
        {
            xpos = player.transform.position.x + 1;
        }

        ypos = player.transform.position.y-.3f;

        transform.position = new Vector2(xpos, ypos);
    }
}
