using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeGrunt : MonoBehaviour {
    private PlayerControl player;

    private float xmov, ymov = 0;
    private bool ground, pain, busy, deadly, right = false;
    private int currentLife = 0;
    private int random = 1;

    private Animator anim;


    // Use this for initialization
    void Start()
    {
        random = Random.Range(1, 3);
        player = FindObjectOfType<PlayerControl>();
        anim = GetComponent<Animator>();
        right = true;
    }

    private void stopMoving()
    {
        xmov = 0;
        ymov = 0;
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
        //RUNNING!!!
        if (player.transform.position.x > transform.position.x -0f)
        {
            xmov = .26f;
        }
        else if (player.transform.position.x < transform.position.x - 2.5f)
        {
            xmov = -.26f;
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
            if (!pain)
            {
                CancelInvoke("attackLeft");
                CancelInvoke("attackRight");
                CancelInvoke("isVulnerable");
                CancelInvoke("reset");
                currentLife++;
                if (currentLife >= 5)
                {
                    player.gainLife();

                    Destroy(gameObject);
                }
                Invoke("IsVulnerable", .4f);
                Invoke("reset", .6f);
                pain = true;



                //ON   HIT!!!!
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
            deadly = false;
        }


    }

    private void dodge()
    {
        Invoke("reset", .3f);
        busy = true;
        if (player.transform.position.x > transform.position.x)
        {
            xmov = -.4f;
        }
        else
        {
            xmov = .4f;
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
        }

        if (other.gameObject.name.Equals("UpKick(Clone)"))
        {
            transform.Translate(0, .5f * Time.deltaTime * 60, 0);
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
        Invoke("reset", .4f);
        Invoke("stopMoving", .2f);
        deadly = true;
    }
    void attackLeft()
    {
        xmov = -.4f;
        Invoke("reset", .4f);
        Invoke("stopMoving", .2f);
        deadly = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!ground)
        {
            ymov -= .01f;
            if (ymov > 3)
            {
                ymov = 3;
            }
        }

        if (player.transform.position.x > transform.position.x - 18 && player.transform.position.x < transform.position.x + 25)
        {
            if (!pain)
            {
                if (ground)
                {
                    if (!busy)
                    {
                        seekPlayer();
                    }
                }
            }
        }
        if (player.transform.position.x < transform.position.x + 4 && player.transform.position.x > transform.position.x - 4
            && player.transform.position.y < transform.position.y + 4 && player.transform.position.y > transform.position.y - 4)
        {
            if (!pain)
            {
                if (ground)
                {
                    if (!busy)
                    {

                        if (random == 2)
                        {
                            random = Random.Range(1, 3);
                            busy = true;
                            xmov = 0;
                            if (player.transform.position.x > transform.position.x)
                            {
                                if (player.transform.position.x < transform.position.x && !right)
                                {
                                    flip();
                                    right = true;
                                }
                                Invoke("attackRight", .2f);
                            }
                            else
                            {
                                if (player.transform.position.x < transform.position.x && right)
                                {
                                    flip();
                                    right = false;
                                }
                                Invoke("attackLeft", .2f);
                            }
                        }
                        else
                        {
                            random = Random.Range(1, 3);
                            xmov = 0;
                            dodge();

                        }
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
        anim.SetBool("ground", ground);

        transform.Translate(xmov, ymov, 0);
    }
}