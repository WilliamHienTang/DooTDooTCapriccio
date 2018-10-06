using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour {

    GameObject gameManager;
    GameObject note;
    bool active;
    public Transform tapParticle;
    public Transform hitParticle;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Note"))
        {
            active = true;
            note = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Note"))
        {
            active = false;
        }
    }

    public void OnPress()
    {
        FindObjectOfType<AudioManager>().Play("tap");
        Instantiate(tapParticle, transform.position, tapParticle.rotation);

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
        Instantiate(hitParticle, transform.position, hitParticle.rotation);

        if (type == "perfect")
        {
            gameManager.GetComponent<GameManager>().IncreaseCombo();
            gameManager.GetComponent<GameManager>().IncreaseScore(1000);
        }
        else if (type == "great")
        {
            gameManager.GetComponent<GameManager>().IncreaseCombo();
            gameManager.GetComponent<GameManager>().IncreaseScore(750);
        }
        else if (type == "good")
        {
            gameManager.GetComponent<GameManager>().ResetCombo();
            gameManager.GetComponent<GameManager>().IncreaseScore(500);
        }
        else if (type == "bad")
        {
            gameManager.GetComponent<GameManager>().ResetCombo();
            gameManager.GetComponent<GameManager>().IncreaseScore(250);
        }
    }

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager");
	}
	
	// Update is called once per frame
	void Update () {

    }
}
