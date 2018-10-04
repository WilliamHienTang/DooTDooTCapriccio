using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectCollider : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Note"))
        {
            other.gameObject.GetComponent<Note>().SetScoreType("perfect");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Note"))
        {
            other.gameObject.GetComponent<Note>().SetScoreType("great");
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
