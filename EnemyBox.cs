using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBox : MonoBehaviour
{

   
    private PlayerControl player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        Invoke("destroy", .05f);
    }
    private void destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name.Equals("Player"))
        {
         
                if (!player.isInvulnerable())
                {
                   
                    player.takeDamage();
                }
            }
        }
    }
