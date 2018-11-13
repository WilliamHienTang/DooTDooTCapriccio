using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool isTouchingDevice;
    public Transform noteObject;
    public Transform holdNoteObject;
    public Transform tailNoteObject;
    public Transform doubleNoteObject;
    public Transform doubleHoldObject;
    public Transform doubleTailObject;
    public Transform oneNoteOneTailObject;
    public Transform oneNoteOneHoldObject;
    public Transform oneHoldOneTailObject;

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

    Transform holdLaneInstance1;
    Transform holdLaneInstance2;
    Transform holdLaneInstance3;
    Transform holdLaneInstance4;
    Transform holdLaneInstance5;

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

        int lane1 = 0;
        float length1 = 0; 
        bool tail1 = false;

        if ((noteChart[chartIndex].headHitTime - speedOffset) <= songTimer && noteChart[chartIndex].headHitTime > 0)
        {
            lane1 = noteChart[chartIndex].laneIndex;

            if (noteChart[chartIndex].tailHitTime > 0)
            {
                length1 = noteSpeed * (noteChart[chartIndex].tailHitTime - noteChart[chartIndex].headHitTime);
            }
            chartIndex++;
        }
        else if ((noteChart[chartIndex].tailHitTime - speedOffset) <= songTimer && noteChart[chartIndex].headHitTime == 0)
        {
            lane1 = noteChart[chartIndex].laneIndex;
            tail1 = true;
            chartIndex++;
        }
        else
        {
            return;
        }

        if (chartIndex >= noteChart.Length)
        {
            InstantiateNotes(lane1, 0, length1, 0, tail1, false);
            enabled = false;
            StartCoroutine(EndGame());
            return;
        }

        int lane2 = 0;
        float length2 = 0;
        bool tail2 = false;

        if ((noteChart[chartIndex].headHitTime - speedOffset) <= songTimer && noteChart[chartIndex].headHitTime > 0)
        {
            lane2 = noteChart[chartIndex].laneIndex;

            if (noteChart[chartIndex].tailHitTime > 0)
            {
                length2 = noteSpeed * (noteChart[chartIndex].tailHitTime - noteChart[chartIndex].headHitTime);
            }
            chartIndex++;
        }
        else if ((noteChart[chartIndex].tailHitTime - speedOffset) <= songTimer && noteChart[chartIndex].headHitTime == 0)
        {
            lane2 = noteChart[chartIndex].laneIndex;
            tail2 = true;
            chartIndex++;
        }

        InstantiateNotes(lane1, lane2, length1, length2, tail1, tail2);
    }

    void InstantiateNotes(int lane1, int lane2, float length1, float length2, bool tail1, bool tail2)
    {
        if (lane1 == 0)
        {
            return;
        }

        if (lane2 == 0)
        {
            if (length1 == 0)
            {
                if (!tail1)
                {
                    InstantiateNote(lane1);
                }
                else
                {
                    InstantiateTail(lane1);
                }
            }
            else
            {
                InstantiateHold(lane1, length1);
            }
        }

        else
        {
            if (length1 == 0 && length2 == 0)
            {
                if(tail1 && tail2)
                {
                    InstantiateDoubleTail(lane1, lane2);
                }
                else if (tail1)
                {
                    InstantiateOneNoteOneTail(lane2, lane1);
                }
                else if (tail2)
                {
                    InstantiateOneNoteOneTail(lane1, lane2);
                }
                else
                {
                    InstantiateDoubleNote(lane1, lane2);
                }
            }
            else if (length1 == 0)
            {
                if (tail1)
                {
                    InstantiateOneHoldOneTail(lane2, lane1, length2);
                }
                else
                {
                    InstantiateOneNoteOneHold(lane1, lane2, length2);
                }
            }
            else if (length2 == 0)
            {
                if (tail2)
                {
                    InstantiateOneHoldOneTail(lane1, lane2, length1);
                }
                else
                {
                    InstantiateOneNoteOneHold(lane2, lane1, length1);
                }
            }
            else
            {
                InstantiateDoubleHold(lane1, lane2, length1, length2);
            }
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

        Instantiate(noteObject, new Vector3(xPosition, noteObject.localPosition.y, Constants.spawnZ), noteObject.rotation);
    }

    void InstantiateTail(int laneIndex)
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

        Transform tail = Instantiate(tailNoteObject, new Vector3(xPosition, tailNoteObject.localPosition.y, Constants.spawnZ), tailNoteObject.rotation);

        switch (laneIndex)
        {
            case 1:
                tail.parent = holdLaneInstance1;
                break;
            case 2:
                tail.parent = holdLaneInstance2;
                break;
            case 3:
                tail.parent = holdLaneInstance3;
                break;
            case 4:
                tail.parent = holdLaneInstance4;
                break;
            case 5:
                tail.parent = holdLaneInstance5;
                break;
            default:
                return;
        }
    }

    void InstantiateHold(int laneIndex, float length)
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

        Transform holdNote = Instantiate(holdNoteObject, new Vector3(xPosition, holdNoteObject.localPosition.y, Constants.spawnZ), holdNoteObject.rotation);
        Transform holdLane = holdNote.Find("HoldLane");
        holdLane.localPosition = new Vector3(holdLane.localPosition.x, holdLane.localPosition.y, length / 2.0f);
        holdLane.localScale = new Vector3(holdLane.localScale.x, holdLane.localScale.y, length);

        switch (laneIndex)
        { 
            case 1:
                holdLaneInstance1 = holdNote;
                break;
            case 2:
                holdLaneInstance2 = holdNote;
                break;
            case 3:
                holdLaneInstance3 = holdNote;
                break;
            case 4:
                holdLaneInstance4 = holdNote;
                break;
            case 5:
                holdLaneInstance5 = holdNote;
                break;
            default:
                return;
        }
    }

    void InstantiateDoubleNote(int lane1, int lane2)
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

        Transform doubleNote = Instantiate(doubleNoteObject, new Vector3(doubleNoteObject.localPosition.x, doubleNoteObject.localPosition.y, Constants.spawnZ), doubleNoteObject.rotation);

        Transform note1 = doubleNote.Find("Note1");
        note1.localPosition = new Vector3(xPosition1, note1.localPosition.y, note1.localPosition.z);

        Transform note2 = doubleNote.Find("Note2");
        note2.localPosition = new Vector3(xPosition2, note2.localPosition.y, note2.localPosition.z);

        note1.parent = null;
        note2.parent = null;
        Destroy(doubleNote.gameObject);
    }

    void InstantiateDoubleTail(int lane1, int lane2)
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

        Transform doubleTail = Instantiate(doubleTailObject, new Vector3(doubleTailObject.localPosition.x, doubleTailObject.localPosition.y, Constants.spawnZ), doubleTailObject.rotation);

        Transform tail1 = doubleTail.Find("Tail1");
        tail1.localPosition = new Vector3(xPosition1, tail1.localPosition.y, tail1.localPosition.z);

        Transform tail2 = doubleTail.Find("Tail2");
        tail2.localPosition = new Vector3(xPosition2, tail2.localPosition.y, tail2.localPosition.z);

        tail1.parent = null;
        tail2.parent = null;
        Destroy(doubleTail.gameObject);

        switch (lane1)
        {
            case 1:
                tail1.parent = holdLaneInstance1;
                break;
            case 2:
                tail1.parent = holdLaneInstance2;
                break;
            case 3:
                tail1.parent = holdLaneInstance3;
                break;
            case 4:
                tail1.parent = holdLaneInstance4;
                break;
            case 5:
                tail1.parent = holdLaneInstance5;
                break;
            default:
                return;
        }

        switch (lane2)
        {
            case 1:
                tail2.parent = holdLaneInstance1;
                break;
            case 2:
                tail2.parent = holdLaneInstance2;
                break;
            case 3:
                tail2.parent = holdLaneInstance3;
                break;
            case 4:
                tail2.parent = holdLaneInstance4;
                break;
            case 5:
                tail2.parent = holdLaneInstance5;
                break;
            default:
                return;
        }
    }

    void InstantiateDoubleHold(int lane1, int lane2, float length1, float length2)
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

        Transform doubleHead = Instantiate(doubleHoldObject, new Vector3(doubleHoldObject.localPosition.x, doubleHoldObject.localPosition.y, Constants.spawnZ), doubleHoldObject.rotation);

        Transform holdNote1 = doubleHead.Find("HoldNote1");
        holdNote1.localPosition = new Vector3(xPosition1, holdNote1.localPosition.y, holdNote1.localPosition.z);

        Transform holdNote2 = doubleHead.Find("HoldNote2");
        holdNote2.localPosition = new Vector3(xPosition2, holdNote2.localPosition.y, holdNote2.localPosition.z);

        Transform holdLane1 = holdNote1.Find("HoldLane");
        holdLane1.localPosition = new Vector3(holdLane1.localPosition.x, holdLane1.localPosition.y, length1 / 2.0f);
        holdLane1.localScale = new Vector3(holdLane1.localScale.x, holdLane1.localScale.y, length1);

        Transform holdLane2 = holdNote2.Find("HoldLane");
        holdLane2.localPosition = new Vector3(holdLane2.localPosition.x, holdLane2.localPosition.y, length2 / 2.0f);
        holdLane2.localScale = new Vector3(holdLane2.localScale.x, holdLane2.localScale.y, length2);

        holdNote1.parent = null;
        holdNote2.parent = null;
        Destroy(doubleHead.gameObject);

        switch (lane1)
        {
            case 1:
                holdLaneInstance1 = holdNote1;
                break;
            case 2:
                holdLaneInstance2 = holdNote1;
                break;
            case 3:
                holdLaneInstance3 = holdNote1;
                break;
            case 4:
                holdLaneInstance4 = holdNote1;
                break;
            case 5:
                holdLaneInstance5 = holdNote1;
                break;
            default:
                return;
        }

        switch (lane2)
        {
            case 1:
                holdLaneInstance1 = holdNote2;
                break;
            case 2:
                holdLaneInstance2 = holdNote2;
                break;
            case 3:
                holdLaneInstance3 = holdNote2;
                break;
            case 4:
                holdLaneInstance4 = holdNote2;
                break;
            case 5:
                holdLaneInstance5 = holdNote2;
                break;
            default:
                return;
        }
    }

    void InstantiateOneNoteOneTail(int noteLane, int tailLane)
    {
        float noteXPosition;
        switch (noteLane)
        {
            case 1:
                noteXPosition = Constants.lane1X;
                break;
            case 2:
                noteXPosition = Constants.lane2X;
                break;
            case 3:
                noteXPosition = Constants.lane3X;
                break;
            case 4:
                noteXPosition = Constants.lane4X;
                break;
            case 5:
                noteXPosition = Constants.lane5X;
                break;
            default:
                noteXPosition = 0;
                break;
        }

        float tailXPosition;
        switch (tailLane)
        {
            case 1:
                tailXPosition = Constants.lane1X;
                break;
            case 2:
                tailXPosition = Constants.lane2X;
                break;
            case 3:
                tailXPosition = Constants.lane3X;
                break;
            case 4:
                tailXPosition = Constants.lane4X;
                break;
            case 5:
                tailXPosition = Constants.lane5X;
                break;
            default:
                tailXPosition = 0;
                break;
        }

        Transform oneNoteOneTail = Instantiate(oneNoteOneTailObject, new Vector3(oneNoteOneTailObject.localPosition.x, oneNoteOneTailObject.localPosition.y, Constants.spawnZ), oneNoteOneTailObject.rotation);

        Transform note = oneNoteOneTail.Find("Note");
        note.localPosition = new Vector3(noteXPosition, note.localPosition.y, note.localPosition.z);

        Transform tail = oneNoteOneTail.Find("Tail");
        tail.localPosition = new Vector3(tailXPosition, tail.localPosition.y, tail.localPosition.z);

        note.parent = null;
        tail.parent = null;
        Destroy(oneNoteOneTail.gameObject);

        switch (tailLane)
        {
            case 1:
                tail.parent = holdLaneInstance1;
                break;
            case 2:
                tail.parent = holdLaneInstance2;
                break;
            case 3:
                tail.parent = holdLaneInstance3;
                break;
            case 4:
                tail.parent = holdLaneInstance4;
                break;
            case 5:
                tail.parent = holdLaneInstance5;
                break;
            default:
                return;
        }
    }

    void InstantiateOneNoteOneHold(int noteLane, int holdNoteLane, float length)
    {
        float noteXPosition;
        switch (noteLane)
        {
            case 1:
                noteXPosition = Constants.lane1X;
                break;
            case 2:
                noteXPosition = Constants.lane2X;
                break;
            case 3:
                noteXPosition = Constants.lane3X;
                break;
            case 4:
                noteXPosition = Constants.lane4X;
                break;
            case 5:
                noteXPosition = Constants.lane5X;
                break;
            default:
                noteXPosition = 0;
                break;
        }

        float holdXPosition;
        switch (holdNoteLane)
        {
            case 1:
                holdXPosition = Constants.lane1X;
                break;
            case 2:
                holdXPosition = Constants.lane2X;
                break;
            case 3:
                holdXPosition = Constants.lane3X;
                break;
            case 4:
                holdXPosition = Constants.lane4X;
                break;
            case 5:
                holdXPosition = Constants.lane5X;
                break;
            default:
                holdXPosition = 0;
                break;
        }

        Transform oneNoteOneHold = Instantiate(oneNoteOneHoldObject, new Vector3(oneNoteOneHoldObject.localPosition.x, oneNoteOneHoldObject.localPosition.y, Constants.spawnZ), oneNoteOneHoldObject.rotation);

        Transform note = oneNoteOneHold.Find("Note");
        note.localPosition = new Vector3(noteXPosition, note.localPosition.y, note.localPosition.z);

        Transform holdNote = oneNoteOneHold.Find("HoldNote");
        holdNote.localPosition = new Vector3(holdXPosition, holdNote.localPosition.y, holdNote.localPosition.z);

        Transform holdLane = holdNote.Find("HoldLane");
        holdLane.localPosition = new Vector3(holdLane.localPosition.x, holdLane.localPosition.y, length / 2.0f);
        holdLane.localScale = new Vector3(holdLane.localScale.x, holdLane.localScale.y, length);

        note.parent = null;
        holdNote.parent = null;
        Destroy(oneNoteOneHold.gameObject);

        switch (holdNoteLane)
        {
            case 1:
                holdLaneInstance1 = holdNote;
                break;
            case 2:
                holdLaneInstance2 = holdNote;
                break;
            case 3:
                holdLaneInstance3 = holdNote;
                break;
            case 4:
                holdLaneInstance4 = holdNote;
                break;
            case 5:
                holdLaneInstance5 = holdNote;
                break;
            default:
                return;
        }
    }

    void InstantiateOneHoldOneTail(int holdNoteLane, int tailLane, float length)
    {
        float holdXPosition;
        switch (holdNoteLane)
        {
            case 1:
                holdXPosition = Constants.lane1X;
                break;
            case 2:
                holdXPosition = Constants.lane2X;
                break;
            case 3:
                holdXPosition = Constants.lane3X;
                break;
            case 4:
                holdXPosition = Constants.lane4X;
                break;
            case 5:
                holdXPosition = Constants.lane5X;
                break;
            default:
                holdXPosition = 0;
                break;
        }

        float tailXPosition;
        switch (tailLane)
        {
            case 1:
                tailXPosition = Constants.lane1X;
                break;
            case 2:
                tailXPosition = Constants.lane2X;
                break;
            case 3:
                tailXPosition = Constants.lane3X;
                break;
            case 4:
                tailXPosition = Constants.lane4X;
                break;
            case 5:
                tailXPosition = Constants.lane5X;
                break;
            default:
                tailXPosition = 0;
                break;
        }

        Transform oneHoldOneTail = Instantiate(oneHoldOneTailObject, new Vector3(oneHoldOneTailObject.localPosition.x, oneHoldOneTailObject.localPosition.y, Constants.spawnZ), oneHoldOneTailObject.rotation);

        Transform tail = oneHoldOneTail.Find("Tail");
        tail.localPosition = new Vector3(tailXPosition, tail.localPosition.y, tail.localPosition.z);

        Transform holdNote = oneHoldOneTail.Find("HoldNote");
        holdNote.localPosition = new Vector3(holdXPosition, holdNote.localPosition.y, holdNote.localPosition.z);

        Transform holdLane = holdNote.Find("HoldLane");
        holdLane.localPosition = new Vector3(holdLane.localPosition.x, holdLane.localPosition.y, length / 2.0f);
        holdLane.localScale = new Vector3(holdLane.localScale.x, holdLane.localScale.y, length);

        tail.parent = null;
        holdNote.parent = null;
        Destroy(oneHoldOneTail.gameObject);

        switch (holdNoteLane)
        {
            case 1:
                holdLaneInstance1 = holdNote;
                break;
            case 2:
                holdLaneInstance2 = holdNote;
                break;
            case 3:
                holdLaneInstance3 = holdNote;
                break;
            case 4:
                holdLaneInstance4 = holdNote;
                break;
            case 5:
                holdLaneInstance5 = holdNote;
                break;
            default:
                return;
        }

        switch (tailLane)
        {
            case 1:
                tail.parent = holdLaneInstance1;
                break;
            case 2:
                tail.parent = holdLaneInstance2;
                break;
            case 3:
                tail.parent = holdLaneInstance3;
                break;
            case 4:
                tail.parent = holdLaneInstance4;
                break;
            case 5:
                tail.parent = holdLaneInstance5;
                break;
            default:
                return;
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
