using UnityEngine;
using System.Collections;

public class PlayerScore {
	public float Score { get; private set; }

	public PlayerScore()
	{
		Reset();
	}

	public float AddScore(float points)
	{
		return Score += points;
	}

	public void Reset()
	{
		Score = 0f;
	}
}
