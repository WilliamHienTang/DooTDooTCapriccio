using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour {

    GameObject note;
    bool active;

    void OnTriggerEnter(Collider collider)
    {
        active = true;
        note = collider.gameObject;
    }

    void OnTriggerExit(Collider collider)
    {
        active = false;
    }

    public void OnPress()
    {
        if (active)
        {
            //FindObjectOfType<AudioManager>().Play("perfect");
            Destroy(note);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }
}
