using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitCollider : MonoBehaviour {

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

    string colliderName;
    public GameObject gameManager;
    public GameObject gameCanvas;
    public Transform heldNote;
    GameObject heldNoteInstance;
    GameObject noteInstance;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameCanvas = GameObject.Find("GameCanvas");
        colliderName = gameObject.name;
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
                    if (hit.transform.name == colliderName)
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
                if (hit.transform.name == colliderName)
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
            noteInstance = other.gameObject;
        }

        else if (other.CompareTag(Constants.tailNoteTag))
        {
            Destroy(heldNoteInstance);
            StopLoopingParticle();
            noteInstance = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.noteTag) || other.CompareTag(Constants.tailNoteTag))
        {
            Destroy(other.gameObject);
            MissNote();
        }

        else if (other.CompareTag(Constants.headNoteTag))
        {
            Instantiate(missHoldParticle, transform.position, missHoldParticle.transform.rotation);
            DestroyHoldNote();
            MissNote();
        }
    }

    public void OnPress()
    {
        FindObjectOfType<AudioManager>().Play(Constants.tapSFX);
        Instantiate(tapParticle, transform.position, tapParticle.transform.rotation);

        if (noteInstance == null)
        {
            return;
        }

        else if (noteInstance.CompareTag(Constants.noteTag))
        {
            HandleNote(noteInstance.GetComponent<NoteScore>().GetScoreType(), noteInstance.tag);
        }

        else if (noteInstance.CompareTag(Constants.headNoteTag))
        {
            HandleNote(noteInstance.GetComponent<NoteScore>().GetScoreType(), noteInstance.tag);
            GameObject holdLane = noteInstance.transform.parent.Find("HoldLane").gameObject;
            noteInstance = holdLane;
            heldNoteInstance = Instantiate(heldNote, new Vector3(transform.position.x, heldNote.position.y, transform.position.z), heldNote.rotation).gameObject;
        }
    }

    public void OnRelease()
    {
        if (noteInstance == null)
        {
            return;
        }

        else if (noteInstance.CompareTag(Constants.tailNoteTag))
        {
            Destroy(heldNoteInstance);
            HandleNote(noteInstance.GetComponent<NoteScore>().GetScoreType(), noteInstance.tag);
        }

        else if (noteInstance.CompareTag(Constants.holdLaneTag))
        {
            Destroy(heldNoteInstance);
            Instantiate(missHoldParticle, transform.position, missHoldParticle.transform.rotation);
            DestroyHoldNote();
        }
    }

    public void OutOfBound()
    {
        if (noteInstance == null)
        {
            return;
        }

        else if (noteInstance.CompareTag(Constants.holdLaneTag))
        {
            Instantiate(missHoldParticle, transform.position, missHoldParticle.transform.rotation);
            DestroyHoldNote();
        }
    }

    void DestroyHoldNote()
    {
        if (noteInstance == null)
        {
            return;
        }

        else if (noteInstance.CompareTag(Constants.headNoteTag) || noteInstance.CompareTag(Constants.holdLaneTag))
        {
            noteInstance.transform.parent.GetComponent<HoldNote>().DestroyTail();
            Destroy(noteInstance.transform.parent.gameObject);
            StopLoopingParticle();
            MissNote();
        }
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
        ClearScoreTypeText();
        Instantiate(missText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 25.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
        gameManager.GetComponent<GameManager>().ResetCombo();
        gameManager.GetComponent<GameManager>().MissNote();
    }

    void HandleNote(string scoreType, string noteType)
    {
        FindObjectOfType<AudioManager>().Play(scoreType);
        Destroy(noteInstance);

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

        if (scoreType == Constants.perfect)
        {
            ClearScoreTypeText();
            Instantiate(perfectText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 25.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
            gameManager.GetComponent<GameManager>().IncreaseCombo();
            gameManager.GetComponent<GameManager>().IncreaseScore(Constants.perfectScore, Constants.perfects);
        }
        else if (scoreType == Constants.great)
        {
            ClearScoreTypeText();
            Instantiate(greatText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 25.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
            gameManager.GetComponent<GameManager>().IncreaseCombo();
            gameManager.GetComponent<GameManager>().IncreaseScore(Constants.greatScore, Constants.greats);
        }
        else if (scoreType == Constants.good)
        {
            ClearScoreTypeText();
            Instantiate(goodText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 25.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
            gameManager.GetComponent<GameManager>().ResetCombo();
            gameManager.GetComponent<GameManager>().IncreaseScore(Constants.goodScore, Constants.goods);
        }
        else if (scoreType == Constants.bad)
        {
            ClearScoreTypeText();
            Instantiate(badText, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y - 25.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
            gameManager.GetComponent<GameManager>().ResetCombo();
            gameManager.GetComponent<GameManager>().IncreaseScore(Constants.badScore, Constants.bads);
        }
    }

    void ClearScoreTypeText()
    { 
        GameObject[] scoreTypeTexts = GameObject.FindGameObjectsWithTag(Constants.scoreType);

        for (int i = 0; i < scoreTypeTexts.Length; i++)
        {
            Destroy(scoreTypeTexts[i]);
        }
    }
}
