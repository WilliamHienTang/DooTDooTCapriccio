using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public string songName;
    int[] noteLanes = {1, 5, 5, 4, 3, 2, 5, 2, 5, 4, 5, 2, 3, 5, 3, 5, 5};
    int noteIndex = 0;
    bool timerReset = true;
    float xPosition;
    public Transform noteObject;
    public Transform holdNoteObject;

    IEnumerator SpawnNote()
    {
        yield return new WaitForSeconds(0.5f);

        if(noteLanes[noteIndex] == 1)
        {
            xPosition = -1.02f;
        }
        else if (noteLanes[noteIndex] == 2)
        {
            xPosition = -0.51f;
        }
        else if (noteLanes[noteIndex] == 3)
        {
            xPosition = 0f;
        }
        else if (noteLanes[noteIndex] == 4)
        {
            xPosition = 0.51f;
        }
        else if (noteLanes[noteIndex] == 5)
        {
            xPosition = 1.02f;
        }

        noteIndex++;
        timerReset = true;

        Instantiate(noteObject, new Vector3(xPosition, 0.0f, 10.0f), noteObject.rotation);
        //Instantiate(holdNoteObject, new Vector3(xPosition, 0.0f, 10.0f), holdNoteObject.rotation);
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

	// Use this for initialization
	IEnumerator Start () {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Combo", 0);

        yield return new WaitForSeconds(1.0f);
        FindObjectOfType<AudioManager>().Play(PlayerPrefs.GetString("SelectedSong"));
    }
	
	// Update is called once per frame
	void Update () {
        if (timerReset && noteIndex < noteLanes.Length)
        {
            timerReset = false;
            StartCoroutine(SpawnNote());
        }
	}
}
