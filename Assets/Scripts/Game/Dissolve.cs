using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour {

    Material material;
    float noteSpeed;
    float length;
    float duration;

    float songTimer;
    float dsptimesong; // time in seconds at the start of the song
    float oldSongTimer;

    void OnTriggerEnter(Collider other)
    {

        if (other.transform.name == "DissolveCollider")
        {
            dsptimesong = (float)AudioSettings.dspTime;
            enabled = true;
        }
    }

    // Use this for initialization
    void Start () {
        enabled = false;
        material = GetComponent<Renderer>().material;
        noteSpeed = PlayerPrefs.GetFloat(Constants.noteSpeed);
        length = GetComponent<Renderer>().bounds.size.z;
        duration = length / noteSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        songTimer = (float)(AudioSettings.dspTime - dsptimesong);
        Debug.Log(songTimer);
        if (songTimer == oldSongTimer)
        {
            songTimer += Time.deltaTime;
        }
        oldSongTimer = songTimer;

        material.SetFloat("_DissolveAmount", songTimer / duration);
    }
}
