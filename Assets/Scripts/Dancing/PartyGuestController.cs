using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PartyGuestController : MonoBehaviour
{

    public int NumGuests = 6;
    public float SpawnRange = 8;
    public GameObject externalSpawnPoint = null;
    public int maxGuests = 5;
    public List<Dancer> PrefabDancers = new List<Dancer>();

    public List<DanceMoveList> AvailableMoves = new List<DanceMoveList>();

    private float timeOfNextSpawn = 4f;
    private Boolean cheerSoundScheduled = false;

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

        var prefabIdx = Random.Range(0, PrefabDancers.Count);
        var prefab = PrefabDancers[prefabIdx];
        var newDancer = Instantiate(prefab, transform);
        var walker = newDancer.GetComponent<Walker>();
        walker.reset(false);
        walker.enabled = true;
        newDancer.transform.position = externalSpawnPoint.transform.position + Vector3.up * walker.walkingHeight;
        newDancer.GetComponentInChildren<SpriteRenderer>().transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
    }

	// Use this for initialization
	void Start () {
	}

	public void SpawnInitial()
	{
		for (int i = 0; i < NumGuests; i++)
		{
			var prefabIdx = Random.Range(0, PrefabDancers.Count);
			var prefab = PrefabDancers[prefabIdx];
			var newDancer = Instantiate(prefab, transform);
			var newPos = Random.Range(0, SpawnRange) - SpawnRange * 0.5f;
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
		if (FindObjectOfType<GameController>().IsRunning())
		{
			if (Time.fixedTime > timeOfNextSpawn)
			{
				spawnExternal();
				timeOfNextSpawn = Time.fixedTime + Random.Range(3f, 6f);
			}

			checkCheerSound();
		}
    }

    void checkCheerSound()
    {
        if (cheerSoundScheduled) return;
        if (countActiveDancers() >= maxGuests * 0.8f)
        {
            StartCoroutine(ScheduleCheer());
        }
    }

    IEnumerator ScheduleCheer()
    {
        cheerSoundScheduled = true;
        var cheerDelay = Random.Range(3f, 5f);
        var soundCollection = gameObject.GetComponentInChildren<SoundCollection>();
        soundCollection.playRandomDelayed(cheerDelay);
        yield return new WaitForSeconds(cheerDelay);
        var bgMusic = gameObject.GetComponentInChildren<BackgroundMusic>().GetComponent<AudioSource>();
        bgMusic.volume -= 0.2f;
        yield return new WaitForSeconds(7.5f);
        cheerSoundScheduled = false;
        bgMusic.volume += 0.2f;
    }
}
