using UnityEngine;

public class CameraScript : MonoBehaviour {

    private float xpos, ypos,oldHealth,repeat = 0;
    public float maxX, minX, maxY, minY,xRand,yRand;

    private PlayerControl player;
	// Use this for initialization
	void Start () {

        player = GameObject.FindObjectOfType<PlayerControl>();
	}

    void screenShake()
    {
       
        xRand = Random.Range(-.14f, .14f);
        yRand = Random.Range(-.09f, .19f);
        if (repeat < 20)
        {
            repeat += 1;
            Invoke("screenShake", .01f);
        }
        else
        {
            xRand = 0;
            yRand = 0;

            repeat = 0;
        }
    }
	
	// Update is called once per frame
	void LateUpdate () {
        xpos = player.transform.position.x;
        ypos = player.transform.position.y;


        if (xpos > maxX)
        {
            xpos = maxX;
        }
        else if(xpos<minX)
        {
            xpos = minX;
        }

        if (ypos < minY)
        {
            ypos = minY;
        }
        else if (ypos>maxY)
        {
            ypos = maxY;
        }
        gameObject.transform.position=new Vector3(xpos+xRand, yRand+ypos/2, transform.position.z);


      if (oldHealth < player.getLife())
        {
            screenShake();
        }
        oldHealth = player.getLife();
       
	}
}
