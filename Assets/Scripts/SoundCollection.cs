using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundCollection : MonoBehaviour {

    public List<AudioClip> sounds = new List<AudioClip>();

    public Boolean isPlaying;

    public void playRandom()
    {
        if (isPlaying) return;

        var source = setupRandom();
        source.Play();
        StartCoroutine(SetPlayingState(source.clip.length));
    }

    public void playRandomDelayed(float delay)
    {
        if (isPlaying) return;

        var source = setupRandom();
        source.PlayDelayed(delay);
        StartCoroutine(SetPlayingState(delay + source.clip.length));
    }

    private AudioSource setupRandom()
    {
        int randomIdx = Random.Range(0, sounds.Count);
        var source = GetComponent<AudioSource>();
        source.clip = sounds[randomIdx];
        return source;
    }

    IEnumerator SetPlayingState(float delay)
    {
        isPlaying = true;
        yield return new WaitForSeconds(delay);
        isPlaying = false;
    }
}
