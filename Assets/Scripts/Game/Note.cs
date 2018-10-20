using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {

    Rigidbody RB;
    string scoreType = null;
    float noteSpeed;
    Vector3 initPosition;
    Vector3 colliderPosition;
    float colliderZPosition = 1.0f;
    float distance;

    float songTimer; // time in seconds that passed since the song started
    float dsptimesong; // time in seconds at the start of the song
    float oldSongTimer;

    // Use this for initialization
    void Start()
    {
        noteSpeed = PlayerPrefs.GetFloat("NoteSpeed");
        RB = GetComponent<Rigidbody>();
        initPosition = transform.position;
        colliderPosition = new Vector3(initPosition.x, initPosition.y, colliderZPosition);
        distance = (Mathf.Abs(colliderZPosition - initPosition.z));
        dsptimesong = (float)AudioSettings.dspTime;
    }

    // Update is called once per frame
    void Update()
    {
        if ((noteSpeed * songTimer / distance) >= 1)
        {
            RB.velocity = new Vector3(0, 0, -noteSpeed);
            enabled = false;
        }

        songTimer = (float)(AudioSettings.dspTime - dsptimesong);
        if (songTimer == oldSongTimer)
        {
            songTimer += Time.deltaTime;
        }
        oldSongTimer = songTimer;

        transform.position = Vector3.Lerp(initPosition, colliderPosition, (noteSpeed * songTimer / distance));   
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
