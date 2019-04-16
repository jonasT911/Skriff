using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selector : MonoBehaviour
{
    public GameObject opt1;
    public GameObject opt2;
    public GameObject opt3;
    public GameObject opt4;


    private int selector=1;
    private int wait = 0;
    private const int TIME = 13;
    private string nextLevel;

    // Start is called before the first frame update
    void select()
    {
        if (selector == 1)
        {
            newGame();
        }
        if (selector == 2)
        {
            loadGame();
        }
        if (selector == 3)
        {
            options();
        }
        if (selector == 4)
        {
            credits();
        }
    }

    void newGame()
    {
        SceneManager.LoadScene("OpeningDialogue");
    }

    void loadGame()
    {
        wait = 10000;
        SaveGame.Load();
      

    }
    void options()
    {

    }
    void credits()
    {

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Input.GetAxisRaw("Submit") > 0 || Input.GetAxisRaw("attack")>0|| Input.GetAxisRaw("attackC")>0)
        {
            print("selected");
            select();
        }
    
        if (wait > 0)
        {
            wait--;
        }
        else
        {
            if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") < 0 || Input.GetAxis("HorizontalC") > 0 || Input.GetAxis("VerticalController") < 0)
            {
                if (selector < 4)
                {
                    selector++;
                    transform.Translate(0, -2.25f, 0);
                    print("down");
                    wait = TIME;
                }
            }
            if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("HorizontalC") < 0 || Input.GetAxis("VerticalController") > 0)
            {
                if (selector > 1)
                {
                    selector--;
                    transform.Translate(0, 2.25f, 0);
                    wait = TIME;
                }
            }
        }
    }
}
