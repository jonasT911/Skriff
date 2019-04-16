using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private float xmov, ymov,lifeGain=0;
    private bool nAttack, sideAttack, upAttack, ground, right, busy, freefall, downKick, invuln, slide, crouch,nAir,dodging,SideSlow,IsInDirectMode,JumpIsStale,isPaused = false;
    private float speed = .25f;
    private float playerJumpPower = 1150;
    public int life = 0;
    public int emitdust = 0;

    public GameObject UpKick;
    public GameObject SideAttack;
    public GameObject DownKickBox;
    public GameObject flipKickBox;
    public GameObject neutralAttack;
    public GameObject windmillHitbox;
    public GameObject dustCloud;
    public GameObject singleDust;
    public GameObject PauseSelect;


    private float recover = 1;

    private Animator anim;

    string[] testController;
    public AudioSource MCvoice;
    public AudioClip fist;
    public AudioClip Grunt;
    public AudioClip Sword;
    public AudioClip woosh;


    // Use this for initialization
    void Start()
    {
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        anim = GetComponent<Animator>();
        
        MCvoice = GetComponent<AudioSource>();
        CheckController();

    }

    private void StaleJump()
    {
        JumpIsStale = true;
    }
    private void CheckController()
    {
        if (Input.GetJoystickNames().Length > 0) // Auto switch if gamepad is activated.
        {
            if (Input.GetJoystickNames()[0].Length > 0)
            {
                if (!IsInDirectMode)
                {
                    IsInDirectMode = true; // toggle mode
                  
                }
            }
            else
            {
                if (IsInDirectMode)
                {
                    IsInDirectMode = false;
                }
            }
           
        }
      //  print(Input.GetAxis("HorizontalC")+"   " +Input.GetAxis("VerticalController"));
     //   print(IsInDirectMode);
        Invoke("CheckController", .5f);
    }

    private void OnCollisionStay2D(Collision2D other)
    {

        GameObject collidedWith = other.gameObject;



        if (transform.position.y - 1.89 > collidedWith.transform.position.y + 2.3 * collidedWith.transform.localScale.y / 2)
        {
            if (collidedWith.tag.Equals("Solid"))
            {

                if (ground == false)
                {
                    Instantiate(dustCloud, new Vector2(transform.position.x, transform.position.y-.9f), Quaternion.identity);
                }
                freefall = false;
                ground = true;

                if (downKick)
                {
                    downKick = false;
                    stopMoving();
                    Invoke("reset", .15f);
                }
                if (nAir)
                {
                    nAir = false;
                    busy = true;
                    CancelInvoke("NeutralAir");
                    CancelInvoke("reset");
                    stopMoving();
                    Invoke("reset", .05f);
                }
            }
        }

    }


    private void OnCollisionExit2D(Collision2D other)
    {
        GameObject collidedWith = other.gameObject;
        if (collidedWith.tag.Equals("Solid"))
        {
            ground = false;
        }
    }


    public void windmillConnect()
    {
        print("bounce");
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 15);
    }

    public int getLife()
    {
        return life;
    }

    public void gainLife()
    {
        lifeGain++;
        
        if (lifeGain > 3)
        {
            if (!(life == 0))
            {
                life --;
                lifeGain = 0;
            }
            else
            {
                lifeGain=4;
            }
        }
    }

    public float getLifeGain()
    {
        return lifeGain;
    }

    public void takeDamage()
    {
      
        MCvoice.clip = Grunt;
        MCvoice.Play(0);

       
        freefall = false;


         stopMoving();
         reset();
      
        CancelInvoke("reset");
        CancelInvoke("stopMoving");

        Invoke("vulnerable", 1f);
       
        Invoke("reset" ,.3f);
         busy = true;

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        invuln = true;
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 500);
        life++;

        recover = .1f;

        if (lifeGain> 3)
        {
            gainLife();
        }

       

        if (life >= 10)
        {
              print("you are dead");
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    private void vulnerable()
    {
        invuln = false;
    }

    public bool isInvulnerable()
    {

        return invuln;
    }

    public bool isRight()
    {
        return right;
    }
    public bool Diving()
    {
        return downKick;
    }

    void flipPlayer()
    {
        right = !right;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public void downSlide()
    {
        Instantiate(flipKickBox, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
       
    }
    public void sideSlash()
    {
        Instantiate(SideAttack, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }

    public void NeutralAir()
    {
        Instantiate(windmillHitbox, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }
    public void CanDodge()
    {
        dodging = false;
    }



    private void reset()
    {
      
        nAttack = false;
        sideAttack = false;
        downKick = false;
        upAttack = false;
        busy = false;
        slide = false;
        nAir = false;
        recover = 0;

    }

    private void SideReady()
    {
        SideSlow = false;
    }
    void stopMoving()
    {
        
        BoxCollider2D b = GetComponent<BoxCollider2D>();
        if (b != null)
        {
            b.size = new Vector3(3.75f, 9.7f, 1);
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -1.23f);

        }
        xmov = 0;
        ymov = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("p"))
        {
            if (isPaused)
            {
                isPaused = false;
                Time.timeScale = 1;
            }
            else
            {
                isPaused = true;
                Time.timeScale = 0;
                //Instantiate(PauseSelect, new Vector2(transform.position.x, 3.75f), Quaternion.identity);
            }

        }

        if (!isPaused)
        {

            BoxCollider2D b = GetComponent<BoxCollider2D>();



            if (!busy || invuln)
            {

                if (!ground)
                {
                    xmov = xmov * .95f;
                }
            }






            if (xmov > 0 && !right)
            {
                flipPlayer();
            }
            else if (xmov < 0 && right)
            {
                flipPlayer();
            }


            if (IsInDirectMode)
            {

                if (!busy || invuln)
                {
                    if (Mathf.Abs(Input.GetAxis("HorizontalC")) > .9f)
                    {
                        xmov = Input.GetAxis("HorizontalC") * speed * recover;
                    }
                    else
                    {
                        xmov = 0;
                    }
                }
                if (Input.GetButtonDown("dodgeC"))
                {
                    if (!(busy || nAir || dodging))
                    {

                        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                        stopMoving();
                        reset();

                        CancelInvoke("reset");
                        CancelInvoke("stopMoving");
                        CancelInvoke("vulnerable");
                        invuln = false;
                        recover = 1;

                        Instantiate(dustCloud, new Vector2(transform.position.x, transform.position.y - .9f), Quaternion.identity);
                        dodging = true;
                        busy = true;
                        if (right)
                        {
                            xmov = .9f;

                        }
                        else
                        {
                            xmov = -.9f;
                        }


                        Invoke("CanDodge", 0.6f);
                        Invoke("reset", 0.16f);
                        Invoke("stopMoving", 0.14f);

                    }

                }


                if (Input.GetButtonDown("attackC"))
                {
                    if (!busy && !nAir)
                    {
                        if (Input.GetAxis("VerticalController") > .7f)
                        {
                            if (!freefall)
                            {
                                Instantiate(UpKick, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

                                MCvoice.clip = Sword;
                                MCvoice.Play(0);

                                upAttack = true;
                                busy = true;
                                freefall = true;
                                ymov = .5f;

                                if (right)
                                {
                                    xmov = .1f;
                                }
                                else
                                {
                                    xmov = -.1f;
                                }




                                Invoke("reset", 0.5f);
                                Invoke("stopMoving", 0.4f);
                            }
                        }
                        else if (Mathf.Abs(Input.GetAxis("HorizontalC")) > .9f && !SideSlow && Mathf.Abs(Input.GetAxis("HorizontalC")) > Mathf.Abs(Input.GetAxis("VerticalController")))
                        {
                            Invoke("sideSlash", .05f);

                            MCvoice.clip = Sword;
                            MCvoice.Play(0);

                            ymov = 0;

                            if (Input.GetAxis("HorizontalC") > 0)
                            {

                                xmov = .4f;
                            }
                            else
                            {
                                xmov = -.4f;
                            }
                            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

                            print("sideAttack");
                            sideAttack = true;
                            busy = true;
                            SideSlow = true;

                            Invoke("reset", 0.35f);
                            Invoke("stopMoving", 0.25f);
                            Invoke("SideReady", .5f);

                        }
                        else if (Input.GetAxis("VerticalController") < -.7f)
                        {
                            if (!ground)
                            {
                                print("what");
                                Instantiate(DownKickBox, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

                                MCvoice.clip = woosh;
                                MCvoice.Play(0);

                                ymov = -1f;
                                if (right)
                                {
                                    xmov = .2f;
                                }
                                else
                                {
                                    xmov = -.2f;
                                }
                                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

                                downKick = true;
                                busy = true;

                            }
                            else
                            {
                                print("downslide");

                                CancelInvoke("vulnerable");
                                invuln = false;

                                ymov = 0;
                                if (right)
                                {
                                    xmov = .5f;
                                }
                                else
                                {
                                    xmov = -.5f;
                                }
                                Invoke("downSlide", .15f);
                                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

                                CancelInvoke("reset");


                                MCvoice.clip = woosh;
                                MCvoice.Play(0);

                                slide = true;
                                busy = true;
                                SideSlow = false;
                                if (b != null)
                                {
                                    b.size = new Vector3(3.75f, 7.31f, 1);
                                    gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -2.43f);
                                }


                                Invoke("reset", 0.7f);
                                Invoke("stopMoving", 0.4f);
                            }

                        }
                        else
                        {
                            if (ground)
                            {
                                Instantiate(neutralAttack, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

                                MCvoice.clip = Sword;
                                MCvoice.Play(0);

                                nAttack = true;
                                busy = true;

                                Invoke("reset", 0.2f);
                                Invoke("stopMoving", 0.1f);
                            }
                            else
                            {
                                print("neutral");
                                ymov = 0;
                                if (right)
                                {
                                    xmov = .1f;
                                }
                                else
                                {
                                    xmov = -.1f;
                                }

                                Invoke("NeutralAir", .2f);

                                nAir = true;

                                MCvoice.clip = woosh;
                                MCvoice.PlayDelayed(.1f);

                                Invoke("reset", 0.6f);

                            }
                        }

                    }
                }


                if (Input.GetButton("JumpController"))
                {


                    if (ground && (!busy || sideAttack) && !JumpIsStale)
                    {
                        Invoke("StaleJump", .5f);

                        Instantiate(dustCloud, new Vector2(transform.position.x, transform.position.y - .9f), Quaternion.identity);
                        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 21);
                        //   GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
                    }
                }
                else
                {
                    JumpIsStale = false;
                }
                if (!Input.GetButton("JumpController") && !nAir)
                {
                    float vSpeed = GetComponent<Rigidbody2D>().velocity.y;
                    if (vSpeed > 0)
                    {
                        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity / 4;
                    }
                }
                if (recover < 1)
                {
                    if (!(invuln && busy))
                        recover += .1f;
                }
                if (recover > 1)
                {
                    recover = 1;
                }


                if (Input.GetButton("Vertical") && Input.GetAxis("Vertical") < 0 && ground && !busy && xmov == 0)
                {
                    if (!(crouch || slide))
                    {
                        if (b != null)
                        {
                            b.size = new Vector3(3.75f, 7.31f, 1);
                            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -2.43f);

                        }
                    }
                    crouch = true;
                    xmov = 0;
                }
                else
                {
                    if (crouch && !slide)
                    {
                        if (b != null)
                        {
                            b.size = new Vector3(3.75f, 9.7f, 1);
                            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -1.2f);
                        }
                    }
                    crouch = false;
                }
            }
            else   //No controller                                                                                                      HERE          HERE         HERE
            {

                if (!busy || invuln)
                {
                    if (Mathf.Abs(Input.GetAxis("Horizontal")) > .05f)
                    {
                        xmov = Input.GetAxis("Horizontal") * speed * recover;
                    }
                    else
                    {
                        xmov = 0;
                    }
                }
                if (Input.GetButtonDown("dodge"))
                {
                    if (!(busy || nAir || dodging))
                    {

                        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                        stopMoving();
                        reset();

                        CancelInvoke("reset");
                        CancelInvoke("stopMoving");
                        CancelInvoke("vulnerable");
                        invuln = false;
                        recover = 1;

                        Instantiate(dustCloud, new Vector2(transform.position.x, transform.position.y - .9f), Quaternion.identity);
                        dodging = true;
                        busy = true;
                        if (right)
                        {
                            xmov = .9f;

                        }
                        else
                        {
                            xmov = -.9f;
                        }


                        Invoke("CanDodge", 0.6f);
                        Invoke("reset", 0.16f);
                        Invoke("stopMoving", 0.14f);

                    }

                }

                if (Input.GetButtonDown("attack"))
                {
                    if (!busy && !nAir)
                    {
                        if (Input.GetButton("Jump"))
                        {
                            if (!freefall)
                            {
                                Instantiate(UpKick, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

                                MCvoice.clip = Sword;
                                MCvoice.Play(0);

                                upAttack = true;
                                busy = true;
                                freefall = true;
                                ymov = .5f;

                                if (right)
                                {
                                    xmov = .1f;
                                }
                                else
                                {
                                    xmov = -.1f;
                                }




                                Invoke("reset", 0.5f);
                                Invoke("stopMoving", 0.4f);
                            }
                        }
                        else if (Input.GetButton("Horizontal") && !SideSlow)
                        {
                            Invoke("sideSlash", .05f);

                            MCvoice.clip = Sword;
                            MCvoice.Play(0);

                            ymov = 0;

                            if (Input.GetAxis("Horizontal") > 0)
                            {

                                xmov = .4f;
                            }
                            else
                            {
                                xmov = -.4f;
                            }
                            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

                            sideAttack = true;
                            busy = true;
                            SideSlow = true;

                            Invoke("reset", 0.35f);
                            Invoke("stopMoving", 0.25f);
                            Invoke("SideReady", .5f);

                        }
                        else if (Input.GetButton("Vertical") && Input.GetAxis("Vertical") < 0)
                        {
                            if (!ground)
                            {

                                Instantiate(DownKickBox, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

                                MCvoice.clip = woosh;
                                MCvoice.Play(0);

                                ymov = -1f;
                                if (right)
                                {
                                    xmov = .2f;
                                }
                                else
                                {
                                    xmov = -.2f;
                                }
                                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

                                downKick = true;
                                busy = true;

                            }
                            else
                            {

                                CancelInvoke("vulnerable");
                                invuln = false;

                                ymov = 0;
                                if (right)
                                {
                                    xmov = .5f;
                                }
                                else
                                {
                                    xmov = -.5f;
                                }
                                Invoke("downSlide", .15f);
                                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

                                CancelInvoke("reset");


                                MCvoice.clip = woosh;
                                MCvoice.Play(0);

                                slide = true;
                                busy = true;
                                SideSlow = false;
                                if (b != null)
                                {
                                    b.size = new Vector3(3.75f, 7.31f, 1);
                                    gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -2.43f);
                                }


                                Invoke("reset", 0.7f);
                                Invoke("stopMoving", 0.4f);
                            }

                        }
                        else
                        {
                            if (!Input.GetButton("Horizontal"))
                            {
                                if (ground)
                                {
                                    Instantiate(neutralAttack, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

                                    MCvoice.clip = Sword;
                                    MCvoice.Play(0);

                                    nAttack = true;
                                    busy = true;

                                    Invoke("reset", 0.2f);
                                    Invoke("stopMoving", 0.1f);
                                }
                                else
                                {

                                    ymov = 0;
                                    if (right)
                                    {
                                        xmov = .1f;
                                    }
                                    else
                                    {
                                        xmov = -.1f;
                                    }

                                    Invoke("NeutralAir", .2f);

                                    nAir = true;

                                    MCvoice.clip = woosh;
                                    MCvoice.PlayDelayed(.1f);

                                    Invoke("reset", 0.6f);
                                }
                            }
                        }

                    }
                }


                if (Input.GetButton("Jump"))
                {


                    if (ground && (!busy || sideAttack) && !JumpIsStale)
                    {
                        Invoke("StaleJump", .5f);

                        Instantiate(dustCloud, new Vector2(transform.position.x, transform.position.y - .9f), Quaternion.identity);
                        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 21);
                        //   GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
                    }
                }
                else
                {
                    JumpIsStale = false;
                }
                if (Input.GetButtonUp("Jump") && !nAir)
                {
                    float vSpeed = GetComponent<Rigidbody2D>().velocity.y;
                    if (vSpeed > 0)
                    {
                        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity / 4;
                    }
                }
                if (recover < 1)
                {
                    if (!(invuln && busy))
                        recover += .1f;
                }
                if (recover > 1)
                {
                    recover = 1;
                }


                if (Input.GetButton("Vertical") && Input.GetAxis("Vertical") < 0 && ground && !busy && xmov == 0)
                {
                    if (!(crouch || slide))
                    {
                        if (b != null)
                        {
                            b.size = new Vector3(3.75f, 7.31f, 1);
                            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -2.43f);

                        }
                    }
                    crouch = true;
                    xmov = 0;
                }
                else
                {
                    if (crouch && !slide)
                    {
                        if (b != null)
                        {
                            b.size = new Vector3(3.75f, 9.7f, 1);
                            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -1.23f);
                        }
                    }
                    crouch = false;
                }

            }


            anim.SetFloat("speed", System.Math.Abs(xmov));
            anim.SetBool("Jump", !ground && !slide);
            anim.SetBool("upAttack", upAttack);
            anim.SetBool("SideAttack", sideAttack);
            anim.SetBool("downKick", downKick);
            anim.SetBool("nAttack", nAttack);
            anim.SetBool("Crouch", crouch);
            anim.SetBool("sliding", slide);
            anim.SetBool("Nair", nAir);
            anim.SetBool("Hurt", invuln && busy);
            anim.SetBool("Dodging", dodging && busy);
        }


    }

    public void FixedUpdate()
    {
        if (dodging&&busy)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        if (ground && System.Math.Abs(xmov) > 0 && !busy)
        {
            if (emitdust > 10)
            {
                emitdust = 0;
                Instantiate(singleDust, new Vector2(transform.position.x, transform.position.y-.9f), Quaternion.identity);
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

        if (lifeGain > 0)
        {
           // lifeGain -= .0002f;
        }
        transform.Translate(xmov, ymov, 0);
    }
    }

