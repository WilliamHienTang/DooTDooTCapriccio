using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour {

    float destroyTime = 1.0f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, destroyTime);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
