using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DanceMoveAndTime
{
    public float Wait;
    public DanceMove DanceMove;
}

[System.Serializable]
public class Choreography
{
    public List<DanceMoveAndTime> DanceMoves;

    private int danceMoveIdx = 0;
    private DanceMoveAndTime cachedNextMove = null;

    public void everybodyDanceNow()
    {
        var currentMove = DanceMoves[danceMoveIdx];
        if (currentMove.DanceMove.hasNext())
        {
            currentMove.DanceMove.nextIdx();
            cachedNextMove = currentMove;
        }
        else
        {
            currentMove.DanceMove.reset();
            nextIdx();
            cachedNextMove = DanceMoves[danceMoveIdx];
        }
    }

    private void nextIdx()
    {
        var newIdx = danceMoveIdx + 1;
        if (newIdx >= DanceMoves.Count)
        {
            newIdx -= DanceMoves.Count;
        }
        danceMoveIdx = newIdx;
        Debug.Log("New Dance Idx is " + danceMoveIdx);
    }

    public DanceMoveAndTime nextMove()
    {
        if (cachedNextMove == null)
        {
            cachedNextMove = DanceMoves[0];
        }
        return determineMoveOrNestedMove(cachedNextMove);
    }

    private DanceMoveAndTime determineMoveOrNestedMove(DanceMoveAndTime danceMove)
    {
        var nextMove = danceMove.DanceMove.nextMove();
        if (nextMove == null)
        {
            return danceMove;
        }
        return nextMove;
    }
}

public class Dancer : MonoBehaviour
{
    public Choreography choreography;
    private Boolean isGrounded = true;
    private float groundedSince = 0.0f;

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionStay2D(Collision2D collisionInfo)
    {
        isGrounded = true;
    }

    public void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        isGrounded = true;
        groundedSince = Time.fixedTime;
    }

    public void OnCollisionExit2D(Collision2D collisionInfo)
    {
        isGrounded = false;
    }

    void FixedUpdate()
    {
        if (isGrounded)
        {
            var elapsedTime = Time.fixedTime - groundedSince;
            var nextMove = choreography.nextMove();
            if (elapsedTime > nextMove.Wait)
            {
                choreography.everybodyDanceNow();
                doTheMove(nextMove.DanceMove);
                // need to set the time here again in case the next FixedUpdate comes before the dancer has 
                // left the ground.
                groundedSince = Time.fixedTime;
            }
        }
    }
    
    private void doTheMove(DanceMove danceMove)
    {
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(danceMove.Jump);
    }
}
