using UnityEngine;

public class NoteSpeed : MonoBehaviour {

    Vector3 initPosition;
    Vector3 followThroughPosition;
    float noteSpeed;
    float distance;
    float songTimer;
    float dspStart;

    void Start()
    {
        noteSpeed = PlayerPrefs.GetFloat(Constants.noteSpeed);
        initPosition = transform.position;
        followThroughPosition = new Vector3(initPosition.x, initPosition.y, Constants.followThroughZ);
        distance = initPosition.z - Constants.followThroughZ;
        dspStart = (float)AudioSettings.dspTime;
    }

    // Interpolate position based on audio time and note speed
    void Update()
    {
        songTimer = (float)(AudioSettings.dspTime - dspStart);
        transform.position = Vector3.Lerp(initPosition, followThroughPosition, (noteSpeed * songTimer / distance));

        if (noteSpeed * songTimer > distance)
        {
            Destroy(gameObject);
        }
    }
}
