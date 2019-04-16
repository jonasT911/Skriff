using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class D4talkScript : MonoBehaviour
{
    public Text text;
    public GameObject tag1;
    public GameObject tag2;
    public GameObject tag3;

    public GameObject Autumn;
    public GameObject Royer;
    public GameObject MM;

    public string nextLevel;
    string nextChar;

    int lineIndex = -1;
    int iteration = 0;
    string Quote = "See, I'm fine.~";
    string printedString = "";

    bool busy, speed = false;

    void switchCharacters(GameObject activeCharacter, bool speaking, GameObject activeTag, GameObject Inactive, bool left)
    {
        print(activeCharacter);
        if (left == true)
        {
            activeCharacter.transform.position = new Vector2(-10f, 4.72f);
        }
        else
        {
            activeCharacter.transform.position = new Vector2(10, 4.7f);
        }

        if (speaking)
        {

            if (left == true)
            {
                activeTag.transform.position = new Vector2(-9.26f, -1.85f);
            }
            else
            {
                activeTag.transform.position = new Vector2(10.37f, -1.85f);
            }

            Inactive.transform.position = new Vector2(-300, 0);

        }
    }

    void clearCharPicture(GameObject inactiveCharacter)
    {
        inactiveCharacter.transform.position = new Vector2(-301.83f, 2.02f);
    }


    void type()
    {
        if (iteration < Quote.Length)
        {
            nextChar = Quote.Substring(iteration, 1);


            printedString = printedString + nextChar;

            iteration++;

            if (!speed)
            {
                Invoke("type", .04f);
            }
            else
            {
                type();
            }
        }
        else
        {
            speed = false;
            Invoke("ReadyForNext", .1f);
        }
        text.text = printedString;
    }

    void ReadyForNext()
    {
        busy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown || lineIndex == -1)
        {
            if (!busy)
            {
                printedString = "";
                busy = true;
                iteration = 0;

                if (lineIndex == 9)
                {
                    SceneManager.LoadScene(nextLevel);
                }

                if (lineIndex == 8)
                {
                    switchCharacters(MM, true, tag3, tag1, false);
                    Quote = ("Oh boy, a fight!");
                    type();
                }
                if (lineIndex == 7)
                {

                    switchCharacters(Autumn, true, tag1, tag3, true);
                    Quote = ("Okay, you need to shut up now.");
                    type();
                }
                if (lineIndex == 6)
                {
                    switchCharacters(MM, true, tag3, tag1, false);
                    Quote = ("That's not very nice.");
                    type();
                }
                if (lineIndex == 5)
                {

                    switchCharacters(Autumn, true, tag1, tag3, true);
                    Quote = ("You know what, you can bite me!");
                    type();
                }
                if (lineIndex == 4) { 
                    switchCharacters(MM, true, tag3, tag1, false);
                Quote = ("Language!");
                type();
            }
                if (lineIndex == 3)
                {
                    switchCharacters(Autumn, true, tag1, tag3, true);
                    Quote = ("Oh, son of a b...");
                    type();
                   

                }
                if (lineIndex == 2)
                {
                    switchCharacters(MM, true, tag3, tag1, false);
                    clearCharPicture(Royer);
                    Quote = ("             ");
                    type();
                }
                if (lineIndex == 1)
                {

                    switchCharacters(Autumn, true, tag1, tag2, true);
                    Quote = ("I doubt it. But at least I didn't run into that nut again.");
                    type();
                }
                if (lineIndex == 0)
                {

                  
                    Quote = ("Pun not intended.");
                    type();
                }
                else if (lineIndex == -1)
                {
                    switchCharacters(Royer, true, tag2, tag3, false);
                    Quote = ("You're almost out of the woods.");
                    type();

                }
                    lineIndex++;
            }
            
            else
            {
                speed = true;
            }
        }
    }
}
