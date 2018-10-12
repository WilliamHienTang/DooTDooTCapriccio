using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissCollider : MonoBehaviour {

    GameObject gameManager;
    GameObject gameCanvas;
    public GameObject missText;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TailNote"))
        {
            Destroy(other.transform.parent.gameObject);
            gameManager.GetComponent<GameManager>().ResetCombo();
            Instantiate(missText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 75.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
        }

        else if(other.CompareTag("Note") || other.CompareTag("HeadNote"))
        {
            Destroy(other.gameObject);
            gameManager.GetComponent<GameManager>().ResetCombo();
            Instantiate(missText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 75.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
        }
    }

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager");
        gameCanvas = GameObject.Find("GameCanvas");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
