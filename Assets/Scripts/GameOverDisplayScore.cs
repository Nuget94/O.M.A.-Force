using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverDisplayScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Text>().text = "You scored: " + FindObjectOfType<PlayerScorePersistenceManager>().Score.Score;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
