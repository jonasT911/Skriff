using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DPBtalkScript : MonoBehaviour
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

    int lineIndex = 0;
    int iteration = 0;
    string Quote = "See, I'm fine.~";
    string printedString = "";

    bool busy, speed = false;

    void switchCharacters(GameObject activeCharacter, bool speaking, GameObject activeTag, GameObject Inactive, bool left)
    {
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
        if (Input.anyKeyDown || lineIndex == 0)
        {
            if (!busy)
            {
                printedString = "";
                busy = true;
                iteration = 0;

                if (lineIndex == 8)
                {
                    SceneManager.LoadScene(nextLevel);
                }
                if (lineIndex == 9)
                {
                    switchCharacters(Autumn, true, tag3, tag1, true);
                    Quote = ("Yeah, that would be nice.");
                    type();
                }
                if (lineIndex == 8)
                {
                    switchCharacters(Royer, true, tag3, tag1, false);
                    Quote = ("If you keep fighting like that, then we can win this war in no time.");
                    type();
                }

                if (lineIndex == 7)
                {
                    switchCharacters(Autumn, true, tag3, tag1, true);
                    Quote = ("Awesome.");
                    type();
                }
                if (lineIndex == 6)
                {

                    switchCharacters(Royer, true, tag1, tag3, false);
                    Quote = ("Not to interupt your declaration of discontent, but the evac truck is here.");
                    type();
                }
                if (lineIndex == 5)
                {
                    switchCharacters(Autumn, true, tag3, tag1, true);
                    Quote = ("I hate that guy.");
                    type();
                }
                if (lineIndex == 4)
                {

                    clearCharPicture(MM);
                    Quote = "        ";
                    type();
                }
                if (lineIndex == 3)
                {

                    switchCharacters(MM, true, tag3, tag1, false);
                    Quote = ("I'll pass.");
                    type();

                }
                if (lineIndex == 2)
                {
                    switchCharacters(MM, true, tag3, tag1, false);
                    Quote = ("Hhmmmmmh...");
                    type();
                }
                if (lineIndex == 1)
                {

                    switchCharacters(Autumn, true, tag1, tag2, true);
                    Quote = ("There's plenty more where that came from.");
                    type();
                }
                else if (lineIndex == 0)
                {
                    switchCharacters(MM, true, tag3, tag2, false);
                    Quote = ("Ouch! That hurt!");
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
