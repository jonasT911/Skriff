﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour {
    private PlayerControl player;

    private float xmov, ymov = 0;
    private bool ground, pain, busy, deadly, right = false;
    private int currentLife = 0;

    public GameObject Rightflames;
    public GameObject Leftflames;

    private Animator anim;

    public AudioSource Sfx;
    public AudioClip TakeDamage;
    public AudioClip ShootFire;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        anim = GetComponent<Animator>();
        right = true;
    }

    private void stopMoving()
    {
        CancelInvoke("fireRight");
        CancelInvoke("fireLeft");
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
        if (player.transform.position.x > transform.position.x)
        {
            if (player.transform.position.x > transform.position.x + 5f)
            {
                xmov = .2f;
            }
            else if (player.transform.position.x < transform.position.x + 4.4f)
            {
                xmov = -.15f;
            }
            else { xmov = 0; }
        }
        else
        {
            if (player.transform.position.x < transform.position.x - 5f)
            {
                xmov = -.2f;
            }
            else if (player.transform.position.x > transform.position.x - 4.4f)
            {
                xmov = +.15f;
            }
            else { xmov = 0; }
        }
    }

    private void IsVulnerable()
    {
        pain = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag.Equals("playerBox"))
        {
            if (!pain)
            {
                CancelInvoke("attackLeft");
                CancelInvoke("attackRight");
                CancelInvoke("fireRight");
                CancelInvoke("fireLeft");
                CancelInvoke("isVulnerable");
                CancelInvoke("reset");
                currentLife++;
                if (currentLife >= 3)
                {
                    player.gainLife();
                    player.gainLife();
                    Destroy(gameObject);                       //DEATH
                }
                Invoke("IsVulnerable", .4f);
                Invoke("reset", .6f);
                pain = true;




                if (player.transform.position.x > transform.position.x)
                {

                    xmov = -.15f;

                }
                else

                {
                    xmov = .15f;
                }
            }

            if (!other.gameObject.name.Equals("UpKick(Clone)"))
            {
                ymov = .2f;
            }
            deadly = false;
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



    void fireRight()
    {
        Instantiate(Rightflames, new Vector3(transform.position.x+1.5f, transform.position.y, 0), Quaternion.identity);
        Invoke("fireRight", .05f);
    }
    void fireLeft()
    {
        Instantiate(Leftflames, new Vector3(transform.position.x-1.5f, transform.position.y, 0), Quaternion.identity);
        Invoke("fireLeft", .05f);
    }

    void attackRight()
    {
       // Sfx.clip = ShootFire;
     //   Sfx.Play(0);
        fireRight();
        Invoke("reset", .7f);
        Invoke("stopMoving", .4f);

    }
    void attackLeft()
    {
        fireLeft();
        //Sfx.clip = ShootFire;
      //  Sfx.Play(0);
      
        Invoke("reset", .7f);
        Invoke("stopMoving", .4f);

    }




    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

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
        if (player.transform.position.x < transform.position.x + 5 && player.transform.position.x > transform.position.x - 5 && player.transform.position.y < transform.position.y + 3.5f && player.transform.position.y > transform.position.y - 2)
        {
            if (!pain)
            {
                if (ground)
                {
                    if (!busy)
                    {
                        busy = true;
                        xmov = 0;
                        if (player.transform.position.x > transform.position.x)
                        {
                            if (!right)
                            {
                                flip();
                                right = true;
                            }
                            Invoke("attackRight", .2f);
                        }
                        else
                        {
                            if (right)
                            {
                                flip();
                                right = false;
                            }
                            Invoke("attackLeft", .2f);
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



