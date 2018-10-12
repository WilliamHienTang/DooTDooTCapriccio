using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour {

    Material material;
    float dissolveAmount;
    float velocity;
    float length;
    float duration;
    float dissolveCycle;
    float dissolveDelta;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "DissolveCollider")
        {
            enabled = true;
        }
    }

    // Use this for initialization
    void Start () {
        enabled = false;

        material = GetComponent<Renderer>().material;
        dissolveAmount = 0.0f;
        velocity = PlayerPrefs.GetFloat("NoteSpeed");
        length = GetComponent<Renderer>().bounds.size.z;
        duration = length / velocity;
        dissolveCycle = duration / Time.deltaTime;
        dissolveDelta = 1.0f / dissolveCycle;
    }
	
	// Update is called once per frame
	void Update () {
        dissolveAmount += dissolveDelta;
        material.SetFloat("_DissolveAmount", dissolveAmount + dissolveDelta);
    }
}
