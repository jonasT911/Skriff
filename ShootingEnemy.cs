using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    private PlayerControl player;

    private float xmov, ymov = 0;
    private bool ground, pain, busy, deadly, right,aim = false;
    private int currentLife = 0;
    public GameObject bullet;
   // public GameObject arm;

    private Animator anim;

    public bool DoIMove=true;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        anim = GetComponent<Animator>();
        right = true;
    }

    private void stopMoving()
    {
        xmov = 0;
        ymov = 0;
    }
    private void reset()
    {
        xmov = 0;
        ymov =  0;
       busy = false;
        pain = false;
        deadly = false;
        aim = false;

        CancelInvoke("ReadyShot");
        CancelInvoke("Shoot");
    }

    private void IsVulnerable()
    {
        pain = false;
    }

    private void seekPlayer()
    {
        if (player.transform.position.x > transform.position.x)
        {
            if (player.transform.position.x > transform.position.x + 8f)
            {
                xmov = .17f;
            }else if (player.transform.position.x < transform.position.x + 7.4f)
            {
                xmov = -.12f;
            }
            else { xmov = 0; }
        }
        else
        {
            if (player.transform.position.x < transform.position.x - 8f)
            {
                xmov = -.12f;
            }
            else if (player.transform.position.x > transform.position.x - 7.4f)
            {
                xmov = +.17f;
            }
            else { xmov = 0; }
        }
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

                if(player.transform.position.x > transform.position.x&&!right)
                {
                    flip();
                        right = true;
                }
                else if(player.transform.position.x< transform.position.x &&right)
                {
                    flip();
                    right = false;
                }

                CancelInvoke("ReadyShot");
                CancelInvoke("Shoot");
                CancelInvoke("isVulnerable");
                CancelInvoke("reset");
                CancelInvoke("ReadyUp");
                aim = false;
                currentLife++;
                if (currentLife >= 3)
                {
                    player.gainLife();
                    Destroy(gameObject);
                }
                Invoke("IsVulnerable", .4f);
                Invoke("reset", .6f);
                pain = true;




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


    void ReadyShot()
    {
        busy = true;
        xmov = 0;
        Invoke("Shoot", .6f);
    }
    void Shoot()
    {
       
        Invoke("readyUp", 1.3f);
         busy = false;
        if (player.transform.position.x > transform.position.x - 18 && player.transform.position.x < transform.position.x + 18)
        {
            if (player.transform.position.x > transform.position.x)
            {
                Instantiate(bullet, new Vector2(transform.position.x+1, transform.position.y + 1.5f), Quaternion.identity);
            }
            else
            {
                Instantiate(bullet, new Vector2(transform.position.x-1, transform.position.y + 1.5f), Quaternion.identity);
            }
        }
    }

    private void readyUp()
    {
        aim = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!ground)
        {
            ymov -= .01f;

            if (ymov > 4)
            {
                ymov = 4;
            }
        }

        if (player.transform.position.x > transform.position.x - 18 && player.transform.position.x < transform.position.x + 23)
        {
            if (!pain)
            {
                if (!busy)
                {
                    if (ground)
                    {
                        if (DoIMove) { 
                        seekPlayer();
                    }
                        if (!aim)
                        {
                            aim = true;
                          
                            Invoke("ReadyShot", .8f);
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
        anim.SetFloat("xmov", System.Math.Abs(xmov));

        transform.Translate(xmov, ymov , 0);
    }
}
