using UnityEngine;

public class BadCollider : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.noteTag) || other.CompareTag(Constants.headNoteTag) || other.CompareTag(Constants.tailNoteTag))
        {
            other.gameObject.GetComponent<NoteScore>().SetScoreType(Constants.bad);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.noteTag) || other.CompareTag(Constants.headNoteTag) || other.CompareTag(Constants.tailNoteTag))
        {
            other.gameObject.GetComponent<NoteScore>().SetScoreType(null);
        }
    }
}
