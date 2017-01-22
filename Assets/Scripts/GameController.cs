using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private string gameState = "none";

	// Use this for initialization
	void Start () {
		StartGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartTutorial()
	{
		gameState = "tutorial";
	}

	public bool IsTutorial()
	{
		return gameState == "tutorial";
	}

	public void StartGame()
	{
		gameState = "running";
		FindObjectOfType<GameOver>().Reset();
		FindObjectOfType<PartyGuestController>().SpawnInitial();
	}

	public void GameOver()
	{
		gameState = "gameover";
	}

	public bool IsRunning()
	{
		return gameState == "running";
	}
}
