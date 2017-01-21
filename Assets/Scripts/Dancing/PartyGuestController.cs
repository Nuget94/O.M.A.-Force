using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyGuestController : MonoBehaviour
{

    public int NumGuests = 6;
    public float SpawnRange = 8;
    public Dancer PrefabDancer = null;

    public List<DanceMoveList> AvailableMoves = new List<DanceMoveList>();

	// Use this for initialization
	void Start () {
        
	    for (int i = 0; i < NumGuests; i++)
	    {
	        var newDancer = Instantiate(PrefabDancer, transform);
            var newPos = Random.Range(0, SpawnRange ) - SpawnRange * 0.5f;
            Debug.Log("Spawn Pos " + newPos);
	        var danceType = (int) Mathf.Floor(Random.Range(0, AvailableMoves.Count));
            DanceMoveAndTime moveAndTime = new DanceMoveAndTime();
	        moveAndTime.Wait = 0.0f;
	        moveAndTime.DanceMove = AvailableMoves[danceType];
            newDancer.transform.localPosition= new Vector3(newPos, 0, 0);
            newDancer.GetComponent<Dancer>().choreography.DanceMoves.Add(moveAndTime);
	    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
