using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public float gameDuration = 180;
    public GameObject gameTimeText;

	private float startTime;

	// Use this for initialization
	void Start () {
		Reset();
	}
	
	// Update is called once per frame
	void Update () {
		if (FindObjectOfType<GameController>().IsRunning())
		{
			if (Time.realtimeSinceStartup > startTime + gameDuration)
			{
				SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
			}
			else
			{
				gameTimeText.GetComponent<GameTime>().UpdateTime(gameDuration + startTime - Time.realtimeSinceStartup);
			}
		}
	}

	public void Reset()
	{
		startTime = Time.realtimeSinceStartup;
	}
}
