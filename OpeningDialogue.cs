using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningDialogue : MonoBehaviour
{
    public Text text;
    public GameObject tag1;
        public GameObject tag2;

    public GameObject Autumn;
    public GameObject Royer;
    public GameObject MM;

    public string nextLevel;
    string nextChar;

    int lineIndex = 0;
    int iteration = 0;
    string Quote="Look, I know you're worried, but I have to do this.~";
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

        if (speaking) {

            if (left == true)
            {
                activeTag.transform.position = new Vector2(-9.26f, -1.85f);
            }
            else
            {
                activeTag.transform.position = new Vector2(10.37f, -1.85f);
            }

            Inactive.transform.position = new Vector2(-300, 0);

        } }

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

                if (lineIndex == 7)
                {
                    SceneManager.LoadScene(nextLevel);
                }
                if (lineIndex == 6)
                {

                    switchCharacters(Autumn, true, tag1, tag2, true);
                    Quote = "I know.~";
                    type();
                }
                if (lineIndex == 5)
                {
                    Quote = "Be careful, this isn't some street brawl, it's war.~";
                    type();
                }
                if (lineIndex == 4)
                {

                    switchCharacters(Royer, true, tag2, tag1, false);


                    Quote = "You're ... ~";

                    type();
                }
                if (lineIndex == 3)
                {

                    switchCharacters(Autumn, true, tag1, tag2, true);
                    Quote = "It's not fair for me to sit this out just because I know you.";
                    type();
                }
                if (lineIndex == 2)
                {

                 
                    Quote = "No you don't. You know that.";

                    type();
                }
                if (lineIndex == 1)
                {

                    switchCharacters(Royer, true, tag2, tag1, false);
                    Quote = "*sigh*";

                    type();
                }
                else if (lineIndex==0)
                {

                    switchCharacters(Autumn, true, tag1, tag2, true);
                    Quote = "Look, I know you're worried, but I have to do this.";
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
