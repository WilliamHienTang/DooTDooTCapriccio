using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodCollider : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.noteTag) || other.CompareTag(Constants.headNoteTag) || other.CompareTag(Constants.tailNoteTag))
        {
            other.gameObject.GetComponent<Note>().SetScoreType(Constants.good);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.noteTag) || other.CompareTag(Constants.headNoteTag) || other.CompareTag(Constants.tailNoteTag))
        {
            other.gameObject.GetComponent<Note>().SetScoreType(Constants.bad);
        }
    }
}
