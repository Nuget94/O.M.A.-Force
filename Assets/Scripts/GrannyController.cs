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

    void FixedUpdate()
    {
        var grannyVoices = gameObject.GetComponentInChildren<SoundCollection>();
        if (!grannyVoices.isPlaying)
        {
            StartCoroutine(Grumble());
        }
    }

	public void Attack()
    {
        gameObject.GetComponent<AudioSource>().PlayDelayed(0.005f);
        var grannyVoices = gameObject.GetComponentInChildren<SoundCollection>();
        grannyVoices.playRandom();
        anim.SetInteger ("State", 1);
		StartCoroutine( AttackDone() );
	}

	IEnumerator AttackDone()
	{
		yield return new WaitForSeconds(0.5f);
		anim.SetInteger ("State", 0);
	}

    IEnumerator Grumble()
    {
        yield return new WaitForSeconds(Random.Range(3f, 9f));
        var grannyVoices = gameObject.GetComponentInChildren<SoundCollection>();
        grannyVoices.playRandom();
    }
}
