using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CeilingController : MonoBehaviour
{
    public int NumberOfTiles = 21;
    public GameObject CeilingTilePrefab = null;
    public float WaveReach = 5.5f;
    public float WaveInitialIntensity = 1f;
    
    private List<GameObject> ceiling = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
		buildCeiling();
        goGoGadgetoWave(0f, WaveInitialIntensity, WaveReach);
    }

    public void goGoGadgetoWave(float epiCenter, float initialIntensity, float reach)
    {
        foreach (var tile in ceiling)
        {
            var distanceToEpiCenter = Math.Abs(tile.transform.position.x - epiCenter);
            var remainingIntensity = interpolateIntensityByDistance(initialIntensity, reach, distanceToEpiCenter);
            tile.GetComponent<CeilingTile>().hitWithWave(remainingIntensity);
        }
        //Wave w = new Wave(epiCenter, initialIntensity, reach, durationMSec, Time.fixedTime);
        Debug.Log("Wave");
        //waves.Add(w);
    }

    private float interpolateIntensityByDistance(float initialIntensity, float reach, float distanceToEpiCenter)
    {
        return Mathf.Lerp(initialIntensity, 0, distanceToEpiCenter / reach);
    }

    private void buildCeiling()
    {
        SpriteRenderer sr = CeilingTilePrefab.GetComponent<SpriteRenderer>();
        var pixelsPerUnit = sr.sprite.pixelsPerUnit;
        var width = sr.sprite.texture.width;
        var size = width / pixelsPerUnit;
        var half = (float) Math.Ceiling(NumberOfTiles * 0.5f);
        for (int i = 0; i < NumberOfTiles; i++)
        {
            var newPositionX = (i - half) * size;
            var newTile = Instantiate(CeilingTilePrefab, transform.position, Quaternion.identity, transform);
            newTile.transform.localPosition = new Vector3(newPositionX, 0, 0);
            ceiling.Add(newTile);
        }
    }

	// Update is called once per frame
	void Update () {

	}

    void FixedUpdate()
    {
        
    }
}
