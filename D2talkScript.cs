using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class D2talkScript : MonoBehaviour
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
    string Quote="See, I'm fine.~";
    string printedString="";

    bool busy,speed = false;


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
        if (Input.anyKeyDown||lineIndex==0)
        {
            if (!busy)
            {
                printedString = "";
                busy = true;
                iteration = 0;

                if (lineIndex == 12)
                {
                    SceneManager.LoadScene(nextLevel);
                }
                if (lineIndex == 11)
                {
                    switchCharacters(MM, true, tag3, tag1, false);
                    Quote = "You first.";
                    type();

                }
                if (lineIndex == 10)
                {
                    switchCharacters(Autumn, true, tag1, tag3, true);
                    Quote = "Well I'm busy, so start talking!";
                    type();

                }
                if (lineIndex == 9)
                {
                    Quote = "That's no way to introduce yourself.";
                    type();
                }
                if (lineIndex == 8)
                {
                    switchCharacters(MM, true, tag3, tag1, false);
                    Quote = "*gasp*";
                    type();
                }
                if (lineIndex == 7)
                {
                   

                    Quote = "Who are you?";

                    type();
                }
                if (lineIndex == 6)
                {
                    switchCharacters(Autumn, true, tag1, tag2, true);

                    Quote = ". . .";

                    type();
                }
                if (lineIndex == 5)
                {
                    switchCharacters(MM, true, tag3, tag1, false);
                    clearCharPicture(Royer);

                    Quote = "Hello";

                    type();
                }
                if (lineIndex == 4)
                {

                    switchCharacters(Autumn, true, tag1, tag2, true);

                    Quote = "Wait a sec, someone's coming.";

                    type();
                }
                if (lineIndex == 3)
                {
                    switchCharacters(Royer, true, tag2, tag1, false);
                    Quote = "Then I think that our test was a success.";
                    type();

                }
                if (lineIndex == 2)
                {

                   
                    Quote = "But I have to say, this exosuit makes it almost too easy.";
                    type();
                }
                if (lineIndex == 1)
                {

                    switchCharacters(Autumn, true, tag1, tag2, true);

                    Quote = "Heh, thanks.";

                    type();
                }
                else if (lineIndex==0)
                {

                    switchCharacters(Royer, true, tag2, tag1, false);
                    Quote = "Excellent Work!";

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
