using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public float 				moveSpeed 		= 6;	// how fast the player moves
	public GameObject			cane;					// Granny's Cane
	public CeilingController	ceiling;

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
		velocity 	 = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, 0).normalized * moveSpeed;
		rigidbody.MovePosition (rigidbody.position + velocity * Time.fixedDeltaTime);

		if (Input.GetAxisRaw ("Horizontal") == -1){
			spriteRenderer.flipX = true;
		} else if ( Input.GetAxisRaw ("Horizontal") == 1 ) {
			spriteRenderer.flipX = false;
		};

		// hit with cane god damnit
		if ( Input.GetAxisRaw ("Vertical") == 1){
			//cane.transform. = cane.transform.y + 2;
			//ceiling.goGoGadgetoWave( transform.position.x, 1, 7, 2000 );
			Debug.Log (transform.position.x);
		}
	}

	void OnTriggerEnter(Collider trigger) {
		
	}

	void OnTriggerExit(Collider trigger) {
		
	}

	void OnCollisionEnter(Collision collision)
	{
		
	}
}
