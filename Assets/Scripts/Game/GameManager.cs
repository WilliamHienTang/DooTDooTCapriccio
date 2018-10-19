using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour {

    public Transform noteObject;
    public Transform holdNoteObject;

    float speedOffset;
    float noteSpeed;
    readonly float songDelay = 5.0f;
    readonly float spawnDistance = 20.0f;
    
    string jsonPath;
    string json;
    NoteSpawn[] noteChart;
    int chartIndex = 0;

    float bpm = 94.0f;
    float songTimer; // time in seconds that passed since the song started
    float oldSongTimer;
    float secPerBeat; // duration of a beat
    float dsptimesong; // time in seconds at the start of the song

    // Use this for initialization
    IEnumerator Start()
    {
        enabled = false;

        // initialize player prefs
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Combo", 0);

        // load the note chart from the json file
        jsonPath = Application.dataPath + "/JsonNoteCharts/" + PlayerPrefs.GetString("SelectedSong") + "_" + PlayerPrefs.GetString("Difficulty") + ".json";
        json = File.ReadAllText(jsonPath);
        noteChart = JsonHelper.FromJson<NoteSpawn>(json);

        // initialize time tracking
        secPerBeat = 60.0f / bpm;
        dsptimesong = (float) AudioSettings.dspTime;

        // play song and delay prior to spawning notes
        FindObjectOfType<AudioManager>().Play(PlayerPrefs.GetString("SelectedSong"));
        noteSpeed = PlayerPrefs.GetFloat("NoteSpeed");
        speedOffset = spawnDistance / noteSpeed;
        yield return new WaitForSeconds(songDelay - speedOffset);
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (chartIndex >= noteChart.Length)
        {
            return;
        }

        songTimer = (float)(AudioSettings.dspTime - dsptimesong);
        if (songTimer == oldSongTimer)
        {
            songTimer += Time.deltaTime; 
        }
        oldSongTimer = songTimer;

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
                xPosition = -1.02f;
                break;
            case 2:
                xPosition = -0.51f;
                break;
            case 3:
                xPosition = 0f;
                break;
            case 4:
                xPosition = 0.51f;
                break;
            case 5:
                xPosition = 1.02f;
                break;
            default:
                return;
        }

        Instantiate(noteObject, new Vector3(xPosition, 0.0f, spawnDistance), noteObject.rotation);
    }

    void InstantiateHoldNote(int laneIndex, float length)
    {
        float xPosition;
        switch (laneIndex)
        {
            case 1:
                xPosition = -1.02f;
                break;
            case 2:
                xPosition = -0.51f;
                break;
            case 3:
                xPosition = 0f;
                break;
            case 4:
                xPosition = 0.51f;
                break;
            case 5:
                xPosition = 1.02f;
                break;
            default:
                return;
        }

        Transform holdNote = Instantiate(holdNoteObject, new Vector3(xPosition, 0.0f, spawnDistance), holdNoteObject.rotation);
        GameObject holdLane = holdNote.transform.Find("HoldLane").gameObject;
        holdNote.transform.Find("HoldLane").localPosition = new Vector3(holdLane.transform.localPosition.x, holdLane.transform.localPosition.y, length / 2.0f);
        holdNote.transform.Find("HoldLane").localScale = new Vector3(holdLane.transform.localScale.x, holdLane.transform.localScale.y, length);
        GameObject tailNote = holdNote.transform.Find("Tail").gameObject;
        holdNote.transform.Find("Tail").localPosition = new Vector3(tailNote.transform.localPosition.x, tailNote.transform.localPosition.y, length);
    }

    public void IncreaseScore(int points)
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + points);
    }

    public void IncreaseCombo()
    {
        PlayerPrefs.SetInt("Combo", PlayerPrefs.GetInt("Combo") + 1);
    }

    public void ResetCombo()
    {
        PlayerPrefs.SetInt("Combo", 0);
    }
}
