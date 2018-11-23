using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitCollider : MonoBehaviour {

    bool isTouchingDevice;
    public ScoreManager scoreManager;
    public Transform gameCanvas;
    AudioManager audioManager;

    // Particles
    public Transform tapParticle;
    public Transform hitParticle;
    public Transform holdParticle;
    public Transform missHoldParticle;
    public Transform headTailParticle;

    // Note and particle instances
    Transform noteInstance;
    Transform holdParticleInstance;
    Transform heldNoteInstance;
    public Transform heldNote;

    string colliderName;

    void Awake()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
                isTouchingDevice = false;
                break;
            default:
                isTouchingDevice = true;
                break;
        }
    }

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        colliderName = gameObject.name;
    }

    // Determine collider presses, holds, and releases
    void Update()
    {
        if (FindObjectOfType<Pause>().IsPaused())
        {
            return;
        }

        if (isTouchingDevice)
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

    // Assign noteInstance to the note entering the collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.noteTag) || other.CompareTag(Constants.headNoteTag))
        {
            noteInstance = other.transform;
        }

        // Destroy the hold lane
        else if (other.CompareTag(Constants.tailNoteTag))
        {
            Destroy(heldNoteInstance.gameObject);
            StopLoopingParticle();
            noteInstance = other.transform;
        }
    }

    // Notes exiting the collider are considered misses
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.noteTag) || other.CompareTag(Constants.tailNoteTag))
        {
            Destroy(other.gameObject);
            MissNote();
        }

        // Destroy the hold note altogether
        else if (other.CompareTag(Constants.headNoteTag))
        {
            Instantiate(missHoldParticle, transform.position, missHoldParticle.rotation);
            DestroyHoldNote();
            MissNote();
        }
    }

    // Score notes pressed
    public void OnPress()
    {
        audioManager.Play(Constants.tapSFX);
        Instantiate(tapParticle, transform.position, tapParticle.transform.rotation);

        if (noteInstance == null)
        {
            return;
        }

        else if (noteInstance.CompareTag(Constants.noteTag))
        {
            Instantiate(hitParticle, transform.position, hitParticle.rotation);
            HandleNote(noteInstance.GetComponent<NoteScore>().GetScoreType());
        }

        // Activate hold note
        else if (noteInstance.CompareTag(Constants.headNoteTag))
        {
            Instantiate(headTailParticle, transform.position, headTailParticle.rotation);
            holdParticleInstance = Instantiate(holdParticle, transform.position, holdParticle.rotation);
            HandleNote(noteInstance.GetComponent<NoteScore>().GetScoreType());
            Transform holdLane = noteInstance.parent.Find("HoldLane");
            noteInstance = holdLane;
            heldNoteInstance = Instantiate(heldNote, new Vector3(transform.position.x, heldNote.position.y, transform.position.z), heldNote.rotation);
        }
    }

    // Score tail releases and destroy hold note releases
    public void OnRelease()
    {
        if (noteInstance == null)
        {
            return;
        }

        else if (noteInstance.CompareTag(Constants.tailNoteTag))
        {
            Instantiate(headTailParticle, transform.position, headTailParticle.rotation);
            HandleNote(noteInstance.GetComponent<NoteScore>().GetScoreType());
        }

        else if (noteInstance.CompareTag(Constants.holdLaneTag))
        {
            Destroy(heldNoteInstance.gameObject);
            Instantiate(missHoldParticle, transform.position, missHoldParticle.rotation);
            DestroyHoldNote();
        }
    }
    
    // Miss for leaving collider bounds during hold note
    public void OutOfBound()
    {
        if (noteInstance == null)
        {
            return;
        }

        else if (noteInstance.CompareTag(Constants.holdLaneTag))
        {
            Destroy(heldNoteInstance.gameObject);
            Instantiate(missHoldParticle, transform.position, missHoldParticle.rotation);
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
            noteInstance.parent.GetComponent<HoldNote>().DestroyTail();
            Destroy(noteInstance.parent.gameObject);
            StopLoopingParticle();
            MissNote();
        }
    }

    // Stop the looping particle of a hold note
    void StopLoopingParticle()
    {
        if (holdParticleInstance)
        {
            for (int i = 0; i < holdParticleInstance.childCount; i++)
            {
                holdParticleInstance.GetChild(i).GetComponent<ParticleSystem>().loop = false;
            }
        }
    }

    void MissNote()
    {
        scoreManager.ResetCombo();
        scoreManager.MissNote();
    }

    // Increase score based on score type
    void HandleNote(string scoreType)
    {
        audioManager.Play(scoreType);
        Destroy(noteInstance.gameObject);

        if (scoreType == Constants.perfect)
        {
            scoreManager.IncreaseCombo();
            scoreManager.IncreaseScore(Constants.perfectScore, Constants.perfects);
        }
        else if (scoreType == Constants.great)
        {
            scoreManager.IncreaseCombo();
            scoreManager.IncreaseScore(Constants.greatScore, Constants.greats);
        }
        else if (scoreType == Constants.good)
        {
            scoreManager.ResetCombo();
            scoreManager.IncreaseScore(Constants.goodScore, Constants.goods);
        }
        else if (scoreType == Constants.bad)
        {
            scoreManager.ResetCombo();
            scoreManager.IncreaseScore(Constants.badScore, Constants.bads);
        }
    }
}
