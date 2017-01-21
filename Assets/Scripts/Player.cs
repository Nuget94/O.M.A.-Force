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
    public int attackNumber = 0;
    public Transform grannyTransform;

    private bool startAttack4 = false;
    public Vector3 endPositionState1;
    public Vector3 endPositionState2Left;
    public Vector3 endPositionState2Right;
    private int stateAttack4 = 0; // 0 = aus 1 = auf dem Weg nach oben 2 auf dem weg nach links / rechts



    private  float timeWait = 0;
	// Use this for initialization
	void Start ()
	{
	    timeWait = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("left"))
        {
            if (this.gameObject.transform.position.x >= -6.3f)
            {
                //grannyTransform.Translate(new Vector3(-0.1f, 0, 0));
                this.gameObject.transform.Translate(new Vector3(-0.1f,0,0));
            }
        }

        if (Input.GetKey("right"))
        {
            if (this.gameObject.transform.position.x <= 7.5f)
            {
                //grannyTransform.Translate(new Vector3(0.1f, 0, 0));
                this.gameObject.transform.Translate(new Vector3(0.1f, 0, 0));
            }
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
        if (Input.GetKey("4"))
        {
            attackNumber = 3;
        }

        if (Input.GetKey("space"))
        {
            if (this.gameObject.transform.position.y <= -1.0f && timeWait < Time.realtimeSinceStartup)
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
        gameObject.transform.GetChild((1)).localPosition = new Vector3(0, 0, 0);
        gameObject.transform.GetChild((1)).GetChild(0).localPosition = new Vector3(0, 0, 0);
        gameObject.transform.GetChild((1)).GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        gameObject.transform.GetChild((1)).GetChild(0).GetComponent<Rigidbody2D>().angularVelocity = 0;
        gameObject.transform.GetChild((1)).GetChild(0).GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forceAttack3));
    }

    private void Attack3()
    {
        granny.Attack();
        timeWait = Time.realtimeSinceStartup + cooldownAttack3 / 1000;
        gameObject.transform.GetChild((2)).localPosition = new Vector3(0,0,0);
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

        
        trueWave.transform.localPosition = new Vector3(0, 0, 0);

        //left Sphere
        rightSphere.transform.localPosition = new Vector3(0, 0, 0);
        rightSphere.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        rightSphere.transform.GetComponent<Rigidbody2D>().angularVelocity = 0;

        //right Sphere
        leftSphere.transform.localPosition = new Vector3(0, 0, 0);
        leftSphere.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        leftSphere.transform.GetComponent<Rigidbody2D>().angularVelocity = 0;

        endPositionState1 = new Vector3(leftSphere.transform.position.x, leftSphere.transform.position.y + 0.15f);
        endPositionState2Left = new Vector3(leftSphere.transform.position.x - 5, leftSphere.transform.position.y + 0.15f);
        endPositionState2Right = new Vector3(leftSphere.transform.position.x + 5, leftSphere.transform.position.y + 0.15f);

        stateAttack4 = 1;
        startAttack4 = true;

    }

   

    private void FixedUpdate()
    {
        if (startAttack4)
        {
            GameObject trueWave = gameObject.transform.GetChild(3).gameObject;
            GameObject leftSphere = trueWave.transform.GetChild(0).gameObject;
            GameObject rightSphere = trueWave.transform.GetChild(1).gameObject;

            switch (stateAttack4)
            {
                case 0:
                    startAttack4 = false;
                    break;
                case 1:
                    float xVectorLeft = 0;
                    float yVectorLeft = 0;
                    float xVectorRight = 0;
                    float yVectorRight = 0;

                    if (leftSphere.transform.position.x < endPositionState1.x )
                    {
                        xVectorLeft = 0.1f;
                    }
                    if (leftSphere.transform.position.y < endPositionState1.y )
                    {
                        yVectorLeft = 0.1f;
                    }
                    if (rightSphere.transform.position.x < endPositionState1.x)
                    {
                        xVectorRight = 0.1f;
                    }
                    if (rightSphere.transform.position.y < endPositionState1.y)
                    {
                        yVectorRight = 0.1f;
                    }
                    
                    leftSphere.transform.position = new Vector3(xVectorLeft, yVectorLeft);
                    rightSphere.transform.position = new Vector3(xVectorRight, yVectorRight);
                    if ((leftSphere.transform.position.x > endPositionState1.x)&& (leftSphere.transform.position.y > endPositionState1.y)&& (rightSphere.transform.position.x > endPositionState1.x)&& (rightSphere.transform.position.y > endPositionState1.y))
                    {
                        stateAttack4 = 2;
                    }
                    
                    break;
                case 2:

                    xVectorLeft = 0;
                    yVectorLeft = 0;
                    xVectorRight = 0;
                    yVectorRight = 0;

                    if (leftSphere.transform.position.x < endPositionState1.x)
                    {
                        xVectorLeft = 0.1f;
                    }
                    if (leftSphere.transform.position.y < endPositionState1.y)
                    {
                        yVectorLeft = 0.1f;
                    }
                    if (rightSphere.transform.position.x < endPositionState1.x)
                    {
                        xVectorRight = 0.1f;
                    }
                    if (rightSphere.transform.position.y < endPositionState1.y)
                    {
                        yVectorRight = 0.1f;
                    }

                    leftSphere.transform.position = new Vector3(xVectorLeft, yVectorLeft);
                    rightSphere.transform.position = new Vector3(xVectorRight, yVectorRight);
                    if ((leftSphere.transform.position.x > endPositionState2Left.x) && (leftSphere.transform.position.y > endPositionState2Left.y) && (rightSphere.transform.position.x > endPositionState2Right.x) && (rightSphere.transform.position.y > endPositionState2Right.y))
                    {
                        stateAttack4 = 0;
                    }

                    break;
            }
            
        }

    }


}
