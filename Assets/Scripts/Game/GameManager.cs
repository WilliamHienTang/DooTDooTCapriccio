using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour {

    public bool isTouchingDevice;
    public Transform noteObject;
    public Transform holdNoteObject;

    string selectedSong;
    float speedOffset;
    float noteSpeed;
    
    string jsonPath;
    string json;
    NoteSpawn[] noteChart;
    int chartIndex = 0;

    float bpm = 94.0f;
    float songTimer;
    float startTime;

    void Awake()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
                isTouchingDevice = false;
                break;
            case RuntimePlatform.Android:
                isTouchingDevice = true;
                break;
        }
    }

    // Use this for initialization
    void Start()
    {
        // initialize player prefs
        PlayerPrefs.SetInt(Constants.score, 0);
        PlayerPrefs.SetInt(Constants.combo, 0);
        noteSpeed = PlayerPrefs.GetFloat(Constants.noteSpeed);
        selectedSong = PlayerPrefs.GetString(Constants.selectedSong);

        // load the note chart from the json file
        jsonPath = Application.dataPath + "/JsonNoteCharts/" + selectedSong + "_" + PlayerPrefs.GetString(Constants.difficulty) + ".json";
        json = File.ReadAllText(jsonPath);
        noteChart = JsonHelper.FromJson<NoteSpawn>(json);

        // initialize time syncing variables
        speedOffset = Constants.spawnZ / noteSpeed;
        startTime = (float)AudioSettings.dspTime;
        songTimer = (float)(AudioSettings.dspTime - startTime);

        // wait for speed offset before playing song
        FindObjectOfType<AudioManager>().PlayScheduled(selectedSong, speedOffset);
    }

    // Update is called once per frame
    void Update()
    {
        if (chartIndex >= noteChart.Length)
        {
            return;
        }

        songTimer = (float)(AudioSettings.dspTime - startTime);
        if ((noteChart[chartIndex].spawnTime - Time.deltaTime) <= songTimer)
        {

            if (noteChart[chartIndex].tailSpawnTime > 0)
            {
                float length = noteSpeed * (noteChart[chartIndex].tailSpawnTime - noteChart[chartIndex].spawnTime);
                InstantiateHoldNote(noteChart[chartIndex].laneIndex, length);
            }
            else
            {
                InstantiateNote(noteChart[chartIndex].laneIndex);
            }
            chartIndex++;
        }

        if (chartIndex >= noteChart.Length)
        {
            return;
        }

        if ((noteChart[chartIndex].spawnTime - Time.deltaTime) <= songTimer)
        {

            if (noteChart[chartIndex].tailSpawnTime > 0)
            {
                float length = noteSpeed * (noteChart[chartIndex].tailSpawnTime - noteChart[chartIndex].spawnTime);
                InstantiateHoldNote(noteChart[chartIndex].laneIndex, length);
            }
            else
            {
                InstantiateNote(noteChart[chartIndex].laneIndex);
            }
            chartIndex++;
        }
    }

    void InstantiateNote(int laneIndex)
    {
        float xPosition;
        switch (laneIndex)
        {
            case 1:
                xPosition = Constants.lane1X;
                break;
            case 2:
                xPosition = Constants.lane2X;
                break;
            case 3:
                xPosition = Constants.lane3X;
                break;
            case 4:
                xPosition = Constants.lane4X;
                break;
            case 5:
                xPosition = Constants.lane5X;
                break;
            default:
                return;
        }

        Instantiate(noteObject, new Vector3(xPosition, 0.0f, Constants.spawnZ), noteObject.rotation);
    }

    void InstantiateHoldNote(int laneIndex, float length)
    {
        float xPosition;
        switch (laneIndex)
        {
            case 1:
                xPosition = Constants.lane1X;
                break;
            case 2:
                xPosition = Constants.lane2X;
                break;
            case 3:
                xPosition = Constants.lane3X;
                break;
            case 4:
                xPosition = Constants.lane4X;
                break;
            case 5:
                xPosition = Constants.lane5X;
                break;
            default:
                return;
        }

        Transform holdNote = Instantiate(holdNoteObject, new Vector3(xPosition, 0.0f, Constants.spawnZ), holdNoteObject.rotation);
        GameObject holdLane = holdNote.transform.Find("HoldLane").gameObject;
        holdNote.transform.Find("HoldLane").localPosition = new Vector3(holdLane.transform.localPosition.x, holdLane.transform.localPosition.y, length / 2.0f);
        holdNote.transform.Find("HoldLane").localScale = new Vector3(holdLane.transform.localScale.x, holdLane.transform.localScale.y, length);
        GameObject tailNote = holdNote.transform.Find("Tail").gameObject;
        holdNote.transform.Find("Tail").localPosition = new Vector3(tailNote.transform.localPosition.x, tailNote.transform.localPosition.y, length);
    }

    public void IncreaseScore(int points)
    {
        PlayerPrefs.SetInt(Constants.score, PlayerPrefs.GetInt(Constants.score) + points);
    }

    public void IncreaseCombo()
    {
        PlayerPrefs.SetInt(Constants.combo, PlayerPrefs.GetInt(Constants.combo) + 1);
    }

    public void ResetCombo()
    {
        PlayerPrefs.SetInt(Constants.combo, 0);
    }
}
