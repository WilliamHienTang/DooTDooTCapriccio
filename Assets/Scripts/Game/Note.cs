using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {

    Rigidbody RB;
    string scoreType = null;
    float velocity;

    // Use this for initialization
    void Start()
    {
        velocity = PlayerPrefs.GetFloat("NoteSpeed");
        RB = GetComponent<Rigidbody>();
        RB.velocity = new Vector3(0, 0, -velocity);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetScoreType(string type)
    {
        scoreType = type;
    }

    public string GetScoreType()
    {
        return scoreType;
    }
}
