using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrannyController : MonoBehaviour
{

    public SoundCollection happyVoices;
    public SoundCollection angryVoices;
    public int voiceChance = 30;    //Chance to play the angry sound while attack


    private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	public void Attack()
    {
        Debug.Log("Attack start");
        if (Random.value < voiceChance / 100)
        {
            gameObject.GetComponent<AudioSource>().PlayDelayed(0.005f);
            angryVoices.playRandom();
        }
        
        anim.SetInteger ("State", 1);
		StartCoroutine( AttackDone() );
	}

	IEnumerator AttackDone()
	{
		yield return new WaitForSeconds(0.5f);
		anim.SetInteger ("State", 0);
	}
}
