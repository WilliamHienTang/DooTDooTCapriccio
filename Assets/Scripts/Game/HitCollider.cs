using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitCollider : MonoBehaviour {

    string name;
    GameObject gameManager;
    GameObject gameCanvas;
    GameObject note;

    public GameObject tapParticle;
    public GameObject hitParticle;
    public GameObject holdParticle;
    public GameObject missHoldParticle;
    public GameObject headTailParticle;
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
        name = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Pause>().IsPaused())
        {
            return;
        }

        if (gameManager.GetComponent<GameManager>().isTouchingDevice)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.touches[i];
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.name == name)
                    {
                        if (touch.phase == TouchPhase.Began)
                        {
                            OnPress();
                        }
                        else if (touch.phase == TouchPhase.Ended)
                        {
                            OnRelease();
                        }
                    }

                    else
                    {
                        OutOfBound();
                    }
                }
            }
        }

        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == name)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        OnPress();
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        OnRelease();
                    }
                }

                else
                {
                    OutOfBound();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.noteTag) || other.CompareTag(Constants.headNoteTag))
        {
            note = other.gameObject;
        }

        else if (other.CompareTag(Constants.tailNoteTag))
        {
            StopLoopingParticle();
            note = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.noteTag))
        {
            Destroy(other.gameObject);
        }

        else if (other.CompareTag(Constants.tailNoteTag))
        {
            Destroy(other.transform.parent.gameObject);
        }

        else if (other.CompareTag(Constants.headNoteTag))
        {
            Instantiate(missHoldParticle, transform.position, missHoldParticle.transform.rotation);
            DestroyHoldNote();
        }
        MissNote();
    }

    public void OnPress()
    {
        FindObjectOfType<AudioManager>().Play(Constants.tapSFX);
        Instantiate(tapParticle, transform.position, tapParticle.transform.rotation);

        if (note == null)
        {
            return;
        }

        else if (note.CompareTag(Constants.noteTag))
        {
            HandleNote(note.GetComponent<Note>().GetScoreType(), note.tag);
        }

        else if (note.CompareTag(Constants.headNoteTag))
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

        else if (note.CompareTag(Constants.tailNoteTag))
        {
            DestroyHoldNote();
            HandleNote(note.GetComponent<Note>().GetScoreType(), note.tag);
        }

        else if (note.CompareTag(Constants.holdLaneTag))
        {
            Instantiate(missHoldParticle, transform.position, missHoldParticle.transform.rotation);
            DestroyHoldNote();
            MissNote();
        }
    }

    public void OutOfBound()
    {
        if(note == null)
        {
            return;
        }

        if (note.CompareTag(Constants.holdLaneTag))
        {
            Instantiate(missHoldParticle, transform.position, missHoldParticle.transform.rotation);
            DestroyHoldNote();
            MissNote();
        }
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
        Destroy(GameObject.FindWithTag(Constants.scoreType));
        Instantiate(missText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 50.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
        gameManager.GetComponent<GameManager>().ResetCombo();
    }

    void HandleNote(string scoreType, string noteType)
    {
        FindObjectOfType<AudioManager>().Play(scoreType);
        Destroy(note);

        if (noteType == Constants.noteTag)
        {
            Instantiate(hitParticle, transform.position, hitParticle.transform.rotation);
        }
        else if (noteType == Constants.headNoteTag)
        {
            Instantiate(headTailParticle, transform.position, headTailParticle.transform.rotation);
            holdParticleInstance = Instantiate(holdParticle, transform.position, holdParticle.transform.rotation);
        }
        else if (noteType == Constants.tailNoteTag)
        {
            Instantiate(headTailParticle, transform.position, headTailParticle.transform.rotation);
        }

        if (scoreType == "perfect")
        {
            Destroy(GameObject.FindWithTag(Constants.scoreType));
            Instantiate(perfectText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 50.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
            gameManager.GetComponent<GameManager>().IncreaseCombo();
            gameManager.GetComponent<GameManager>().IncreaseScore(Constants.perfectScore);
        }
        else if (scoreType == "great")
        {
            Destroy(GameObject.FindWithTag(Constants.scoreType));
            Instantiate(greatText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 50.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
            gameManager.GetComponent<GameManager>().IncreaseCombo();
            gameManager.GetComponent<GameManager>().IncreaseScore(Constants.greatScore);
        }
        else if (scoreType == "good")
        {
            Destroy(GameObject.FindWithTag(Constants.scoreType));
            Instantiate(goodText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 50.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
            gameManager.GetComponent<GameManager>().ResetCombo();
            gameManager.GetComponent<GameManager>().IncreaseScore(Constants.goodScore);
        }
        else if (scoreType == "bad")
        {
            Destroy(GameObject.FindWithTag(Constants.scoreType));
            Instantiate(badText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 50.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
            gameManager.GetComponent<GameManager>().ResetCombo();
            gameManager.GetComponent<GameManager>().IncreaseScore(Constants.badScore);
        }
    }
}
