using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDecoration : MonoBehaviour
{
    public float jumpForce = 100;
    public int sideJumpForce = 100;
    public float bpm = 120;
    public float beatDelay = 1;

    private static float time;
    private static System.Random rndm = new System.Random();


	// Use this for initialization
	void Start ()
	{
	    time = Time.realtimeSinceStartup + beatDelay / 1000;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (time < Time.realtimeSinceStartup)
	    {
            SetTime();
	        Rigidbody2D[] bodies = GetComponentsInChildren<Rigidbody2D>();
	        foreach (Rigidbody2D body in bodies)
	        {
	            int rndmNumber = rndm.Next(0, sideJumpForce * 2) - sideJumpForce;
                Debug.Log(rndmNumber);
	            body.AddForce(new Vector2(rndmNumber, jumpForce));
            }
            
	    }

	}

    void SetTime()
    {
        time = Time.realtimeSinceStartup + 1 / (bpm / 60);
    }   
    

}

