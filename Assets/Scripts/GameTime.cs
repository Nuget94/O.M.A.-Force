using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateTime(float leftTime)
    {
        string formatedGameTime = string.Format("{0}:{1:00}", (int)leftTime / 60, (int)leftTime % 60); 
        gameObject.GetComponent<Text>().text = "Game Time Left: " + formatedGameTime;
    }

}
