using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    int[] noteLanes = {1, 2, 3, 4, 3, 2, 2, 3, 4, 1, 1, 2, 3, 2, 2, 3};
    int noteIndex = 0;
    bool timerReset = true;
    float xPosition;
    public Transform noteObject;

    IEnumerator SpawnNote()
    {
        yield return new WaitForSeconds(0.25f);

        if(noteLanes[noteIndex] == 1)
        {
            xPosition = -0.76f;
        }
        else if (noteLanes[noteIndex] == 2)
        {
            xPosition = -0.254f;
        }
        else if (noteLanes[noteIndex] == 3)
        {
            xPosition = 0.254f;
        }
        else if (noteLanes[noteIndex] == 4)
        {
            xPosition = 0.76f;
        }

        noteIndex++;
        timerReset = true;
        Instantiate(noteObject, new Vector3(xPosition, 0.0f, 10.0f), noteObject.rotation);
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
	void Start () {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Combo", 0);
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
