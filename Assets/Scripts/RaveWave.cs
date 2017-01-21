using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaveWave : MonoBehaviour
{

    public float jumpInterval = 1000;
    public float jumpForce = 200;
    public Vector2 centerOfMass = new Vector2(0,0f);
    private float time;
    public bool isGrounded = false;


	// Use this for initialization
	void Start ()
	{
        GetComponent<Rigidbody2D>().centerOfMass = centerOfMass;
	    time = Time.realtimeSinceStartup;
	}

    public void OnCollisionStay2D(Collision2D collisionInfo)
    {
        Debug.Log("Is Grounded");
        isGrounded = true;
    }

    public  void OnCollisionEnter2D(Collision2D collisionInfo )
    {
        Debug.Log("Is Grounded");
        isGrounded = true;
    }

    public void OnCollisionExit2D(Collision2D collisionInfo)
    {
        Debug.Log("Is Not Grounded");
        isGrounded = false;
    }

    


    // Update is called once per frame
    void Update () {
        
	    if ((Time.realtimeSinceStartup > time + jumpInterval/1000) && isGrounded )
	    {
	        time = Time.realtimeSinceStartup;
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }
	    



    }
}
