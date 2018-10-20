using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour {

    Material material;
    float dissolveAmount;
    float speed;
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
        speed = PlayerPrefs.GetFloat(Constants.noteSpeed);
        length = GetComponent<Renderer>().bounds.size.z;
        duration = length / speed;
    }
	
	// Update is called once per frame
	void Update () {
        dissolveCycle = duration / Time.deltaTime;
        dissolveDelta = 1.0f / dissolveCycle;
        dissolveAmount += dissolveDelta;
        material.SetFloat("_DissolveAmount", dissolveAmount + dissolveDelta);
    }
}
