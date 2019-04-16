using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArialAce : MonoBehaviour {
    private PlayerControl player;

    private float xmov, ymov = 0;
    private bool ground, pain, busy, deadly, right,aim = false;
    private int currentLife = 0;
    private int whichSide,yheight = 1;

    private Animator anim;

    public GameObject bullet;
    // Use this for initialization
    void Start()
    {
        whichSide = 1;
        yheight = Random.Range(3,11);
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
        whichSide = Random.Range(1, 3);
        if (whichSide == 2)
        {
            whichSide = -1;

        }
        yheight = Random.Range(3, 9);
        xmov = 0;
        ymov = 0;
        busy = false;
        pain = false;
        aim = false;

        CancelInvoke("ReadyShot");
        CancelInvoke("Shoot");
    }


    private void seekPlayer()
    {
        
        if (player.transform.position.x+(10*whichSide) > transform.position.x + .2f)
        {
            xmov = .18f;
        }
        else if (player.transform.position.x+(10*whichSide) < transform.position.x - .2f)
        {
            xmov = -.18f;
        }

        if (player.transform.position.y+yheight > transform.position.y)
        {
            ymov = .1f;
        }
        else if (player.transform.position.y+yheight < transform.position.y - 1)
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
            if (!pain)
            {
                CancelInvoke("ReadyShot");
                CancelInvoke("Shoot");
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
                aim = false;



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

    private void readyUp()
    {
        aim = false;
    }

    void ReadyShot()
    {
        busy = true;
        xmov = 0;
        ymov = -.07f;
        Invoke("Shoot", .6f);
    }
    void Shoot()
    {
        CancelInvoke("Shoot");
        whichSide = Random.Range(1, 3);
        if (whichSide == 2)
        {
            whichSide = -1;

        }
        yheight = Random.Range(2, 7);

        Invoke("readyUp", 1.3f);
        busy = false;
        if (player.transform.position.x > transform.position.x - 18 && player.transform.position.x < transform.position.x + 18)
        {
            if (player.transform.position.x > transform.position.x)
            {
                Instantiate(bullet, new Vector2(transform.position.x + 1, transform.position.y + 1.5f), Quaternion.identity);
            }
            else
            {
                Instantiate(bullet, new Vector2(transform.position.x - 1, transform.position.y + 1.5f), Quaternion.identity);
            }
        }
    }


  
 

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pain)
        {
            ymov -= .01f;

            if (ymov > 3.5f)
            {
                ymov = 3.5f;
            }
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        if (!ground)
        {
            //  ymov -= .02f;
        }

        if (player.transform.position.x > transform.position.x - 15 && player.transform.position.x < transform.position.x + 15)
        {
            if (!pain)
            {
                if (!busy)
                {
                    seekPlayer();
                }
            }
        }
        if (player.transform.position.x < transform.position.x +18 && player.transform.position.x > transform.position.x - 18)
        {
            if (!pain)
            {
                if (!busy)
                {
                    
                        seekPlayer();
                        if (!aim)
                        {
                            aim = true;

                            Invoke("ReadyShot", 1.3f);
                        }
                    
                }
            }
        }


        if (xmov > 0&&!right)
        {
            flip();
            right = true;
        }
        else if (xmov < 0&&right)
        {
            flip();
            right = false;
        }

        anim.SetBool("pain", pain);
        anim.SetBool("attacking", busy);
        transform.Translate(xmov, ymov, 0);
    }
}