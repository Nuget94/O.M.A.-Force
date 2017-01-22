using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public float gameDuration = 180;
    public GameObject gameTimeText;

	private float startTime;
	private GameObject uiCanvas = null;
	private GameObject gameOverCanvas = null;

	// Use this for initialization
	void Start () {
		Reset();

		uiCanvas = GameObject.Find("UI Canvas");
		gameOverCanvas = GameObject.Find("Game Over Canvas");;

		uiCanvas.SetActive(true);
		gameOverCanvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (FindObjectOfType<GameController>().IsRunning())
		{
			if (Time.realtimeSinceStartup > startTime + gameDuration)
			{
				uiCanvas.SetActive(false);
				gameOverCanvas.SetActive(true);

				foreach (GameObject taggedObject in GameObject.FindGameObjectsWithTag("Party Guest"))
				{
					Destroy(taggedObject);
				}

				foreach (GameObject taggedObject in GameObject.FindGameObjectsWithTag("Attack"))
				{
					Destroy(taggedObject);
				}

				foreach (MoveDecoration moveDecoration in GameObject.FindObjectsOfType<MoveDecoration>())
				{
					moveDecoration.enabled = false;
				}

				GameObject.Find("granny").GetComponent<Animator>().SetInteger("State", 0);
				Destroy(GameObject.Find("granny"));

				FindObjectOfType<GameController>().GameOver();

				FindObjectOfType<PartyGuestController>().SpawnCredits();
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
