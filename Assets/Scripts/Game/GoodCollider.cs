using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodCollider : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Note") || other.CompareTag("HeadNote") || other.CompareTag("TailNote"))
        {
            other.gameObject.GetComponent<Note>().SetScoreType("good");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Note") || other.CompareTag("HeadNote") || other.CompareTag("TailNote"))
        {
            other.gameObject.GetComponent<Note>().SetScoreType("bad");
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
