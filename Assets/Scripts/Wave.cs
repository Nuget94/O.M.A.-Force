using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Wave
{
    public float epiCenter = 0.0f;
    public float initialIntensity = 0f;
    public float reach = 0f;
    public float speed = 0f;
    public long durationMSec = 0L;
    public float startTimeMSec = 0.0f;

    public float getTransformForTile(float distanceToEpicenter, float timeMSec)
    {
        var distanceFactor = reach - Math.Abs(distanceToEpicenter);
        if (distanceFactor < 0)
        {
            return 0.0f;
        }
        var timeFactor = 1 - (timeMSec - startTimeMSec) / durationMSec;
        var height = (float) Math.Sin(distanceToEpicenter + Math.PI * timeFactor) * initialIntensity * timeFactor;
        return height;
    }

    public Boolean noLongerEffective(float currentTimeMsec)
    {
        return currentTimeMsec - startTimeMSec > durationMSec;
    }
}

