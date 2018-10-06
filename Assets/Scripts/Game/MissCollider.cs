using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissCollider : MonoBehaviour {

    GameObject gameManager;

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        gameManager.GetComponent<GameManager>().ResetCombo();
    }

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
