using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDecoration : MonoBehaviour
{
    public float jumpForce = 10;
    public float bpm = 120;

    private static float time;


	// Use this for initialization
	void Start ()
    {
        SetTime();
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
	            body.AddForce(new Vector2(0, jumpForce));
            }
            
	    }

	}

    void SetTime()
    {
        time = Time.realtimeSinceStartup + 1 / (bpm / 60);
    }   
    

}

