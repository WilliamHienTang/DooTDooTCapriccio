using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

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

    float songTimer;
    float dspStart;

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
        InitPlayerPrefs();

        // load the note chart from the json file
        jsonPath = Application.dataPath + "/JsonNoteCharts/" + selectedSong + "_" + PlayerPrefs.GetString(Constants.difficulty) + ".json";
        json = File.ReadAllText(jsonPath);
        noteChart = JsonHelper.FromJson<NoteSpawn>(json);

        // play song
        FindObjectOfType<AudioManager>().Play(selectedSong);

        // initialize time syncing variables
        speedOffset = (Constants.spawnZ - Constants.activatorZ) / noteSpeed;
        dspStart = (float)AudioSettings.dspTime;
    }

    // Update is called once per frame
    void Update()
    {
        songTimer = (float)(AudioSettings.dspTime - dspStart);

        if (chartIndex >= noteChart.Length)
        {
            StartCoroutine(EndGame());
            enabled = false;
            return;
        }

        if ((noteChart[chartIndex].headHitTime - speedOffset) <= songTimer)
        {
            if (noteChart[chartIndex].tailHitTime > 0)
            {
                float length = noteSpeed * (noteChart[chartIndex].tailHitTime - noteChart[chartIndex].headHitTime);
                InstantiateHoldNote(noteChart[chartIndex].headHitTime - speedOffset, noteChart[chartIndex].tailHitTime - speedOffset, noteChart[chartIndex].laneIndex, length);
            }
            else
            {
                InstantiateNote(noteChart[chartIndex].headHitTime - speedOffset, noteChart[chartIndex].laneIndex);
            }
            chartIndex++;
        }

        if (chartIndex >= noteChart.Length)
        {
            StartCoroutine(EndGame());
            enabled = false;
            return;
        }

        if ((noteChart[chartIndex].headHitTime - speedOffset) <= songTimer)
        {
            if (noteChart[chartIndex].tailHitTime > 0)
            {
                float length = noteSpeed * (noteChart[chartIndex].tailHitTime - noteChart[chartIndex].headHitTime);
                InstantiateHoldNote(noteChart[chartIndex].headHitTime - speedOffset, noteChart[chartIndex].tailHitTime - speedOffset, noteChart[chartIndex].laneIndex, length);
            }
            else
            {
                InstantiateNote(noteChart[chartIndex].headHitTime - speedOffset, noteChart[chartIndex].laneIndex);
            }
            chartIndex++;
        }
    }

    void InstantiateNote(float hitTime, int laneIndex)
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

        Transform note = Instantiate(noteObject, new Vector3(xPosition, noteObject.transform.localPosition.y, Constants.spawnZ), noteObject.rotation);
        note.GetComponent<Note>().SetStartTime(hitTime);
    }

    void InstantiateHoldNote(float headHitTime, float tailHitTime, int laneIndex, float length)
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

        Transform holdNote = Instantiate(holdNoteObject, new Vector3(xPosition, holdNoteObject.transform.localPosition.y, Constants.spawnZ), holdNoteObject.rotation);

        GameObject headNote = holdNote.transform.Find("Head").gameObject;
        headNote.GetComponent<Note>().SetStartTime(headHitTime);

        GameObject holdLane = holdNote.transform.Find("HoldLane").gameObject;
        holdNote.transform.Find("HoldLane").localPosition = new Vector3(holdLane.transform.localPosition.x, holdLane.transform.localPosition.y, length / 2.0f);
        holdNote.transform.Find("HoldLane").localScale = new Vector3(holdLane.transform.localScale.x, holdLane.transform.localScale.y, length);

        GameObject tailNote = holdNote.transform.Find("Tail").gameObject;
        holdNote.transform.Find("Tail").localPosition = new Vector3(tailNote.transform.localPosition.x, tailNote.transform.localPosition.y, length);
        tailNote.GetComponent<Note>().SetStartTime(tailHitTime);
    }

    public float GetSongTimer()
    {
        return songTimer;
    }

    public void IncreaseScore(int points, string scoreTypeCount)
    {
        PlayerPrefs.SetInt(Constants.score, PlayerPrefs.GetInt(Constants.score) + points);
        PlayerPrefs.SetInt(Constants.notesHit, PlayerPrefs.GetInt(Constants.notesHit) + 1);
        PlayerPrefs.SetInt(scoreTypeCount, PlayerPrefs.GetInt(scoreTypeCount) + 1);
    }

    public void IncreaseCombo()
    {
        PlayerPrefs.SetInt(Constants.combo, PlayerPrefs.GetInt(Constants.combo) + 1);
    }

    public void ResetCombo()
    {
        if (PlayerPrefs.GetInt(Constants.combo) > PlayerPrefs.GetInt(Constants.selectedSong + Constants.maxCombo))
        {
            PlayerPrefs.SetInt(Constants.selectedSong + Constants.maxCombo, PlayerPrefs.GetInt(Constants.combo));
        }

        PlayerPrefs.SetInt(Constants.combo, 0);
    }

    public void MissNote()
    {
        PlayerPrefs.SetInt(Constants.misses, PlayerPrefs.GetInt(Constants.misses) + 1);
    }

    void InitPlayerPrefs()
    {
        // initialize player prefs
        PlayerPrefs.SetInt(Constants.score, 0);
        PlayerPrefs.SetInt(Constants.combo, 0);
        PlayerPrefs.SetInt(Constants.selectedSong + Constants.maxCombo, 0);
        PlayerPrefs.SetInt(Constants.perfects, 0);
        PlayerPrefs.SetInt(Constants.greats, 0);
        PlayerPrefs.SetInt(Constants.goods, 0);
        PlayerPrefs.SetInt(Constants.bads, 0);
        PlayerPrefs.SetInt(Constants.misses, 0);
        PlayerPrefs.SetInt(Constants.notesHit, 0);
        noteSpeed = PlayerPrefs.GetFloat(Constants.noteSpeed);
        selectedSong = PlayerPrefs.GetString(Constants.selectedSong);
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(10.0f);
        FindObjectOfType<AudioManager>().Stop(selectedSong);
        SceneManager.LoadScene(Constants.scoreScreen);
    }
}
