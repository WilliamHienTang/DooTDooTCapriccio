using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {

    Rigidbody RB;
    string scoreType = null;
    float noteSpeed;
    Vector3 initPosition;
    Vector3 followThroughPosition;
    float distance;

    float songTimer;
    float dspStart;

    // Use this for initialization
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        noteSpeed = PlayerPrefs.GetFloat(Constants.noteSpeed);
        initPosition = transform.position;
        followThroughPosition = new Vector3(initPosition.x, initPosition.y, Constants.followThroughZ);
        distance = initPosition.z - Constants.followThroughZ;
        dspStart = (float)AudioSettings.dspTime;
    }

    // Update is called once per frame
    void Update()
    {        
        songTimer = (float)(AudioSettings.dspTime - dspStart);
        transform.position = Vector3.Lerp(initPosition, followThroughPosition, (noteSpeed * songTimer / distance));
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
