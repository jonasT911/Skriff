using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBull : MonoBehaviour {

    private PlayerControl player;

    private float xmov, ymov = 0;
    private bool ground, pain, busy, deadly, right = false;
    private int currentLife = 0;

    private Animator anim;


    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        anim = GetComponent<Animator>();
        right = true;
    }

    private void stopMoving()
    {
        xmov =xmov/4;
        ymov = .07f;
        deadly = false;
    }
    private void reset()
    {
        xmov = 0;
        ymov = 0;
        busy = false;
        pain = false;

    }


    private void seekPlayer()
    {
        if (player.transform.position.x > transform.position.x -3f)
        {
            xmov = .25f;
        }
        else if (player.transform.position.x < transform.position.x -8f)
        {
            xmov = -.2f;
        }

        if (player.transform.position.y > transform.position.y)
        {
            ymov = .1f;
        }
        else if (player.transform.position.y < transform.position.y - 1)
        {
            ymov = -.1f;
        }
        else
        {
            ymov = -.02f;
        }

    }

    private void IsVulnerable()
    {
        pain = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals("Player"))
        {
            if (deadly)
            {

                if (!player.isInvulnerable())
                {


                    player.takeDamage();
                }
            }
        }
        if (other.tag.Equals("playerBox"))
        {
            {
                if (!pain)
                {
                    CancelInvoke("attackLeft");
                    CancelInvoke("attackRight");
                    CancelInvoke("isVulnerable");
                    CancelInvoke("reset");
                    currentLife++;
                    if (currentLife >= 4)
                    {
                        player.gainLife();
                        player.gainLife();
                        Destroy(gameObject);
                    }
                    Invoke("IsVulnerable", .4f);
                    Invoke("reset", .6f);
                    pain = true;
                    deadly = false;



                    if (player.transform.position.x > transform.position.x)
                    {

                        xmov = -.1f;

                    }
                    else

                    {
                        xmov = .1f;
                    }
                }

                if (!other.gameObject.name.Equals("UpKick(Clone)"))
                {
                    ymov = .2f;
                }

            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag.Equals("Solid"))
        {

           
            if (transform.position.y > other.transform.position.y + 2.3 * other.transform.localScale.y / 2)
            {
                ground = true;
                ymov = 0;
            }
            else
            {
 stopMoving();
            }
        }

      


        if (other.gameObject.name.Equals("Player"))
        {
            if (other.transform.position.x > transform.position.x)
            {
                other.transform.Translate(.3f, 0, 0);
            }
            else
            {
                other.transform.Translate(-.3f, 0, 0);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Solid"))
        {
            ground = false;
        }
    }

    private void flip()
    {
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x = localScale.x * -1;
        transform.localScale = localScale;
    }




    void attackRight()
    {
        xmov = .4f;
        Invoke("reset", 1.5f);
        Invoke("stopMoving", .6f);
        deadly = true;
    }
    void attackLeft()
    {
        xmov = -.4f;
        Invoke("reset", 1.5f);
        Invoke("stopMoving", .6f);
        deadly = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        if (pain)
        {
            ymov -= .01f;

            if (ymov > 3.5f)
            {
                ymov = 3.5f;
            }
        }

        if (player.transform.position.x > transform.position.x - 18 && player.transform.position.x < transform.position.x + 25)
        {
            if (!pain)
            {
                if (!busy)
                {
                    seekPlayer();
                }
            }
        }
        if (player.transform.position.x < transform.position.x + 10 && player.transform.position.x > transform.position.x - 10 && player.transform.position.y < transform.position.y + 5 && player.transform.position.y > transform.position.y - 2)
        {
            if (!pain)
            {

                if (!busy)
                {
                    busy = true;
                    xmov = 0;
                    ymov = 0;
                    if (player.transform.position.x > transform.position.x)
                    {
                        if (!right)
                        {
                            flip();
                            right = true;
                        }
                        Invoke("attackRight", .26f);
                    }
                    else
                    {
                        if (right)
                        {
                            flip();
                            right = false;
                        }
                        Invoke("attackLeft", .26f);
                    }
                }
            }
        }
        


        if (player.transform.position.x > transform.position.x && !right && !busy)
        {
            flip();
            right = true;
        }
        else if (player.transform.position.x < transform.position.x && right && !busy)
        {
            flip();
            right = false;
        }


        anim.SetBool("pain", pain);
        anim.SetBool("attacking", busy);
        anim.SetBool("ground", !ground);

        transform.Translate( xmov, ymov, 0);
    }
}