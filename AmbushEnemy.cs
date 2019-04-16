using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushEnemy : MonoBehaviour {

    public Ambush ambush;

   public int TurnNumber = 0;

	// Use this for initialization
	void Start () {
        ambush = GameObject.FindObjectOfType<Ambush>();
	}
	
	// Update is called once per frame
	void Update () {
		if (ambush.Wave == TurnNumber)
        {

        }
	}
}
