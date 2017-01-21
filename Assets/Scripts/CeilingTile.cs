using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingTile : MonoBehaviour {

    List<Wave> waves = new List<Wave>();

    public void hitWithWave(float intensity)
    {
        Wave w = new Wave();
        w.startTime = Time.fixedTime;
        w.duration = 1.04f * intensity;
        w.intensity = intensity;
        waves.Add(w);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        var currentTime = Time.fixedTime;
        var newTranslationY = 0.0f;
        var newIntensities = new List<Wave>();
        foreach (var wave in waves)
        {
            var newIntensity = wave.intensity * 0.97f;
            var timeFactor = (currentTime - wave.startTime) / wave.duration;
            if (newIntensity > 0.05f && timeFactor < 1f)
            {
                wave.intensity = newIntensity;
                newIntensities.Add(wave);
            }
        }
        foreach (var remainingWave in newIntensities)
        {
            var timeFactor = (currentTime - remainingWave.startTime) / remainingWave.duration;
            newTranslationY = Mathf.Sin(4 * Mathf.PI * timeFactor) * remainingWave.intensity;
        }
        transform.localPosition = new Vector3(transform.localPosition.x, newTranslationY, transform.localPosition.z);
        waves = newIntensities;
    }
}
