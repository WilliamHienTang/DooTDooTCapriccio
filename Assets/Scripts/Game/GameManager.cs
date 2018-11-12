using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool isTouchingDevice;
    public Transform noteObject;
    public Transform headNoteObject;
    public Transform tailNoteObject;
    public Transform doubleNoteObject;
    public Transform doubleHeadObject;
    public Transform doubleTailObject;
    public Transform oneNoteOneTailObject;
    public Transform oneNoteOneHeadObject;
    public Transform oneHeadOneTailObject;

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

    GameObject holdLaneInstance1;
    GameObject holdLaneInstance2;
    GameObject holdLaneInstance3;
    GameObject holdLaneInstance4;
    GameObject holdLaneInstance5;

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
                InstantiateHead(lane1, length1);
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
                    InstantiateOneNoteOneTail(lane1, lane2, lane1);
                }
                else if (tail2)
                {
                    InstantiateOneNoteOneTail(lane1, lane2, lane2);
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
                    InstantiateOneHeadOneTail(lane1, lane2, lane2, length2);
                }
                else
                {
                    InstantiateOneNoteOneHead(lane1, lane2, lane2, length2);
                }
            }
            else if (length2 == 0)
            {
                if (tail2)
                {
                    InstantiateOneHeadOneTail(lane1, lane2, lane1, length1);
                }
                else
                {
                    InstantiateOneNoteOneHead(lane1, lane2, lane1, length1);
                }
            }
            else
            {
                InstantiateDoubleHead(lane1, lane2, length1, length2);
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

        Instantiate(noteObject, new Vector3(xPosition, noteObject.transform.localPosition.y, Constants.spawnZ), noteObject.rotation);
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

        Instantiate(tailNoteObject, new Vector3(xPosition, tailNoteObject.transform.localPosition.y, Constants.spawnZ), tailNoteObject.rotation);
    }

    void InstantiateHead(int laneIndex, float length)
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

        Transform head = Instantiate(headNoteObject, new Vector3(xPosition, headNoteObject.transform.localPosition.y, Constants.spawnZ), headNoteObject.rotation);

        GameObject holdLane = head.transform.Find("HoldLane").gameObject;
        head.transform.Find("HoldLane").localPosition = new Vector3(holdLane.transform.localPosition.x, holdLane.transform.localPosition.y, length / 2.0f);
        head.transform.Find("HoldLane").localScale = new Vector3(holdLane.transform.localScale.x, holdLane.transform.localScale.y, length);
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

        Transform doubleNote = Instantiate(doubleNoteObject, new Vector3(doubleNoteObject.transform.localPosition.x, doubleNoteObject.transform.localPosition.y, Constants.spawnZ), doubleNoteObject.rotation);

        GameObject note1 = doubleNote.transform.Find("Note1").gameObject;
        GameObject note2 = doubleNote.transform.Find("Note2").gameObject;
        doubleNote.transform.Find("Note1").localPosition = new Vector3(xPosition1, note1.transform.localPosition.y, note1.transform.localPosition.z);
        doubleNote.transform.Find("Note2").localPosition = new Vector3(xPosition2, note2.transform.localPosition.y, note2.transform.localPosition.z);

        note1.transform.parent = null;
        note2.transform.parent = null;
        Destroy(doubleNote);
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

        Transform doubleTail = Instantiate(doubleTailObject, new Vector3(doubleTailObject.transform.localPosition.x, doubleTailObject.transform.localPosition.y, Constants.spawnZ), doubleTailObject.rotation);

        GameObject tail1 = doubleTail.transform.Find("Tail1").gameObject;
        GameObject tail2 = doubleTail.transform.Find("Tail2").gameObject;
        doubleTail.transform.Find("Tail1").localPosition = new Vector3(xPosition1, tail1.transform.localPosition.y, tail2.transform.localPosition.z);
        doubleTail.transform.Find("Tail2").localPosition = new Vector3(xPosition2, tail2.transform.localPosition.y, tail2.transform.localPosition.z);

        tail1.transform.parent = null;
        tail2.transform.parent = null;
        Destroy(doubleTail);
    }

    void InstantiateDoubleHead(int lane1, int lane2, float length1, float length2)
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

        Transform doubleHead = Instantiate(doubleHeadObject, new Vector3(doubleHeadObject.transform.localPosition.x, doubleHeadObject.transform.localPosition.y, Constants.spawnZ), doubleHeadObject.rotation);

        GameObject holdNote1 = doubleHead.transform.Find("HoldNote1").gameObject;
        GameObject holdNote2 = doubleHead.transform.Find("HoldNote2").gameObject;
        doubleHead.transform.Find("HoldNote1").localPosition = new Vector3(xPosition1, holdNote1.transform.localPosition.y, holdNote1.transform.localPosition.z);
        doubleHead.transform.Find("HoldNote2").localPosition = new Vector3(xPosition2, holdNote1.transform.localPosition.y, holdNote1.transform.localPosition.z);

        GameObject holdLane1 = holdNote1.transform.Find("HoldLane").gameObject;
        holdNote1.transform.Find("HoldLane").localPosition = new Vector3(holdLane1.transform.localPosition.x, holdLane1.transform.localPosition.y, length1 / 2.0f);
        holdNote1.transform.Find("HoldLane").localScale = new Vector3(holdLane1.transform.localScale.x, holdLane1.transform.localScale.y, length1);

        GameObject holdLane2 = holdNote2.transform.Find("HoldLane").gameObject;
        holdNote2.transform.Find("HoldLane").localPosition = new Vector3(holdLane2.transform.localPosition.x, holdLane2.transform.localPosition.y, length2 / 2.0f);
        holdNote2.transform.Find("HoldLane").localScale = new Vector3(holdLane2.transform.localScale.x, holdLane2.transform.localScale.y, length2);

        holdNote1.transform.parent = null;
        holdNote2.transform.parent = null;
        Destroy(doubleHead);
    }

    void InstantiateOneNoteOneTail(int lane1, int lane2, int tailLane)
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

        Transform oneNoteOneTail = Instantiate(oneNoteOneTailObject, new Vector3(oneNoteOneTailObject.transform.localPosition.x, oneNoteOneTailObject.transform.localPosition.y, Constants.spawnZ), oneNoteOneTailObject.rotation);
        GameObject tail = oneNoteOneTail.transform.Find("Tail").gameObject;
        GameObject note = oneNoteOneTail.transform.Find("Note").gameObject;
        if (tailLane == lane1)
        {
            oneNoteOneTail.transform.Find("Tail").localPosition = new Vector3(xPosition1, tail.transform.localPosition.y, tail.transform.localPosition.z);
            oneNoteOneTail.transform.Find("Note").localPosition = new Vector3(xPosition2, note.transform.localPosition.y, note.transform.localPosition.z);
        }
        else
        {
            oneNoteOneTail.transform.Find("Tail").localPosition = new Vector3(xPosition2, tail.transform.localPosition.y, tail.transform.localPosition.z);
            oneNoteOneTail.transform.Find("Note").localPosition = new Vector3(xPosition1, note.transform.localPosition.y, note.transform.localPosition.z);
        }

        tail.transform.parent = null;
        note.transform.parent = null;
        Destroy(oneNoteOneTail);
    }

    void InstantiateOneNoteOneHead(int lane1, int lane2, int headLane, float length)
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

        Transform oneNoteOneHead = Instantiate(oneNoteOneHeadObject, new Vector3(oneNoteOneHeadObject.transform.localPosition.x, oneNoteOneHeadObject.transform.localPosition.y, Constants.spawnZ), oneNoteOneHeadObject.rotation);
        GameObject note = oneNoteOneHead.transform.Find("Note").gameObject;
        GameObject holdNote = oneNoteOneHead.transform.Find("HoldNote").gameObject;
        GameObject holdLane = holdNote.transform.Find("HoldLane").gameObject;
        if (headLane == lane1)
        {
            oneNoteOneHead.transform.Find("Note").localPosition = new Vector3(xPosition2, note.transform.localPosition.y, note.transform.localPosition.z);
            oneNoteOneHead.transform.Find("HoldNote").localPosition = new Vector3(xPosition1, holdNote.transform.localPosition.y, holdNote.transform.localPosition.z);
        }
        else
        {
            oneNoteOneHead.transform.Find("Note").localPosition = new Vector3(xPosition1, note.transform.localPosition.y, note.transform.localPosition.z);
            oneNoteOneHead.transform.Find("HoldNote").localPosition = new Vector3(xPosition2, holdNote.transform.localPosition.y, holdNote.transform.localPosition.z);
        }

        holdNote.transform.Find("HoldLane").localPosition = new Vector3(holdLane.transform.localPosition.x, holdLane.transform.localPosition.y, length / 2.0f);
        holdNote.transform.Find("HoldLane").localScale = new Vector3(holdLane.transform.localScale.x, holdLane.transform.localScale.y, length);

        note.transform.parent = null;
        holdNote.transform.parent = null;
        Destroy(oneNoteOneHead);
    }

    void InstantiateOneHeadOneTail(int lane1, int lane2, int headLane, float length)
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

        Transform oneNoteOneTail = Instantiate(oneNoteOneTailObject, new Vector3(oneNoteOneTailObject.transform.localPosition.x, oneNoteOneTailObject.transform.localPosition.y, Constants.spawnZ), oneNoteOneTailObject.rotation);
        GameObject tail = oneNoteOneTail.transform.Find("Tail").gameObject;
        GameObject holdNote = oneNoteOneTail.transform.Find("HoldNote").gameObject;
        GameObject holdLane = holdNote.transform.Find("HoldLane").gameObject;
        if (headLane == lane1)
        {
            oneNoteOneTail.transform.Find("Tail").localPosition = new Vector3(xPosition2, tail.transform.localPosition.y, tail.transform.localPosition.z);
            oneNoteOneTail.transform.Find("HoldNote").localPosition = new Vector3(xPosition1, holdNote.transform.localPosition.y, holdNote.transform.localPosition.z);
        }
        else
        {
            oneNoteOneTail.transform.Find("Tail").localPosition = new Vector3(xPosition1, tail.transform.localPosition.y, tail.transform.localPosition.z);
            oneNoteOneTail.transform.Find("HoldNote").localPosition = new Vector3(xPosition2, holdNote.transform.localPosition.y, holdNote.transform.localPosition.z);
        }

        holdNote.transform.Find("HoldLane").localPosition = new Vector3(holdLane.transform.localPosition.x, holdLane.transform.localPosition.y, length / 2.0f);
        holdNote.transform.Find("HoldLane").localScale = new Vector3(holdLane.transform.localScale.x, holdLane.transform.localScale.y, length);

        tail.transform.parent = null;
        holdNote.transform.parent = null;
        Destroy(oneNoteOneTail);
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
