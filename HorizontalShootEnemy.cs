using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalShootEnemy : MonoBehaviour {

    private PlayerControl player;

    private float xmov, ymov = 0;
    private bool ground, pain, busy, deadly, right, aim, recoiling,notFirst,Shooting= false;
    private int currentLife,emitdust = 0;
    public GameObject RBullet,LBullet;

    public GameObject dustCloud;
    public GameObject singleDust;
    public GameObject deadBody;
    public GameObject sparks;
   

    public AudioSource Sfx;
    public AudioClip shoot;

    private Animator anim;
    private CameraScript Maincamera;

    public bool DoIMove = true;

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
        ymov = 0;
    }
    private void reset()
    {
        xmov = 0;
        ymov = 0;
        busy = false;
        pain = false;
        deadly = false;
        aim = false;
        recoiling = false;
        Shooting = false;

        CancelInvoke("ReadyShot");
        CancelInvoke("ShootRight");
        CancelInvoke("ShootLeft");
        CancelInvoke("ShootRecoil");
        CancelInvoke("recoil");
        CancelInvoke("readyUp");
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
                xmov = .18f;
            }
            else if (player.transform.position.x < transform.position.x + 7.4f)
            {
                xmov = -.15f;
            }
            else { xmov = 0; }
        }
        else
        {
            if (player.transform.position.x < transform.position.x - 8f)
            {
                xmov = -.15f;
            }
            else if (player.transform.position.x > transform.position.x - 7.4f)
            {
                xmov = +.18f;
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
                Instantiate(sparks, new Vector2((transform.position.x + other.transform.position.x) / 2, (transform.position.y + other.transform.position.y * 2) / 3), Quaternion.identity);



                if (player.transform.position.x > transform.position.x)
                {
                    if (!right)
                    {
                        print("flip");
                        right = true;
                        flip();
                    }
                }
                else if(player.transform.position.x < transform.position.x)
                {
                    if (right)
                    {
                        print("flip Left");
                        right = false;
                        flip();
                    }
                }

                CancelInvoke("ReadyShot");
                CancelInvoke("ShootRight");
                CancelInvoke("ShootLeft");
                CancelInvoke("ReadyShot");
                CancelInvoke("isVulnerable");
                CancelInvoke("recoil");
                CancelInvoke("reset");
                CancelInvoke("readyUp");

                recoiling = false;

                busy = false;
                currentLife++;
                if (currentLife >= 3)
                {
                    player.gainLife();
                    Instantiate(deadBody, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    Destroy(gameObject);
                }
                Invoke("IsVulnerable", .2f);
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
            if (transform.position.y > other.transform.position.y + 2.3 * other.transform.localScale.y / 2)
            {
                if (ground == false)
                {
                    Instantiate(dustCloud, new Vector2(transform.position.x, transform.position.y - .8f), Quaternion.identity);
                }
                ground = true;
                ymov = 0;
            }
        }

        if (other.gameObject.name.Equals("UpKick(Clone)"))
        {
            CancelInvoke("IsVulnerable");
                 Invoke("IsVulnerable", .2f);
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
        if (player.transform.position.x > transform.position.x - 18 && player.transform.position.x < transform.position.x + 18)
        {


            if (player.transform.position.x > transform.position.x)
            {
                Invoke("ShootRight", .6f);
                if(!right)
                {
                    flip();
                    right = true;
                }

            }
            else
            {
                Invoke("ShootLeft", .6f);

                if (right)
                {
                    flip();
                    right = false;
                }
            }
            Shooting = true;
                busy = true;
        xmov = 0;

        }
        else
        {
            Invoke("readyUp", 1.3f);
        }
        }


    void ShootRight()
    {
        Sfx.Play(0);
        recoiling = true;
        Invoke("recoil", .29f);
                Instantiate(RBullet, new Vector2(transform.position.x + 1, transform.position.y + 1f), Quaternion.identity);

        Shooting = false;
  
        
    }

    void ShootLeft()
    {
        Shooting = false;
        Sfx.Play(0);
        recoiling = true;
        Invoke("recoil", .29f);
        Instantiate(LBullet, new Vector2(transform.position.x - 1, transform.position.y + 1f), Quaternion.identity);

    }

   void recoil()
    {
        Invoke("readyUp", 1.3f);
        recoiling = false;
        
        busy = false;
    }

    private void readyUp()
    {
        aim = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

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


        if (!ground)
        {
            ymov -= .01f;

            if (ymov > 4)
            {
                ymov = 4;
            }
        }

        if (Maincamera.transform.position.x > transform.position.x - 18 && Maincamera.transform.position.x < transform.position.x + 23)
        {
            if (!pain)
            {
                if (!busy)
                {
                    if (ground)
                    {
                        if (DoIMove)
                        {
                            seekPlayer();
                        }
                        if (!aim)
                        {
                            aim = true;

                            if (!notFirst)
                            {
                                Invoke("ReadyShot", .3f);
                            }
                            else
                            {
                                Invoke("ReadyShot", .8f);
                            }
                        }
                    }
                }
            }
        }





        if ((xmov>0||(!DoIMove&& player.transform.position.x > transform.position.x))&& !right && !busy&&!pain&&ground)
        {
            flip();
            right = true;
        }
        else if ((xmov < 0 || (!DoIMove && player.transform.position.x < transform.position.x)) && right && !busy && !pain&&ground)
        {
            flip();
            right = false;
        }


        anim.SetBool("pain", pain);
        anim.SetBool("attacking", Shooting);
        anim.SetBool("ground", ground);
        anim.SetBool("recoiling", recoiling);
        anim.SetFloat("xmov", System.Math.Abs(xmov));

        transform.Translate(xmov, ymov, 0);
    }
}
