using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    float velocityOffset;
    readonly float songDelay = 5.0f;
    readonly float spawnDistance = 20.0f;
    bool flag = true;

    public Transform noteObject;
    public Transform holdNoteObject;

    // Use this for initialization
    IEnumerator Start()
    {
        enabled = false;
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Combo", 0);

        FindObjectOfType<AudioManager>().Play(PlayerPrefs.GetString("SelectedSong"));
        velocityOffset = spawnDistance / PlayerPrefs.GetFloat("NoteSpeed");
        yield return new WaitForSeconds(songDelay - velocityOffset);
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            //InstantiateNote(4);
            InstantiateHoldNote(2, 5.0f);
        }
        flag = false;
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
