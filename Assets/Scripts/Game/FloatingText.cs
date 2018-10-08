using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour {

    public float destroyTime = 1.0f;
    public Vector3 RandomIntensity = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start () {
        Destroy(gameObject, destroyTime);
        transform.localPosition += new Vector3(Random.Range(-RandomIntensity.x, RandomIntensity.x), Random.Range(-RandomIntensity.y, RandomIntensity.y), Random.Range(-RandomIntensity.z, RandomIntensity.z));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
