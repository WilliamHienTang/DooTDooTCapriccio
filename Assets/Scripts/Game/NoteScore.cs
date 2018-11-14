using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScore : MonoBehaviour {

    string scoreType = null;

    public void SetScoreType(string type)
    {
        scoreType = type;
    }

    public string GetScoreType()
    {
        return scoreType;
    }
}
