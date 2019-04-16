using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpKickScript : MonoBehaviour {

    private PlayerControl player;
    private float xpos, ypos=0;


	// Use this for initialization
	void Start () {
        Invoke("DestroyThis", .35f);
        player = FindObjectOfType<PlayerControl>();
	}

    private void DestroyThis()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate() { 
		if (player.isRight())
        {
            xpos = player.transform.position.x + .71f;
        }
        else
        {
            xpos = player.transform.position.x - .71f;
        }

        ypos = player.transform.position.y;

        transform.position = new Vector2(xpos, ypos);
	}
}
