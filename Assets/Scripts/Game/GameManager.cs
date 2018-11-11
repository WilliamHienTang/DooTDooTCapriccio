using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool isTouchingDevice;
    public Transform noteObject;
    public Transform holdNoteObject;
    public Transform doubleNoteObject;

    string song;
    string difficulty;
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
        jsonPath = Application.dataPath + "/JsonNoteCharts/" + song + "_" + difficulty + ".json";
        json = File.ReadAllText(jsonPath);
        noteChart = JsonHelper.FromJson<NoteSpawn>(json);

        // play song
        FindObjectOfType<AudioManager>().Play(song);

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
            enabled = false;
            StartCoroutine(EndGame());
            return;
        }

        /*int lane1 = 0;
        int lane2 = 0;
        float length1 = 0; 
        float length2 = 0;*/

        if ((noteChart[chartIndex].headHitTime - speedOffset) <= songTimer)
        {
            //lane1 = noteChart[chartIndex].laneIndex;

            if (noteChart[chartIndex].tailHitTime > 0)
            {
                //length1 = noteSpeed * (noteChart[chartIndex].tailHitTime - noteChart[chartIndex].headHitTime);
                float length = noteSpeed * (noteChart[chartIndex].tailHitTime - noteChart[chartIndex].headHitTime);
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
            enabled = false;
            StartCoroutine(EndGame());
            return;
        }

        if ((noteChart[chartIndex].headHitTime - speedOffset) <= songTimer)
        {
            //lane2 = noteChart[chartIndex].laneIndex;

            if (noteChart[chartIndex].tailHitTime > 0)
            {
                //length2 = noteSpeed * (noteChart[chartIndex].tailHitTime - noteChart[chartIndex].headHitTime);
                
                float length = noteSpeed * (noteChart[chartIndex].tailHitTime - noteChart[chartIndex].headHitTime);
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

        Instantiate(noteObject, new Vector3(xPosition, noteObject.transform.localPosition.y, Constants.spawnZ), noteObject.rotation);
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

        Transform holdNote = Instantiate(holdNoteObject, new Vector3(xPosition, holdNoteObject.transform.localPosition.y, Constants.spawnZ), holdNoteObject.rotation);

        GameObject holdLane = holdNote.transform.Find("HoldLane").gameObject;
        holdNote.transform.Find("HoldLane").localPosition = new Vector3(holdLane.transform.localPosition.x, holdLane.transform.localPosition.y, length / 2.0f);
        holdNote.transform.Find("HoldLane").localScale = new Vector3(holdLane.transform.localScale.x, holdLane.transform.localScale.y, length);

        GameObject tailNote = holdNote.transform.Find("Tail").gameObject;
        holdNote.transform.Find("Tail").localPosition = new Vector3(tailNote.transform.localPosition.x, tailNote.transform.localPosition.y, length);
    }

    void InstantiateDoubleNote(int lane1, int lane2, float length1, float length2)
    {
        float xPosition1;
        switch (lane1)
        {
            case 1:
                xPosition1 = Constants.lane1X;
                break;
            case 2:
                xPosition1 = Constants.lane2X;
                break;
            case 3:
                xPosition1 = Constants.lane3X;
                break;
            case 4:
                xPosition1 = Constants.lane4X;
                break;
            case 5:
                xPosition1 = Constants.lane5X;
                break;
            default:
                xPosition1 = 0;
                break; 
        }

        float xPosition2;
        switch (lane2)
        {
            case 1:
                xPosition2 = Constants.lane1X;
                break;
            case 2:
                xPosition2 = Constants.lane2X;
                break;
            case 3:
                xPosition2 = Constants.lane3X;
                break;
            case 4:
                xPosition2 = Constants.lane4X;
                break;
            case 5:
                xPosition2 = Constants.lane5X;
                break;
            default:
                xPosition2 = 0;
                break;
        }

        if (length1 > 0)
        {
            Transform holdNote = Instantiate(holdNoteObject, new Vector3(xPosition1, holdNoteObject.transform.localPosition.y, Constants.spawnZ), holdNoteObject.rotation);

            GameObject holdLane = holdNote.transform.Find("HoldLane").gameObject;
            holdNote.transform.Find("HoldLane").localPosition = new Vector3(holdLane.transform.localPosition.x, holdLane.transform.localPosition.y, length1 / 2.0f);
            holdNote.transform.Find("HoldLane").localScale = new Vector3(holdLane.transform.localScale.x, holdLane.transform.localScale.y, length1);

            GameObject tailNote = holdNote.transform.Find("Tail").gameObject;
            holdNote.transform.Find("Tail").localPosition = new Vector3(tailNote.transform.localPosition.x, tailNote.transform.localPosition.y, length1);
        }
        else if (lane1 > 0)
        {
            Instantiate(noteObject, new Vector3(xPosition1, noteObject.transform.localPosition.y, Constants.spawnZ), noteObject.rotation);
        }


        if (length2 > 0)
        {
            Transform holdNote = Instantiate(holdNoteObject, new Vector3(xPosition2, holdNoteObject.transform.localPosition.y, Constants.spawnZ), holdNoteObject.rotation);

            GameObject holdLane = holdNote.transform.Find("HoldLane").gameObject;
            holdNote.transform.Find("HoldLane").localPosition = new Vector3(holdLane.transform.localPosition.x, holdLane.transform.localPosition.y, length2 / 2.0f);
            holdNote.transform.Find("HoldLane").localScale = new Vector3(holdLane.transform.localScale.x, holdLane.transform.localScale.y, length2);

            GameObject tailNote = holdNote.transform.Find("Tail").gameObject;
            holdNote.transform.Find("Tail").localPosition = new Vector3(tailNote.transform.localPosition.x, tailNote.transform.localPosition.y, length2);
        }
        else if (lane2 > 0)
        {
            Instantiate(noteObject, new Vector3(xPosition2, noteObject.transform.localPosition.y, Constants.spawnZ), noteObject.rotation);
        }
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
        if (PlayerPrefs.GetInt(Constants.combo) > PlayerPrefs.GetInt(song + difficulty + Constants.maxCombo))
        {
            PlayerPrefs.SetInt(song + difficulty + Constants.maxCombo, PlayerPrefs.GetInt(Constants.combo));
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
        noteSpeed = PlayerPrefs.GetFloat(Constants.noteSpeed);
        song = PlayerPrefs.GetString(Constants.selectedSong);
        difficulty = PlayerPrefs.GetString(Constants.difficulty);

        PlayerPrefs.SetInt(Constants.score, 0);
        PlayerPrefs.SetInt(Constants.combo, 0);
        PlayerPrefs.SetInt(song + difficulty + Constants.maxCombo, 0);
        PlayerPrefs.SetInt(Constants.perfects, 0);
        PlayerPrefs.SetInt(Constants.greats, 0);
        PlayerPrefs.SetInt(Constants.goods, 0);
        PlayerPrefs.SetInt(Constants.bads, 0);
        PlayerPrefs.SetInt(Constants.misses, 0);
        PlayerPrefs.SetInt(Constants.notesHit, 0);
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(speedOffset + Constants.songDelay);
        FindObjectOfType<AudioManager>().Stop(song);
        SceneManager.LoadScene(Constants.scoreScreen);
    }
}
