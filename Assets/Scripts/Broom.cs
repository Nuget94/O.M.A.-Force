using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : MonoBehaviour
{

    public float force = 100;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("left"))
        {
            if (this.gameObject.transform.position.x >= -9)
            {
                this.gameObject.transform.Translate(new Vector3(-0.1f,0,0));
            }
        }

        if (Input.GetKey("right"))
        {
            if (this.gameObject.transform.position.x <= 13.2f)
            {
                this.gameObject.transform.Translate(new Vector3(0.1f, 0, 0));
            }
        }

        if (Input.GetKey("space"))
        {
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force));
        }



    }
}
