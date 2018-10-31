using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {

    GameObject gameManager;
    Rigidbody RB;
    string scoreType = null;
    float noteSpeed;
    Vector3 initPosition;
    Vector3 activatorPosition;
    float distance;

    float songTimer;
    float dspStart;
    public float startTime;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        RB = GetComponent<Rigidbody>();
        noteSpeed = PlayerPrefs.GetFloat(Constants.noteSpeed);
        initPosition = transform.position;
        activatorPosition = new Vector3(initPosition.x, initPosition.y, Constants.activatorZ);
        distance = initPosition.z - Constants.activatorZ;
        dspStart = (float)AudioSettings.dspTime;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (gameManager.GetComponent<GameManager>().GetSongTimer() < startTime)
        {
            Debug.Log(gameManager.GetComponent<GameManager>().GetSongTimer());
            return;   
        }*/
        
        songTimer = (float)(AudioSettings.dspTime - dspStart);

        if ((noteSpeed * songTimer / distance) >= 1)
        {
            RB.velocity = new Vector3(0, 0, -noteSpeed);
            enabled = false;
        }

        transform.position = Vector3.Lerp(initPosition, activatorPosition, (noteSpeed * songTimer / distance));   
    }

    public void SetStartTime(float time)
    {
        startTime = time;
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
