using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPrefsText : MonoBehaviour {

    public TextMeshProUGUI score;
    public TextMeshProUGUI combo;

    // Use this for initialization
    void Start () {
		
	}

	// Update is called once per frame
	void Update () {
        score.text = AddLeadingScoreZeros(PlayerPrefs.GetInt(Constants.score));
        combo.text = PlayerPrefs.GetInt(Constants.combo).ToString();
    }

    string AddLeadingScoreZeros(int score)
    {
        string scoreString = score.ToString();
        string zeros = "";

        int numZeros = Constants.scoreDigits - scoreString.Length;
        for (int i = 0; i < numZeros; i++)
        {
            zeros += "<color=#808080>0</color>";
        }
        return zeros + scoreString;
    }
}
