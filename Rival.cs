using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rival : MonoBehaviour {
    private PlayerControl player;

    private float xmov, ymov,offset = 0;
    private bool ground,pain,busy,deadly ,right,superArmor,resetJump,Rummage= false;   //added SuperArmor

    private bool GroundShoot, Stab, downKick, airShoot, BackJump,grenade,teleport,painAnimation,JumpingBack,JumpingForward,EscapeAnim,land = false; //all this
    private int currentLife,random,repeat,delaycount,cheat = 0;   //random+repeat

    private Animator anim;
    public GameObject rightbullet;
    public GameObject leftbullet;
    public GameObject RGrenade;
    public GameObject LGrenade;

    public GameObject dustCloud;
    public GameObject MyBox;
    public GameObject Smoke;
    public GameObject sparks;

    public AudioSource Sfx;
    public AudioClip fist;
    public AudioClip knife;
    public AudioClip shooting;

    public int difficulty = 3;

    // Use this for initialization
    void Start() {
        player = FindObjectOfType<PlayerControl>();
        anim = GetComponent<Animator>();
        right = true;
    }

    private void stopMoving()
    {
        xmov = 0;
        ymov = 0;
        deadly = false;

        //belowThis
        GroundShoot = false;
        Stab = false;
        downKick = false;
        BackJump = false;
        airShoot = false;
        Rummage = false;
        grenade = false;
        superArmor = false;
        JumpingBack = false;
        JumpingForward = false;
        EscapeAnim = false;

       if (repeat >= 3)
        {
            teleport = false;
        }
        else
        {
            if (teleport)
            {
                
                Instantiate(Smoke, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                transform.position = new Vector2(0, -30);

            }
        }
    }

    void painAnimEnd()
    {
        painAnimation = false;
        CancelInvoke("painAnimEnd");
    }
    private void reset()
    {
        xmov = 0;
        ymov = 0;
        busy = false;
        pain = false;
        land = false;
        teleport = false;
        painAnimation = false;
        CancelInvoke("ResetJump");

        CancelInvoke("SMG");
        CancelInvoke("NearKick");
        CancelInvoke("airMove");
        CancelInvoke("diveKicking");
        CancelInvoke("isVulnerable");
        CancelInvoke("grenadeToss");
        CancelInvoke("Reappear");
        

        CancelInvoke("reset");

      
    }


    //Resetting Jump
    private void readyReset()
    {
        
        resetJump = true;
    }
    private void ResetJump()
    {
        CancelInvoke("readyReset");
        CancelInvoke("ResetJump");


        if (player.transform.position.x < transform.position.x && !right)
        {
            flip();
            right = true;
        }
        else if (player.transform.position.x > transform.position.x && right)
        {
            flip();
            right = false;
        }

        superArmor = true;
        if (0 > transform.position.x)
        {
            xmov = .85f;

        }
        else
        {
            xmov = -.85f;
        }

        busy = true;
        EscapeAnim = true;
        ymov = .05f;



     CancelInvoke("reset");
        CancelInvoke("stopMoving");
        Invoke("stopMoving", .5f);
        Invoke("reset", .51f);
    }
    //rival Moves

        private void Reappear()
    {
        print(cheat);
        repeat++;
        if (repeat < 3)
        {
            Invoke("Reappear", 1.1f);


        }
       
        ymov = 0;
        superArmor = true;
        random= Random.Range(1, 5);

        if (random == 1||(cheat==2&&random==2))
        {
            if (player.transform.position.x > -6f)
            {
                transform.position = new Vector2(player.transform.position.x - 11, -5);
            }
            else
            {
                transform.position = new Vector2(player.transform.position.x + 11, -5);
            }
            busy = true;
            Stab = true;

            Invoke("NearKick", .5f);
        }
        if (random == 3 || (cheat == 2 && random == 4))
        {
            if (player.transform.position.x < 6)
            {
                transform.position = new Vector2(player.transform.position.x + 11, -5);
            }
            else
            {
                transform.position = new Vector2(player.transform.position.x - 11, -5);
            }
            busy = true;
            Stab = true;

            Invoke("NearKick", .5f);
        }

        if (random ==2&&!(cheat==2))
        {
            if (player.transform.position.x > -6)
            {
                transform.position = new Vector2(player.transform.position.x - 9, 0);
            }
            else
            {
                transform.position = new Vector2(player.transform.position.x + 9, 0);
            }
            busy = true;
            Stab = true;
            cheat++;

            Invoke("NearKick", .5f);
        }
        if (random == 4&& !(cheat == 2))
        {
            if (player.transform.position.x < 6)
            {
                transform.position = new Vector2(player.transform.position.x + 9, 0);
            }
            else
            {
                transform.position = new Vector2(player.transform.position.x - 9, 0);
            }
            busy = true;
            Stab = true;
            cheat++;

            Invoke("NearKick", .5f);
        }

        Instantiate(Smoke, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        if (player.transform.position.x > transform.position.x && !right)
        {
            flip();
            right = true;
        }
        else if (player.transform.position.x < transform.position.x && right )
        {
            flip();
            right = false;
        }
       
    }

    private void grenadeToss()
    {
        resetJump = false;
        CancelInvoke("readyReset");

        if (right)
        {
            Instantiate(RGrenade, new Vector2(transform.position.x + 1, transform.position.y+1 ), Quaternion.identity);
        }
        else
        {
            Instantiate(LGrenade, new Vector2(transform.position.x - 1, transform.position.y +1), Quaternion.identity);
        }

        Invoke("stopMoving", .3f);
        if (!(delaycount == difficulty))
        {
            
            delaycount++;
            Invoke("reset", .5f);
        }
        else
        {
           
            delaycount = 0;
            Invoke("reset", .65f);
        }
    }

    private void airMove()
    {
        stopMoving();

        random = Random.Range(1, 3);

        if (random == 1)
        {

           

            repeat = 0;
            busy = true;
            airShoot = true;

            Invoke("SMG", .2f);
        }
        if (random == 2)
        {
           

            busy = true;
            downKick = true;

            ymov = .1f;
            Invoke("diveKicking",.2f);
        }

        }

    private void diveKicking()
    {
        resetJump = false;
        CancelInvoke("readyReset");

        superArmor = true;
        deadly = true;
        ymov = -.6f;
        if (right)
        {
            xmov = .5f;
        }
        else
        {
            xmov = -.5f;
        }
      


    }
    private void SMG()
    {
        resetJump = false;
        CancelInvoke("readyReset");
        if (ground)
        {
            offset = 5.5f;
        }
        else
        {
            offset = 2;
        }

        if (repeat < 3)
        {
            Sfx.clip = shooting;
            Sfx.pitch = 1f;
            Sfx.Play(0);
            if (right)
            {
                Instantiate(rightbullet, new Vector2(transform.position.x + offset, transform.position.y+1 ), Quaternion.identity);
            }
            else
            {
                Instantiate(leftbullet, new Vector2(transform.position.x - offset, transform.position.y +1), Quaternion.identity);
            }
            repeat++;
            Invoke("SMG", .1f);
        }
        else
        {
            Invoke("stopMoving", .3f);
            if (!(delaycount == difficulty))
            {
                delaycount++;
                Invoke("reset", .41f);
            }
            else
            {
                
                delaycount = 0;
                Invoke("reset", .6f);
            }
        }

    }

    private void NearKick()
    {

        resetJump = false;
        CancelInvoke("readyReset");


        superArmor = true;
        deadly = true;
        if (right)
        {
            xmov = .35f;
        }
        else
        {
            xmov = -.35f;
        }


        Invoke("stopMoving", .35f);

        if (teleport)
        {
            if (repeat >= 3)
            {
                delaycount = 0;
               
                
                Invoke("reset", 1f);
            }

        }
        else {
            if (!(delaycount == difficulty))
            {

                delaycount++;
                Invoke("reset", .53f);
            }
            else
            {
                delaycount = 0;
                Invoke("reset", .63f);
            }
        }
    }



    // old Stuff
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
                Instantiate(sparks, new Vector2((transform.position.x + other.transform.position.x) / 2, (transform.position.y + other.transform.position.y * 2) / 3), Quaternion.identity);
                if (!superArmor)
                {
                    CancelInvoke("SMG");
                    CancelInvoke("NearKick");
                    CancelInvoke("airMove");
                    CancelInvoke("diveKicking");
                    CancelInvoke("isVulnerable");
                    CancelInvoke("painAnimEnd");

                    CancelInvoke("reset");
                    currentLife++;
                    if (currentLife>100)
                    {
                        player.gainLife();
                        Destroy(gameObject);
                    }

                    painAnimation = true;
                    stopMoving();
                    Invoke("painAnimEnd", .45f);
                    Invoke("IsVulnerable", .1f);
                    Invoke("reset", .6f);
                    pain = true;

                    delaycount = 0;


                    if (player.transform.position.x > transform.position.x)
                    {

                        xmov = -.1f;

                    }
                    else

                    {
                        xmov = .1f;
                    }


                    if (!other.gameObject.name.Equals("UpKick(Clone)") && !superArmor)
                    {
                        ymov = .2f;
                    }
                    deadly = false;

                    if (resetJump)
                    {
                        ResetJump();
                    }
                    else
                    {
                        Invoke("readyReset", 4.5f/difficulty+.4f);
                    }

                }

                else
                {
                    currentLife++;
                }
            }
        }
      
            
    }



    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name.Equals("Player"))
        {
        /*    if (deadly)
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
           if (transform.position.y  > other.transform.position.y + 2.3 * other.transform.localScale.y / 2)
            {
                if (!ground)
                {
                    Instantiate(dustCloud, new Vector2(transform.position.x, transform.position.y - .8f), Quaternion.identity);
                }
                
                ground = true;
                airShoot = false;
                ymov = 0;
                if (downKick)
                {

                    land = true;
                    stopMoving();
                    downKick = false;
                    deadly = false;
                    if (!(delaycount == difficulty))
                    {

                        delaycount++;
                        Invoke("reset", .2f);
                    }
                    else
                    {
                        delaycount = 0;
                        Invoke("reset", .4f);
                    }
                }
            }
        }

        if (other.gameObject.name.Equals("UpKick(Clone)"))
        {
            if (!superArmor)
            {
                transform.Translate(0, .5f * Time.deltaTime * 60, 0);
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
      
        
    

    // Update is called once per frame
    void FixedUpdate () {

       
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        if (!ground&&!teleport)
        {
            ymov -= .01f;
            if (ymov > 3)
            {
                ymov = 3;
            }
        }


        //The attack code
        if (!busy&&!pain)

        {
            cheat = 0;
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

         
            busy = true;
            repeat = 0;

            if (ground)
            {


                if (Mathf.Abs(player.transform.position.x - transform.position.x) > 19f)
                {
                    random = Random.Range(2, 5);
                    //far moves
                    
                    if (random <= 2)
                    {
                     

                        GroundShoot = true;

                        Invoke("SMG", .6f);
                    }
                    if (random == 3)
                    {

                        JumpingForward = true;

                        ymov = .4f;

                        if (player.transform.position.x > transform.position.x)
                        {
                            xmov = .15f;
                        }
                        else
                        {
                            xmov = -.15f;
                        }
                        Invoke("airMove", .6f);
                    }
                    if (random == 4)
                    {
                        if (currentLife > 40)
                        {
                            Instantiate(Smoke, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

                            teleport = true;
                            Invoke("Reappear", .5f);
                         
                            transform.position = new Vector3(0, -30, 0);
                        }
                        else
                        {
                            busy = false;
                        }
                    }

                    if (random == 0)
                    {
                      
                        grenade = true;

                        Invoke("grenadeToss", .4f);
                    }

                    }
                else if (Mathf.Abs(player.transform.position.x - transform.position.x) < 5) 
                {
                    //close attack
                  

                   

                    
                     
                      
                        Stab = true;

                        Invoke("NearKick", .3f);


                    
                }
                else
                {
                    //mid attack
                    

                    random = Random.Range(1, 4);

                    if (random == 1)
                    {
                       

                        Stab = true;

                        Invoke("NearKick", .3f);
                    }
                    if (random == 2)
                    {


                        JumpingBack = true;
                            ymov = .4f;

                            if (player.transform.position.x > transform.position.x)
                            {
                                xmov = -.2f;
                            }
                            else
                            {
                                xmov = .2f;
                            }
                        if (transform.position.x < -10 || transform.position.x > 10)
                        {
                            JumpingForward = true;
                            JumpingBack = false;
                            xmov = xmov * -1;

                        }
                            Invoke("airMove", .6f);
                        }
                    if (random == 3)
                    {
                        if (currentLife > 60)
                        {
                            Instantiate(Smoke, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

                            teleport = true;
                            Invoke("Reappear", .5f);
                            transform.position = new Vector3(0, -30, 0);
                        }
                        else
                        {
                            busy = false;
                        }
                    }



                }
            }
            else
            {
                busy = false;
            }
        }

        if (Stab && superArmor||GroundShoot)
        {

            if (right)
            {
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-5, -1.5f);

            }
            else
            {
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-5, -1.5f);
            }
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0.09f, -1.5f);
        }
        
        

        if (deadly)
        {
            if (Stab)
            {
                if (right)
                {
                    Instantiate(MyBox, new Vector2(transform.position.x + 3f, transform.position.y), Quaternion.identity);
                }
                else
                {
                    Instantiate(MyBox, new Vector2(transform.position.x - 3f, transform.position.y), Quaternion.identity);
                }
            }
            if (downKick)
            {
                if (right)
                {
                    Instantiate(MyBox, new Vector2(transform.position.x + 1f, transform.position.y-2.1f), Quaternion.identity);
                }
                else
                {
                    Instantiate(MyBox, new Vector2(transform.position.x - 1f, transform.position.y-2.1f), Quaternion.identity);
                }
            }
        }

        if (player.transform.position.x > transform.position.x && !right&&!busy)
        {
            flip();
            right = true;
        }
        else if (player.transform.position.x < transform.position.x && right&&!busy)
        {
            flip();
            right = false;
        }

        anim.SetBool("Busy", Rummage);
        anim.SetBool("GroundShoot", GroundShoot);
        anim.SetBool("Pain", painAnimation);
        anim.SetBool("Ground", ground);
        anim.SetBool("Stab", Stab);
        anim.SetBool("Ground", ground);
        anim.SetBool("Fair", JumpingForward);
        anim.SetBool("Bair", JumpingBack);
        
        anim.SetBool("airShoot", airShoot);
        anim.SetBool("diveKick", downKick);
        anim.SetBool("Grenade", grenade);
        anim.SetBool("Escape", EscapeAnim);
        anim.SetBool("land", land);

        transform.Translate(xmov, ymov, 0);
    }
}
