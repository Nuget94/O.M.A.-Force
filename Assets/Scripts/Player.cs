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
    private Transform startPosition;



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

        startPosition = leftSphere.transform;


        startAttack4 = true;

    }

   

    private void FixedUpdate()
    {
        if (startAttack4)
        {
            GameObject trueWave = gameObject.transform.GetChild(3).gameObject;
            GameObject leftSphere = trueWave.transform.GetChild(0).gameObject;
            GameObject rightSphere = trueWave.transform.GetChild(1).gameObject;


            leftSphere.transform.position = Vector3.Lerp(startPosition.position ,
                new Vector3(startPosition.position.x, startPosition.position.y + 0.35f), cooldownAttack4 / 4);
            leftSphere.transform.position = Vector3.Lerp(startPosition.position,
                new Vector3(startPosition.position.x - 5, startPosition.position.y), cooldownAttack4 / 4 * 3);
            rightSphere.transform.position = Vector3.Lerp(startPosition.position,
                new Vector3(startPosition.position.x, startPosition.position.y + 0.35f), cooldownAttack4 / 4);
            rightSphere.transform.position = Vector3.Lerp(startPosition.position,
                new Vector3(startPosition.position.x + 5, startPosition.position.y), cooldownAttack4 / 4 * 3);
        }

    }


}
