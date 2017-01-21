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
		
	}

	public void Attack()
    {
        gameObject.GetComponent<AudioSource>().PlayDelayed(0.005f);
        anim.SetInteger ("State", 1);
		StartCoroutine( AttackDone() );
	}

	IEnumerator AttackDone()
	{
		yield return new WaitForSeconds(0.5f);
		anim.SetInteger ("State", 0);
	}
}
