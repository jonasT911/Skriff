using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private PlayerControl player;
    private float xmov = 0;
    private float ymov = .3f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroy", 5);

        player = FindObjectOfType<PlayerControl>();

        if (player.transform.position.x > transform.position.x)
        {
            xmov = -.3f;

        }
        else
        {
            xmov = .3f;
        }


        if (player.transform.position.x < transform.position.x)
        {

            print("flip");
            Vector2 localScale = gameObject.transform.localScale;
            localScale.x = localScale.x * -1;
            transform.localScale = localScale;
        }
    }

    private void destroy()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("playerBox"))
        {
            
        }
    }
    void FixedUpdate()
    {
        ymov = ymov - .02f;

        if (ymov < -2)
        {
            ymov = -2;
        }

        transform.Translate(xmov, ymov,0);
    }
}
