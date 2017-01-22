using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lichterkette : InterpolatingLight
{
    public List<Sprite> lights = new List<Sprite>();

    private int lightIdx = 0;

    public override void nextColor()
    {
        nextLight();
        base.nextColor();
    }

    void nextLight()
    {
        lightIdx++;
        if (lightIdx >= lights.Count)
        {
            lightIdx -= lights.Count;
        }
        GetComponent<SpriteRenderer>().sprite = lights[lightIdx];
    }
}
