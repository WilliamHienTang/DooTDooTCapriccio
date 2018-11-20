using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpeed : MonoBehaviour {

    float noteSpeed;
    Vector3 initPosition;
    Vector3 followThroughPosition;
    float distance;

    float songTimer;
    float dspStart;

    // Use this for initialization
    void Start()
    {
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

        if (noteSpeed * songTimer > distance)
        {
            Destroy(gameObject);
        }
    }

    public void Stop()
    {
        enabled = false;
    }
}
