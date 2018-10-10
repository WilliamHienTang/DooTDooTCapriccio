using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour {

    GameObject gameManager;
    GameObject gameCanvas;
    GameObject note;
    bool active;

    public Transform tapParticle;
    public Transform hitParticle;

    public GameObject perfectText;
    public GameObject greatText;
    public GameObject goodText;
    public GameObject badText;
    public GameObject missText;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Note") || other.CompareTag("ReleaseNote") || other.CompareTag("HoldNote"))
        {
            active = true;
            note = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Note") || other.CompareTag("ReleaseNote"))
        {
            active = false;
        }
    }

    public void OnPress()
    {
        FindObjectOfType<AudioManager>().Play("tap");
        Instantiate(tapParticle, transform.position, tapParticle.rotation);

        if (active && note.CompareTag("Note"))
        {
            active = false;
            HandleNote(note.GetComponent<Note>().GetScoreType());
        }
    }

    public void OnRelease()
    {
        if (active && note.CompareTag("ReleaseNote"))
        {
            active = false;
            HandleNote(note.GetComponent<Note>().GetScoreType());
        }

        else if (active && note.CompareTag("HoldNote"))
        {
            active = false;
            Destroy(note.transform.parent.gameObject);
            gameManager.GetComponent<GameManager>().ResetCombo();
            Instantiate(missText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 75.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
        }
    }

    public void OnHold()
    {
        if (active && note.CompareTag("HoldNote"))
        {
            
        }
    }

    void HandleNote(string type)
    {
        FindObjectOfType<AudioManager>().Play(type);
        Destroy(note);
        Instantiate(hitParticle, transform.position, hitParticle.rotation);

        if (type == "perfect")
        {
            Instantiate(perfectText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 75.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
            gameManager.GetComponent<GameManager>().IncreaseCombo();
            gameManager.GetComponent<GameManager>().IncreaseScore(1000);
        }
        else if (type == "great")
        {
            Instantiate(greatText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 75.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
            gameManager.GetComponent<GameManager>().IncreaseCombo();
            gameManager.GetComponent<GameManager>().IncreaseScore(750);
        }
        else if (type == "good")
        {
            Instantiate(goodText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 75.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
            gameManager.GetComponent<GameManager>().ResetCombo();
            gameManager.GetComponent<GameManager>().IncreaseScore(500);
        }
        else if (type == "bad")
        {
            Instantiate(badText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 75.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
            gameManager.GetComponent<GameManager>().ResetCombo();
            gameManager.GetComponent<GameManager>().IncreaseScore(250);
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
