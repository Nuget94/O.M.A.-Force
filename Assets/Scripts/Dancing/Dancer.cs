﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

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
    private float leftGroundAt;
    private float lastX = 10000.0f; // some value of to the far right, so dancers face right initially

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
            checkWalkout();
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
            else
            {
                checkWalkout();
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
            leftGroundAt = Time.fixedTime;
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
            stopTurn(true);
            isDead = true;
            FindObjectOfType<GrannyController>().happyVoices.playRandom();
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

        if (!isGrounded && Time.fixedTime - leftGroundAt > 10.0f && !walker.isActiveAndEnabled)
        {
            // Actor is likely stuck, 10s past its scheduled walkout time. Just destroy it.
        //    Destroy(gameObject);
            //FindObjectOfType<Scoring>().guestStuck();
        }

        checkFacingDirection();
    }

    private Coroutine turnAround = null;
    private float rotationTarget = 0.0f;

    // rotation 0 means facing left, rotation 1 means facing right
    IEnumerator TurnToRotationY(float rotation)
    {
        float rotationStep = 20f;
        float timeStep = 0.006f;
        var sprite = GetComponentInChildren<SpriteRenderer>();
        rotationTarget = rotation;
        var ownRotation = sprite.transform.rotation.eulerAngles.y;
        if (ownRotation < 0.0f 
            || FloatComparer.AreEqual(ownRotation, rotation * 180f, rotationStep * 1.001f))
        {
            sprite.transform.localRotation = Quaternion.Euler(new Vector3(0f, rotation * 180f, 0f));
            turnAround = null;     
        }
        else
        {
            if (rotation < 0.5f)
            {
                // rotate towards 0
                sprite.transform.Rotate(Vector3.up, -5f, Space.Self);
            }
            else
            {
                // rotate towards 1
                sprite.transform.Rotate(Vector3.up, 5f, Space.Self);
            }           
            yield return new WaitForSeconds(timeStep);
            turnAround = StartCoroutine(TurnToRotationY(rotation));
        }
    }

    private void stopTurn(Boolean setToRotationTarget)
    {
        if (turnAround != null)
        {
            StopCoroutine(turnAround);
        }
        if (setToRotationTarget)
        {
            var sprite = GetComponentInChildren<SpriteRenderer>();
            sprite.transform.localRotation = Quaternion.AngleAxis(rotationTarget * 180f, Vector3.up);
        }
    }

    private void checkFacingDirection()
    {
        var walker = gameObject.GetComponent<Walker>();
        if (isDead && !walker.isActiveAndEnabled)
        {
            return;
        }

        var diff = transform.position.x - lastX;
        var threshold = 0.003f;
        if (diff > threshold && rotationTarget < 0.5f)
        {
            stopTurn(false);
            turnAround = StartCoroutine(TurnToRotationY(1.0f));
        }
        else if(diff < -threshold && rotationTarget > 0.5f)
        {
            stopTurn(false);
            turnAround = StartCoroutine(TurnToRotationY(0.0f));
        }
        lastX = transform.position.x;
    }

    private void checkWalkout()
    {
        var walker = gameObject.GetComponent<Walker>();
        if (walker.isActiveAndEnabled) return;

        if (isDead && Time.fixedTime > walkOutTime)
        {
            transform.rotation = Quaternion.identity;
            transform.position = new Vector3(transform.position.x, walker.walkingHeight, transform.position.z);
            walker.reset(true);
            walker.enabled = true;
        }
    }
    
    private void doTheMove(DanceMove danceMove)
    {
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(danceMove.Jump);
    }
}
