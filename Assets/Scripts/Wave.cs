using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Wave
{
    public float epiCenter = 0.0f;
    public float initialIntensity = 0f;
    public float reach = 0f;
    public long durationMSec = 0L;
    public float startTimeMSec = 0.0f;

    public Wave(float epiCenter, float initialIntensity, float reach, long durationMSec, float startTimeMSec)
    {
        this.epiCenter = epiCenter;
        this.initialIntensity = initialIntensity;
        this.reach = reach;
        this.durationMSec = durationMSec;
        this.startTimeMSec = startTimeMSec;
    }

    public float getTransformForTile(float distanceToEpicenter, float timeMSec)
    {
        var distanceAbs = Math.Abs(distanceToEpicenter);
        var tileStartOffsecMSec = distanceAbs / reach * durationMSec;
        var tileAllowedToStartAtTimeMSec = startTimeMSec + tileStartOffsecMSec;
        if (timeMSec < tileAllowedToStartAtTimeMSec)
        {
            return 0.0f;
        }

        var remainingDurationMSec = startTimeMSec + durationMSec - tileAllowedToStartAtTimeMSec;
        var timeFactor = (timeMSec - tileAllowedToStartAtTimeMSec) / remainingDurationMSec;
        var distanceFactor = 1 - distanceAbs / reach;
        if (distanceFactor < 0)
        {
            return 0f;
        }
        return (float) Math.Sin(Math.PI * 6 * timeFactor) * (1 - timeFactor) * distanceFactor * initialIntensity;
    }

    public Boolean noLongerEffective(float currentTimeMsec)
    {
        return currentTimeMsec - startTimeMSec > durationMSec;
    }
}

