using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	public float forceAttack1 = 10000;
	public float forceAttack2 = 10000;
	public float forceAttack3 = 10000;
	public float forceAttack4 = 10000;

	public float cooldownAttack1 = 1000;
	public float cooldownAttack2 = 1000;
	public float cooldownAttack3 = 1000;
	public float cooldownAttack4 = 1000;
	public GrannyController granny;
	public int attackNumber = 2;
	public Transform grannyTransform;

	private bool startAttack4 = false;
	public Vector3 endPositionState1;
	public Vector3 endPositionState2Left;
	public Vector3 endPositionState2Right;
	public int stateAttack4 = 0; // 0 = aus 1 = auf dem Weg nach oben 2 auf dem weg nach links / rechts
	private float startTime;
	private float journeyLenght1;
	private float journeyLenght2;


	private float timeWait = 0;
	// Use this for initialization
	void Start()
	{
		timeWait = Time.realtimeSinceStartup;
	}

	// Update is called once per frame
	void Update()
	{

		if (FindObjectOfType<GameController>().IsRunning())
		{

			if (Input.GetAxisRaw("Horizontal") != 0)
			{
				float direction = Input.GetAxisRaw("Horizontal") / 10;
				this.gameObject.transform.Translate(new Vector3(direction, 0, 0));
				granny.GetComponent<Animator>().SetInteger("State", 2);
			}

			if (Input.GetButtonUp("Horizontal"))
			{
				granny.GetComponent<Animator>().SetInteger("State", 0);
			}

			if (Input.GetKey("1"))
			{
				attackNumber = 0;
			}
			if (Input.GetKey("2"))
			{
				attackNumber = 1;
			}
			if (Input.GetKey("3"))
			{
				attackNumber = 2;
			}
			//if (Input.GetKey("4"))
			//{
			//	attackNumber = 3;
			//}

			if (Input.GetButton("Fire1") && !Input.GetButton("Horizontal"))
			{
				if (timeWait < Time.realtimeSinceStartup)
				{
					switch (attackNumber)
					{
						case 0:
							Attack1();
							break;
						case 1:
							Attack2();
							break;
						case 2:
							Attack3();
							break;
						case 3:
							Attack4();
							break;
					}
				}
				else
				{
					Debug.Log(timeWait - Time.realtimeSinceStartup);
				}

			}
		}
	}

	private void Attack1()
	{
		granny.Attack();
		timeWait = Time.realtimeSinceStartup + cooldownAttack1 / 1000;
		Rigidbody2D[] bodies = gameObject.transform.GetChild(0).GetComponentsInChildren<Rigidbody2D>();
		foreach (Rigidbody2D body in bodies)
		{
			body.AddForce(new Vector2(0, forceAttack1));
		}
	}

	private void Attack2()
	{
		granny.Attack();
		timeWait = Time.realtimeSinceStartup + cooldownAttack2 / 1000;
		gameObject.transform.GetChild((1)).GetChild(0).localPosition = new Vector3(0, 0, 0);
		gameObject.transform.GetChild((1)).GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		gameObject.transform.GetChild((1)).GetChild(0).GetComponent<Rigidbody2D>().angularVelocity = 0;
		gameObject.transform.GetChild((1)).GetChild(0).GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forceAttack3));
	}

    private void Attack3()
    {
        granny.Attack();
        timeWait = Time.realtimeSinceStartup + cooldownAttack3 / 1000;
        gameObject.transform.GetChild((2)).GetChild(0).localPosition = new Vector3(0,0,0);
        gameObject.transform.GetChild((2)).GetChild(1).localPosition = new Vector3(0, 0, 0);
        gameObject.transform.GetChild((2)).GetChild(0).GetComponent<Rigidbody2D>().velocity= new Vector2(0,0);
        gameObject.transform.GetChild((2)).GetChild(0).GetComponent<Rigidbody2D>().angularVelocity = 0;
        gameObject.transform.GetChild((2)).GetChild(0).GetComponent<Rigidbody2D>().AddForce(new Vector2(forceAttack3/3, forceAttack3));
        gameObject.transform.GetChild((2)).GetChild(1).GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        gameObject.transform.GetChild((2)).GetChild(1).GetComponent<Rigidbody2D>().angularVelocity = 0;
        gameObject.transform.GetChild((2)).GetChild(1).GetComponent<Rigidbody2D>().AddForce(new Vector2(-forceAttack3/3, forceAttack3));
    }

    private void Attack4()
    {
        granny.Attack();
        timeWait = Time.realtimeSinceStartup + cooldownAttack4 / 1000;
        GameObject trueWave = gameObject.transform.GetChild(3).gameObject;
        GameObject leftSphere = trueWave.transform.GetChild(0).gameObject;
        GameObject rightSphere = trueWave.transform.GetChild(1).gameObject;

        
        trueWave.transform.position = new Vector3(0, 0, 0);

        //left Sphere
        rightSphere.transform.position = new Vector3(0, 0, 0);


        //right Sphere
        leftSphere.transform.position = new Vector3(0, 0, 0);

        endPositionState1 = new Vector3(leftSphere.transform.position.x, leftSphere.transform.position.y - 0.65f);
        endPositionState2Left = new Vector3(leftSphere.transform.position.x - 5, leftSphere.transform.position.y - 0.65f);
        endPositionState2Right = new Vector3(leftSphere.transform.position.x + 5, leftSphere.transform.position.y -0.65f);

        startTime = Time.time;
        journeyLenght1 = Vector3.Distance(leftSphere.transform.position, endPositionState1);
        journeyLenght2 = Vector3.Distance(leftSphere.transform.position, endPositionState2Left);
        stateAttack4 = 1;
        startAttack4 = true;

    }


	//private void FixedUpdate()
	//{
	//	if (FindObjectOfType<GameController>().IsRunning())
	//	{
	//		if (startAttack4)
	//		{
	//			GameObject trueWave = gameObject.transform.GetChild(3).gameObject;
	//			GameObject leftSphere = trueWave.transform.GetChild(0).gameObject;
	//			GameObject rightSphere = trueWave.transform.GetChild(1).gameObject;

	//			switch (stateAttack4)
	//			{
	//				case 0:
	//					startAttack4 = false;
	//					break;
	//				case 1:
	//					float xVectorLeft = 0;
	//					float yVectorLeft = 0;
	//					float xVectorRight = 0;
	//					float yVectorRight = 0;

	//					leftSphere.transform.position = Vector3.Lerp(leftSphere.transform.position, endPositionState1, (Time.time - startTime) * cooldownAttack4 / 1000 / journeyLenght1 / 4);
	//					rightSphere.transform.position = Vector3.Lerp(rightSphere.transform.position, endPositionState1, (Time.time - startTime) * cooldownAttack4 / 1000 / journeyLenght1 / 4);
	//					/*

	//					if (leftSphere.transform.position.x < endPositionState1.x )
	//					{
	//						xVectorLeft = 0.1f;
	//					}
	//					if (leftSphere.transform.position.y < endPositionState1.y )
	//					{
	//						yVectorLeft = 0.1f;
	//					}
	//					if (rightSphere.transform.position.x < endPositionState1.x)
	//					{
	//						xVectorRight = 0.1f;
	//					}
	//					if (rightSphere.transform.position.y < endPositionState1.y)
	//					{
	//						yVectorRight = 0.1f;
	//					}
                    
	//					leftSphere.transform.position = new Vector3(xVectorLeft, yVectorLeft)+leftSphere.transform.position;
	//					rightSphere.transform.position = new Vector3(xVectorRight, yVectorRight) + rightSphere.transform.position;*/
	//					if (Vector3.Distance(leftSphere.transform.position, endPositionState1) <= 0.05 && Vector3.Distance(rightSphere.transform.position, endPositionState1) <= 0.05)
	//					{
	//						stateAttack4 = 2;
	//					}

	//					break;
	//				case 2:

	//					xVectorLeft = 0;
	//					yVectorLeft = 0;
	//					xVectorRight = 0;
	//					yVectorRight = 0;

	//					leftSphere.transform.position = Vector3.Lerp(leftSphere.transform.position, endPositionState2Left, (Time.time - startTime) * cooldownAttack4 / 1000 / journeyLenght1 / 4 * 3);
	//					rightSphere.transform.position = Vector3.Lerp(rightSphere.transform.position, endPositionState2Right, (Time.time - startTime) * cooldownAttack4 / 1000 / journeyLenght1 / 4 * 3);

	//					/*
	//					if (leftSphere.transform.position.x < endPositionState1.x)
	//					{
	//						xVectorLeft = 0.1f;
	//					}
	//					if (leftSphere.transform.position.y < endPositionState1.y)
	//					{
	//						yVectorLeft = 0.1f;
	//					}
	//					if (rightSphere.transform.position.x < endPositionState1.x)
	//					{
	//						xVectorRight = 0.1f;
	//					}
	//					if (rightSphere.transform.position.y < endPositionState1.y)
	//					{
	//						yVectorRight = 0.1f;
	//					}

	//					leftSphere.transform.position = new Vector3(xVectorLeft, yVectorLeft)+leftSphere.transform.position;
	//					rightSphere.transform.position = new Vector3(xVectorRight, yVectorRight) + rightSphere.transform.position;*/
	//					if (Vector3.Distance(leftSphere.transform.position, endPositionState2Left) <= 0.05 && Vector3.Distance(rightSphere.transform.position, endPositionState2Right) <= 0.05)
	//					{
	//						stateAttack4 = 0;
	//					}

	//					break;
	//			}

	//		}
	//	}

	//}


}
