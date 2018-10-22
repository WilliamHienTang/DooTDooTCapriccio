using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour {

    Material material;
    float noteSpeed;
    float length;
    float duration;

    float songTimer;
    float startTime;

    void OnTriggerEnter(Collider other)
    {

        if (other.transform.name == "DissolveCollider")
        {
            startTime = (float)AudioSettings.dspTime;
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
        songTimer = (float)(AudioSettings.dspTime - startTime);
        material.SetFloat("_DissolveAmount", songTimer / duration);
    }
}
