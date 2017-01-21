using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrannyController : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
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
	}

	public void attack(){
		anim.SetInteger ("State", 1);
		StartCoroutine( attackDone() );
	}

	IEnumerator attackDone()
	{
		yield return new WaitForSeconds(0.5f);
		anim.SetInteger ("State", 0);
	}
}
