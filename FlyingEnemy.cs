using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    private PlayerControl player;

    private float xmov, ymov = 0;
    private bool ground, pain, busy, deadly, right, dontAttack,fall,attackAnim = false;
    private int currentLife = 0;

    private Animator anim;

    public AudioSource Sfx;
    public AudioClip fist;


    public GameObject sparks;
    public GameObject MyBox;
    public GameObject DeadBody;


    private CameraScript Maincamera;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        anim = GetComponent<Animator>();
        right = true;
        Maincamera = FindObjectOfType<CameraScript>();
    }

    private void stopMoving()
    {
        xmov = 0;
        ymov = .01f;
        deadly = false;
    }
    private void reset()
    {
        xmov = 0;
        ymov = 0;
        busy = false;
        attackAnim = false;
        pain = false;
        dontAttack = false;

    }
    private void Attack()
    {
        dontAttack = false;
        fall = false;
    }


    private void seekPlayer()
    {
        transform.position = Vector3.MoveTowards(new Vector2(transform.position.x, transform.position.y - .01f), player.transform.position, .2f);

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

                dontAttack = true;
                attackAnim = false;
                fall = true;

                Instantiate(sparks, new Vector2((transform.position.x + other.transform.position.x) / 2, (transform.position.y + other.transform.position.y * 2) / 3), Quaternion.identity);

                CancelInvoke("attackLeft");
                CancelInvoke("attackRight");
                CancelInvoke("isVulnerable");
                CancelInvoke("Attack");
                CancelInvoke("reset");
                currentLife++;
                if (currentLife >= 4)
                {
                    player.gainLife();
                    Instantiate(DeadBody, new Vector2(transform.position.x, transform.position.y ), Quaternion.identity);
                    Destroy(gameObject);
                }
                Invoke("IsVulnerable", .2f);
                Invoke("reset", .8f);
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



            deadly = false;

        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
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
        if (other.name.Equals("Player"))
        {
       /*     if (deadly)
            {

                if (!player.isInvulnerable())
                {
                    Sfx.clip = fist;
                    Sfx.pitch = 0.53f;
                    Sfx.Play(0);

                    player.takeDamage();
                }
            }*/
        }

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
            CancelInvoke("IsVulnerable");
            Invoke("IsVulnerable", .2f);

            ymov = 0;

            CancelInvoke("reset");
            Invoke("reset", .4f);
           
            transform.Translate(0, .4f * Time.deltaTime * 60, 0);
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
        xmov = .3f;
        Invoke("Attack", .8f);
        Invoke("reset", .5f);
        Invoke("stopMoving", .2f);
        deadly = true;
    }
    void attackLeft()
    {
        xmov = -.3f;
        Invoke("Attack", .8f);
        Invoke("reset", .5f);
        Invoke("stopMoving", .2f);
        deadly = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        if (dontAttack&&!attackAnim)
        {
            ymov -= .01f;

            if (ymov < -3.5f)
            {
                ymov = -3.5f;
            }
        }

        if (Maincamera.transform.position.x > transform.position.x - 18 && Maincamera.transform.position.x < transform.position.x + 25)
        {
            if (!dontAttack)
            {
                if (!busy)
                {
                    seekPlayer();
                }
            }
        }
        if (player.transform.position.x < transform.position.x + 4 && player.transform.position.x > transform.position.x - 4
            && player.transform.position.y < transform.position.y + 3 && player.transform.position.y > transform.position.y - .5)
        {
            if (!pain)
            {
                if (!dontAttack)
                {
                    if (!busy)
                    {
                        ymov = 0;
                        busy = true;
                        attackAnim = true;
                        dontAttack = true;
                        xmov = xmov / 1.5f;
                        if (player.transform.position.x > transform.position.x)
                        {
                            if (!right)
                            {
                                flip();
                                right = true;
                            }
                            Invoke("attackRight", .25f);
                        }
                        else
                        {
                            if (right)
                            {
                                flip();
                                right = false;
                            }
                            Invoke("attackLeft", .25f);
                        }
                    }
                }
            }
        }

        if (deadly)
        {
            if (right)
            {
                Instantiate(MyBox, new Vector2(transform.position.x + 1, transform.position.y), Quaternion.identity);
            }
            else
            {
                Instantiate(MyBox, new Vector2(transform.position.x - 1, transform.position.y), Quaternion.identity);
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
        anim.SetBool("attacking", attackAnim);

        transform.Translate(xmov, ymov, 0);
    }
}