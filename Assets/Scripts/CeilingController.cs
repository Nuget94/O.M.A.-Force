using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CeilingController : MonoBehaviour
{
    public int NumberOfTiles = 21;
    public GameObject CeilingTilePrefab = null;
    
    private List<GameObject> ceiling = new List<GameObject>();
    private List<Wave> waves = new List<Wave>();

    // Use this for initialization
    void Start ()
    {
		buildCeiling();
        Wave w1 = new Wave();
        w1.durationMSec = 10000;
        w1.initialIntensity = 1.0f;
        w1.startTimeMSec = Time.fixedTime;
        w1.reach = 5.75f;
        waves.Add(w1);
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
            var newTile = Instantiate(CeilingTilePrefab, new Vector3(newPositionX, 0, 0), Quaternion.identity, transform);
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
                    tile.transform.position = new Vector3(originalPos.x, newY, originalPos.y);
                }
            }
        }
        waves = waves.Except(toRemove).ToList();
    }
}
