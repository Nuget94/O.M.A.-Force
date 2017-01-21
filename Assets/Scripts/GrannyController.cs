using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrannyController : MonoBehaviour
{

    public SoundCollection happyVoices;
    public SoundCollection angryVoices;

    private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	public void Attack()
    {
        gameObject.GetComponent<AudioSource>().PlayDelayed(0.005f);
        angryVoices.playRandom();
        anim.SetInteger ("State", 1);
		StartCoroutine( AttackDone() );
	}

	IEnumerator AttackDone()
	{
		yield return new WaitForSeconds(0.5f);
		anim.SetInteger ("State", 0);
	}
}
