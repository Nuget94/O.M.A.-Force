using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CeilingController : MonoBehaviour
{
    public int NumberOfTiles = 21;
    public GameObject CeilingTilePrefab = null;
    public long WaveDurationMSec = 1000;
    public float WaveReach = 5.5f;
    public float WaveInitialIntensity = 1f;
    
    private List<GameObject> ceiling = new List<GameObject>();
    private List<Wave> waves = new List<Wave>();

    // Use this for initialization
    void Start ()
    {
		buildCeiling();
    }

    public void goGoGadgetoWave(float epiCenter, float initialIntensity, float reach, long durationMSec)
    {
        Wave w = new Wave(epiCenter, initialIntensity, reach, durationMSec, Time.fixedTime);
        waves.Add(w);
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
            var newTile = Instantiate(CeilingTilePrefab, transform.position + new Vector3(newPositionX, 0, 0), Quaternion.identity, transform);
            ceiling.Add(newTile);
        }
    }

	// Update is called once per frame
	void Update () {

	}

    void FixedUpdate()
    {
        var timeMSec = Time.fixedTime * 1000f;
        var toRemove = new List<Wave>();
        foreach (var wave in waves)
        {
            if (wave.noLongerEffective(timeMSec))
            {
                toRemove.Add(wave);
            }
            else
            {
                foreach (var tile in ceiling)
                {
                    var originalPos = tile.transform.position;
                    var positionToEpiCenter = tile.transform.position.x - wave.epiCenter;
                    var newY = wave.getTransformForTile(tile.transform.position.x, timeMSec);
                    tile.transform.position = transform.position + new Vector3(originalPos.x, newY, originalPos.z);
                }
            }
        }
        waves = waves.Except(toRemove).ToList();
    }
}
