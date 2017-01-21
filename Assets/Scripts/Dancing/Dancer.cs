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
    private Boolean isGrounded = false;
    public float minDelayBeforeWalkOut = 1.5f;
    public float maxDelayBeforeWalkOut = 3.7f;
    private float groundedSince = 0.0f;
    private float walkOutTime = 0.0f;
	private Animator spriteAnim;

    public float jumpInterval = 1000;
    public float jumpForce = 200;
    public float maxAngle = 45;
    public bool isDead = false;
    public Vector2 centerOfMass = new Vector2(0, 0f);


    // Use this for initialization
    void Start ()
    {
        setupRigidBody();
		spriteAnim = GetComponentInChildren<Animator>();
	}

    public void setupRigidBody()
    {
        var rigidBody = GetComponent<Rigidbody2D>();
        if (rigidBody != null)
        {
            GetComponent<Rigidbody2D>().centerOfMass = centerOfMass;
			GetComponent<Rigidbody2D>().mass = 50;
			GetComponent<Rigidbody2D>().angularDrag = 6;
        }
		foreach (var collider in gameObject.GetComponents<Collider2D>())
		{
			collider.enabled = true;
		}
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionStay2D(Collision2D collisionInfo)
    {

        if (collisionInfo.transform.tag == "Dance Floor")
        {
            isGrounded = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.transform.tag == "Dance Floor")
        {
            isGrounded = true;
            groundedSince = Time.fixedTime;
            if (!isDead)
            {
                spriteAnim.SetInteger("State", 0);
            }
        }
        if (collisionInfo.transform.tag == "Ceiling")
        {
            Die();
        }
    }

    public void OnCollisionExit2D(Collision2D collisionInfo)
    {
        if (collisionInfo.transform.tag == "Dance Floor")
        {
            isGrounded = false;
            if (!isDead)
            {
                spriteAnim.SetInteger("State", 1);
            }
        }
    }

    void Die()
    {
        if (!isDead)
        {
            isDead = true;
            walkOutTime = Time.fixedTime + UnityEngine.Random.Range(minDelayBeforeWalkOut, maxDelayBeforeWalkOut);
            this.GetComponent<Rigidbody2D>().angularDrag = 0;
            GetComponent<Rigidbody2D>().centerOfMass = new Vector2(0, 0);
            spriteAnim.SetInteger("State", 2);
        }
    }


    void FixedUpdate()
    {
        var walker = gameObject.GetComponent<Walker>();
        var isWalking = walker.isActiveAndEnabled;
        if (isGrounded && !isDead && !isWalking)
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

        if (((this.transform.localEulerAngles.z < 360 - maxAngle && this.transform.localEulerAngles.z > 90) || (this.transform.localEulerAngles.z > maxAngle && this.transform.localEulerAngles.z < 270)) && isGrounded)
        {
            Die();
        }

        if (isDead && Time.fixedTime > walkOutTime)
        {
            transform.rotation = Quaternion.identity;
            walker.enabled = true;
        }

    }
    
    private void doTheMove(DanceMove danceMove)
    {
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(danceMove.Jump);
    }
}
