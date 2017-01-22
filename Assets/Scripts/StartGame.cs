﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnClick()
	{
		GameObject.FindObjectOfType<PlayerScorePersistenceManager>().Score.Reset();
		SceneManager.LoadScene("Game", LoadSceneMode.Single);
		gameObject.GetComponentInChildren<Text>().color = Color.white;
	}
}
