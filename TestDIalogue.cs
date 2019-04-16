using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestDIalogue : MonoBehaviour
{
    public Text text;
    public GameObject tag1;
        public GameObject tag2;
    string nextChar;

    int lineIndex = 0;
    int iteration = 0;
    string Quote="Or do I?~";
    string printedString="";

    bool busy = false;

    // Start is called before the first frame update
  void type()
    {
 nextChar = Quote.Substring(iteration,  1);

        if (!(nextChar.Equals("~")))
        {
           

            printedString = printedString + nextChar;

            iteration++;

            Invoke("type", .04f);
        }
        else
        {
            busy = false;
        }
        text.text = printedString;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (!busy)
            {
                printedString = "";
                busy = true;
                iteration = 0;
                if (lineIndex == 1)
                {


                    tag1.transform.position = new Vector2(-9.26f, -1.85f);
                    tag2.transform.position = new Vector2(-300, 0);

                    Quote = "You'll never know.~";

                    type();
                }
                else if (lineIndex==0)
                {
                    tag2.transform.position = new Vector2(10.37f, -1.85f);
                    tag1.transform.position = new Vector2(-300, 0);
                    type();
                }

                lineIndex++;
            }
        }
    }
}
