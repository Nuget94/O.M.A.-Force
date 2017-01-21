using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public float gameDuration = 180;

	private float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.realtimeSinceStartup > startTime + gameDuration)
		{
			SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
		}
	}
}
