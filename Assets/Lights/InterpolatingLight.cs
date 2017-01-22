using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InterpolatingLight : MonoBehaviour {

    public List<Color> AvailableColors = new List<Color>();
    public float transitionDuration = 4f;
    public float idleDuration = 1f;

    private float transitionStartTime = 0f;
    private Boolean isIdling = false;
    private float idlingSince = 0f;
    private int currentColorIdx = 0;
    private int newColorIdx = 1;

    void Start()
    {
        nextColor();
    }

    public virtual void nextColor()
    {
        isIdling = false;
        transitionStartTime = Time.fixedTime;
        currentColorIdx = this.newColorIdx;
        int randomColorIdx = currentColorIdx;
        while (randomColorIdx == currentColorIdx)
        {
            randomColorIdx = Random.Range(0, AvailableColors.Count);
        }
        newColorIdx = randomColorIdx;
    }

    void FixedUpdate()
    {
        var percentage = (Time.fixedTime - transitionStartTime) / transitionDuration;
        if (percentage > 1)
        {
            if (isIdling && Time.fixedTime - idlingSince > idleDuration)
            {
                nextColor();
            }
            else if (!isIdling)
            {
                idlingSince = Time.fixedTime;
                isIdling = true;
            }
        }
        else
        {
            SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (var renderer in renderers)
            {
                renderer.color = Color.Lerp(AvailableColors[currentColorIdx], AvailableColors[newColorIdx], percentage);
            }
        }
    }
}
