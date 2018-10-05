using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour {

    GameObject note;
    bool active;
    public Transform wave;

    void OnTriggerEnter(Collider other)
    {
        active = true;
        note = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        active = false;
    }

    public void OnPress()
    {
        if (active)
        {
            HandlePress(note.GetComponent<Note>().GetScoreType());
            active = false;
        }
    }

    public void HandlePress(string type)
    {
        FindObjectOfType<AudioManager>().Play(type);
        Destroy(note);
        Instantiate(wave, transform.position, wave.rotation);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }
}
