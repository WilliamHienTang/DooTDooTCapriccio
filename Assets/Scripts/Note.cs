using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {

    Rigidbody RB;

	// Use this for initialization
	void Start () {
        RB = GetComponent<Rigidbody>();
        RB.velocity = new Vector3(0, 0, -1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
