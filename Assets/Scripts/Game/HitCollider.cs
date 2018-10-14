using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour {

    GameObject gameManager;
    GameObject gameCanvas;
    GameObject note;

    public GameObject tapParticle;
    public GameObject hitParticle;
    public GameObject holdParticle;
    GameObject holdParticleInstance;

    public GameObject perfectText;
    public GameObject greatText;
    public GameObject goodText;
    public GameObject badText;
    public GameObject missText;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameCanvas = GameObject.Find("GameCanvas");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Note") || other.CompareTag("HeadNote") || other.CompareTag("TailNote"))
        {
            note = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Note") || other.CompareTag("TailNote"))
        {
            note = null;
        }

        else if (other.CompareTag("HeadNote"))
        {
            DestroyHoldNote();
            MissNote();
        }
    }

    public void OnPress()
    {
        FindObjectOfType<AudioManager>().Play("tap");
        Instantiate(tapParticle, transform.position, tapParticle.transform.rotation);

        if (note == null)
        {
            return;
        }

        else if (note.CompareTag("Note"))
        {
            HandleNote(note.GetComponent<Note>().GetScoreType(), note.tag);
        }

        else if (note.CompareTag("HeadNote"))
        {
            GameObject holdLane = note.transform.parent.transform.Find("HoldLane").gameObject;
            HandleNote(note.GetComponent<Note>().GetScoreType(), note.tag);
            note = holdLane;
        }
    }

    public void OnRelease()
    {
        if (note == null)
        {
            return;
        }

        else if (note.CompareTag("TailNote"))
        {
            HandleNote(note.GetComponent<Note>().GetScoreType(), note.tag);
            DestroyHoldNote();
        }

        else if (note.CompareTag("HoldNote"))
        {
            DestroyHoldNote();
            MissNote();
        }
    }

    public void OnHold()
    {
        /*if (note == null)
        {
            return;
        }

        else if (note.CompareTag("HoldNote"))
        {
            
        }*/
    }

    void DestroyHoldNote()
    {
        Destroy(note.transform.parent.gameObject);
        StopLoopingParticle();
    }

    void StopLoopingParticle()
    {
        if (holdParticleInstance)
        {
            for (int i = 0; i < holdParticleInstance.transform.childCount; i++)
            {
                holdParticleInstance.transform.GetChild(i).GetComponent<ParticleSystem>().loop = false;
            }
        }
    }

    void MissNote()
    {
        gameManager.GetComponent<GameManager>().ResetCombo();
        Instantiate(missText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 75.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
    }

    void HandleNote(string scoreType, string noteType)
    {
        FindObjectOfType<AudioManager>().Play(scoreType);
        Destroy(note);

        if (noteType == "Note")
        {
            Instantiate(hitParticle, transform.position, hitParticle.transform.rotation);
        }
        else if (noteType == "HeadNote")
        {
            holdParticleInstance = Instantiate(holdParticle, transform.position, holdParticle.transform.rotation);
        }

        if (scoreType == "perfect")
        {
            Instantiate(perfectText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 75.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
            gameManager.GetComponent<GameManager>().IncreaseCombo();
            gameManager.GetComponent<GameManager>().IncreaseScore(1000);
        }
        else if (scoreType == "great")
        {
            Instantiate(greatText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 75.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
            gameManager.GetComponent<GameManager>().IncreaseCombo();
            gameManager.GetComponent<GameManager>().IncreaseScore(750);
        }
        else if (scoreType == "good")
        {
            Instantiate(goodText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 75.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
            gameManager.GetComponent<GameManager>().ResetCombo();
            gameManager.GetComponent<GameManager>().IncreaseScore(500);
        }
        else if (scoreType == "bad")
        {
            Instantiate(badText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 75.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
            gameManager.GetComponent<GameManager>().ResetCombo();
            gameManager.GetComponent<GameManager>().IncreaseScore(250);
        }
    }
}
