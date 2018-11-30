using UnityEngine;

public class HitCollider : MonoBehaviour {

    bool isTouchingDevice;
    public ScoreManager scoreManager;
    public Transform gameCanvas;
    AudioManager audioManager;
    Pause pause;

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

    bool holding;

    void Awake()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.OSXEditor:
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
        pause = FindObjectOfType<Pause>();
    }

    // Determine collider presses, holds, and releases
    void Update()
    {
        if (pause.IsPaused())
        {
            return;
        }

        if (isTouchingDevice)
        {
            if (Input.touchCount > 0)
            {
                // initially holding = false if note instance is a hold lane
                if(noteInstance != null){
                    holding &= !noteInstance.CompareTag(Constants.holdLaneTag);
                }


                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.touches[i];
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform == transform)
                        {
                            switch (touch.phase)
                            {
                                case TouchPhase.Began:
                                    OnPress();
                                    break;

                                case TouchPhase.Ended:
                                    OnRelease();
                                    break;
                            }

                            // holding = true if note instance is a hold lane
                            if(noteInstance != null){
                                holding |= noteInstance.CompareTag(Constants.holdLaneTag);
                            }
                        }
                    }
                }

                // If not holding on to hold note, treat as a hold note release
                if(noteInstance != null && noteInstance.CompareTag(Constants.holdLaneTag))
                {
                    if(!holding)
                    {
                        OnRelease();
                    }
                }

            }
        }

        else
        {
            // initially holding = false if note instance is a hold lane
            if(noteInstance != null){
                holding &= !noteInstance.CompareTag(Constants.holdLaneTag);
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        OnPress();
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        OnRelease();
                    }

                    // holding = true if note instance is a hold lane
                    if(noteInstance != null){
                        holding |= noteInstance.CompareTag(Constants.holdLaneTag);
                    }
                }
            }

            // If not holding on to hold note, treat as a hold note release
            if (noteInstance != null && noteInstance.CompareTag(Constants.holdLaneTag))
            {
                if (!holding)
                {
                    OnRelease();
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
            DestroyHeldNote();
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
    void OnPress()
    {
        audioManager.Play(Constants.tapSFX);
        Instantiate(tapParticle, transform.position, tapParticle.transform.rotation);

        if (noteInstance == null)
        {
            return;
        }

        if (noteInstance.CompareTag(Constants.noteTag))
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
    void OnRelease()
    {
        if (noteInstance == null)
        {
            return;
        }

        if (noteInstance.CompareTag(Constants.tailNoteTag))
        {
            Instantiate(headTailParticle, transform.position, headTailParticle.rotation);
            HandleNote(noteInstance.GetComponent<NoteScore>().GetScoreType());
        }

        else if (noteInstance.CompareTag(Constants.holdLaneTag))
        {
            DestroyHeldNote();
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

        if (noteInstance.CompareTag(Constants.headNoteTag) || noteInstance.CompareTag(Constants.holdLaneTag))
        {
            noteInstance.parent.GetComponent<HoldNote>().DestroyTail();
            Destroy(noteInstance.parent.gameObject);
            StopLoopingParticle();
            MissNote();
        }
    }

    void DestroyHeldNote(){
        if(heldNoteInstance != null){
            Destroy(heldNoteInstance.gameObject);
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
