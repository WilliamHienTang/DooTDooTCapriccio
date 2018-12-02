using UnityEngine;

public class NoteScore : MonoBehaviour {

    string scoreType;

    public void SetScoreType(string type)
    {
        scoreType = type;
    }

    public string GetScoreType()
    {
        return scoreType;
    }
}
