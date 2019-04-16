using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FencingEnemy : MonoBehaviour
{
    private PlayerControl player;

    private float xmov, ymov, combo = 0;
    private bool ground, pain, busy, deadly, right, dontAttack, attackAnim ,bounceBack= false;
    private int currentLife, emitdust = 0;


    private CameraScript Maincamera;

    private Animator anim;

    public AudioSource Sfx;
    public AudioClip fist;
    public AudioClip knife;

    public GameObject dustCloud;
    public GameObject MyBox;
    public GameObject singleDust;
    public GameObject sparks;
    public GameObject deadBody;

    // Use this for initialization
    void Start()
    {
        
        player = FindObjectOfType<PlayerControl>();
        anim = GetComponent<Animator>();
        right = false;
        Maincamera = FindObjectOfType<CameraScript>();
    }

    private void ReadyToAttack()
    {
        dontAttack = false;
    }

    private void stopMoving()
    {
        xmov = 0;


        deadly = false;
    }
    private void reset()
    {
        Invoke("ReadyToAttack", .4f);
        xmov = 0;
        attackAnim = false;
        busy = false;
        pain = false;
        
    }


    private void seekPlayer()
    {

        if (player.transform.position.x + 3f > (transform.position.x))
        {
            xmov = .15f;
        }
        else if (player.transform.position.x < (transform.position.x - 8))
        {
            xmov = -.15f;
        }
        else
        {
            if (xmov == 0){
                xmov = .15f;
            }
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
                if (!deadly)
                {
                    combo++;
                    attackAnim = false;
                    Instantiate(sparks, new Vector2((transform.position.x + other.transform.position.x) / 2, (transform.position.y + other.transform.position.y * 2) / 3), Quaternion.identity);

                    CancelInvoke("attackLeft");
                    CancelInvoke("attackRight");
                    CancelInvoke("isVulnerable");
                    CancelInvoke("ReadyToAttack");
                    CancelInvoke("stepBack");
                    CancelInvoke("reset");
                    currentLife++;
                    if (currentLife >= 5)
                    {
                        player.gainLife();
                        player.gainLife();

                        Instantiate(deadBody, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

                        Destroy(gameObject);
                    }
                    Invoke("IsVulnerable", .2f);
                    Invoke("reset", .8f);
                    pain = true;


                    bounceBack = true;

                    if (player.transform.position.x > transform.position.x)
                    {
                        if (!right)
                        {
                            flip();
                        }
                        right = true;
                        xmov = -.1f;

                    }
                    else

                    {
                        if (right)
                        {
                            flip();
                        }
                        right = false;
                        xmov = .1f;
                    }
                    }else{
                        currentLife++;
                        if (currentLife >= 5)
                        {
                            player.gainLife();

                            Instantiate(deadBody, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

                            Destroy(gameObject);
                        
                    }
                }
            }

            if (!other.gameObject.name.Equals("UpKick(Clone)")&&!deadly)
            {
                ymov = .2f;
            }
            deadly = false;
        }


    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name.Equals("Player"))
        {
            /*  if (deadly)
              {

                  if (!player.isInvulnerable())
                  {
                      Sfx.clip = fist;
                      Sfx.pitch = 0.53f;
                      Sfx.Play(0);
                      player.takeDamage();
                  }
              }
              */
        }
        if (other.tag.Equals("Enemy"))
        {
            if (other.transform.position.x > transform.position.x)
            {
                transform.position = new Vector2(transform.position.x - .1f, transform.position.y);
            }
            else
            {

                transform.position = new Vector2(transform.position.x + .1f, transform.position.y);
            }
        }
        if (other.tag.Equals("Solid"))
        {
            if (transform.position.y - 1.5f > other.transform.position.y + 2.3 * other.transform.localScale.y / 2)
            {

                if (ground == false)
                {
                   
                    combo = 0;
                    reset();
                    dontAttack = true;
                    Instantiate(dustCloud, new Vector2(transform.position.x, transform.position.y - .8f), Quaternion.identity);
                }
                ground = true;
                ymov = 0;
                if (bounceBack)
                {
                    print("bye");
                    dontAttack = true;
                    busy = true;
                    stopMoving();
                    CancelInvoke("attackLeft");
                    CancelInvoke("attackRight");
                    CancelInvoke("isVulnerable");
                    CancelInvoke("ReadyToAttack");
                    CancelInvoke("stepBack");
                    CancelInvoke("reset");

                    stepBack();
                    bounceBack = false;
                }

            }
        }

        if (other.gameObject.name.Equals("UpKick(Clone)"))
        {
            CancelInvoke("IsVulnerable");
            Invoke("IsVulnerable", .2f);

            CancelInvoke("reset");
            Invoke("reset", .4f);
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
        dontAttack = true;

        xmov = .62f;
        Invoke("stepBack", .4f);
        Invoke("stopMoving", .17f);
        deadly = true;
    }
    void attackLeft()
    {
        dontAttack = true;

        xmov = -.62f;
        Invoke("stepBack", .4f);
        Invoke("stopMoving", .17f);
        deadly = true;
    }
    
    void stepBack()
    {
        print("movingNOw");
        ymov = .1f;
        Invoke("reset", .6f);
        Invoke("stopMoving", .2f);
        if (player.transform.position.x > transform.position.x)
        {
            if (!right)
            {
                flip();
                right = true;
            }
            xmov = -.4f;
           
        }
        else
        {
            if (right)
            {
                flip();
                right = false;
            }
            xmov = .4f;
           
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if (ground && System.Math.Abs(xmov) > 0 && !busy)
        {
            if (emitdust > 10)
            {
                emitdust = 0;
                Instantiate(singleDust, new Vector2(transform.position.x, transform.position.y - .8f), Quaternion.identity);
            }

            else
            {
                emitdust++;
            }
        }
        else
        {
            emitdust = 0;
        }



        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        if (!ground)
        {
            ymov -= .01f;
            if (ymov < -4)
            {
                ymov = -4;
            }
        }

        if (Maincamera.transform.position.x > transform.position.x - 18 && Maincamera.transform.position.x < transform.position.x + 25)
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
        if (player.transform.position.x < transform.position.x + 9 && player.transform.position.x > transform.position.x - 9&& player.transform.position.y < transform.position.y + 3 && player.transform.position.y > transform.position.y - 2)
        {
            if (!pain)
            {
                if (ground)
                {
                    if (!dontAttack)
                    {
                        if (!busy)
                        {
                            attackAnim = true;
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
        }

        if (player.transform.position.x > transform.position.x && !right && !busy && ground)
        {
            flip();
            right = true;
        }
        else if (player.transform.position.x < transform.position.x && right && !busy && ground)
        {
            flip();
            right = false;
        }

        if (deadly)
        {
            if (right)
            {
                Instantiate(MyBox, new Vector2(transform.position.x + 1.9f, transform.position.y), Quaternion.identity);
            }
            else
            {
                Instantiate(MyBox, new Vector2(transform.position.x - 1.9f, transform.position.y), Quaternion.identity);
            }
        }

        anim.SetBool("pain", pain);
        anim.SetBool("attacking", attackAnim);
        anim.SetBool("ground", ground);

        transform.Translate(xmov, ymov, 0);
    }
}