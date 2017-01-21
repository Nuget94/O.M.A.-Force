using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyGuestController : MonoBehaviour
{

    public int NumGuests = 6;
    public float SpawnRange = 8;
    public GameObject externalSpawnPoint = null;
    public int maxGuests = 5;
    public Dancer PrefabDancer = null;

    public List<DanceMoveList> AvailableMoves = new List<DanceMoveList>();

    private float timeOfNextSpawn = 4f;

    private int countActiveDancers()
    {
        int numDancers = 0;
        var dancers = FindObjectsOfType<Dancer>();
        foreach (var dancer in dancers)
        {
            Walker walker = dancer.gameObject.GetComponent<Walker>();
            if (walker.isActiveAndEnabled && walker.isLeaving) continue;
            if (dancer.isDead) continue;

            numDancers++;
        }
        return numDancers;
    }

    void spawnExternal()
    {
        if (countActiveDancers() >= maxGuests || externalSpawnPoint == null)
        {
            return;
        }

        var newDancer = Instantiate(PrefabDancer, transform);
        var walker = newDancer.GetComponent<Walker>();
        walker.reset(false);
        walker.enabled = true;
        newDancer.transform.position = externalSpawnPoint.transform.position;
    }

	// Use this for initialization
	void Start () {
        
	    for (int i = 0; i < NumGuests; i++)
	    {
	        var newDancer = Instantiate(PrefabDancer, transform);
            var newPos = Random.Range(0, SpawnRange ) - SpawnRange * 0.5f;
			newDancer.transform.localPosition = new Vector3(newPos, 0, 0);
			InitializeWithRandomDanceMove(newDancer.GetComponent<Dancer>());
	    }
	}

	public void InitializeWithRandomDanceMove(Dancer dancer)
	{
		int danceType = Random.Range(0, AvailableMoves.Count);
		DanceMoveAndTime moveAndTime = new DanceMoveAndTime();
		moveAndTime.Wait = 0.0f;
		moveAndTime.DanceMove = AvailableMoves[danceType];
		dancer.choreography.DanceMoves.Clear();
		dancer.choreography.DanceMoves.Add(moveAndTime);
	}

    void FixedUpdate()
    {
        if (Time.fixedTime > timeOfNextSpawn)
        {
            spawnExternal();
            timeOfNextSpawn = Time.fixedTime + Random.Range(3f, 6f);
        }
    }
}
