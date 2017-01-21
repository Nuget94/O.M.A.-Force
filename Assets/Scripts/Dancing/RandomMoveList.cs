using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveList : DanceMove {

    public List<DanceMove> availableMoves = new List<DanceMove>();
    public float minWait = 0f;
    public float maxWait = 1f;

    public override bool hasNext()
    {
        return true;
    }

    public override DanceMoveAndTime nextMove()
    {
        var nextMoveIdx = Random.Range(0, availableMoves.Count);
        var wait = Random.Range(minWait, maxWait);
        var move = new DanceMoveAndTime();
        move.Wait = wait;
        move.DanceMove = availableMoves[nextMoveIdx];
        return move;
    }

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
