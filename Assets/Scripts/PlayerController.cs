using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public float 				moveSpeed 		= 6;							// how fast the player moves

	private Vector3 			velocity;
	private float				currentSpeed;
	private new Rigidbody 		rigidbody;
	private new SpriteRenderer	spriteRenderer;


	void Start () {
		rigidbody 		= GetComponent<Rigidbody>();
		spriteRenderer	= GetComponent<SpriteRenderer> ();
	}

	void Update () {
		
	}

	void FixedUpdate() {		
		// Move the player around
		velocity 	 = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized * moveSpeed;
		rigidbody.MovePosition (rigidbody.position + velocity * Time.fixedDeltaTime);

		if (Input.GetAxisRaw ("Horizontal") == -1){
			spriteRenderer.flipX = true;
		} else if ( Input.GetAxisRaw ("Horizontal") == 1 ) {
			spriteRenderer.flipX = false;
		};
	}

	void OnTriggerEnter(Collider trigger) {
		
	}

	void OnTriggerExit(Collider trigger) {
		
	}

	void OnCollisionEnter(Collision collision)
	{
		
	}
}
